using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;

namespace RotoscopyTool
{
    public class VideoControllerEditor : EditorWindow
    {
        private VideoController videoController;
        
        private Button playButton;
        private Button pauseButton;
        private ProgressBar progressBar;
        private Label sliderLabel;
        private Slider videoSlider; 
        
        IVisualElementScheduledItem progressBarSchedule;
        bool isPlaying = false;
        
        //Shows window when click menu
        [MenuItem("Tools/Show Video PLayer Editor (Select VideoPlayer Controller)")]
        public static void ShowTool()
        {
            VideoControllerEditor wnd = GetWindow<VideoControllerEditor>();
            wnd.titleContent = new GUIContent("Video Player Controller");
        }
    
        //enable/disable menu when no VideoController gameobject selected 
        [MenuItem("Tools/Show Video PLayer Editor (Select VideoPlayer Controller)", true)]
        private static bool IsVideoManagerSelected()
        {
            return Selection.activeGameObject != null &&
                   Selection.activeGameObject.GetComponent<VideoController>() != null;
        }

        public void CreateGUI()
        {
            GetVideoPlayerController();
            if (videoController == null) return;

            Debug.Log("Editor window CreateGUI");
            VisualElement root = rootVisualElement;
            
            VisualElement label = new Label("Runtime video controller");
            root.Add(label);

            playButton = new Button{ text = "Play video" };
            playButton.name = "playbtn";
            playButton.clicked += PlayClick;
            root.Add(playButton);
            
            pauseButton = new Button{ text = "Pause video" };
            pauseButton.name = "pausebtn";
            pauseButton.clicked += PauseClick;
            root.Add(pauseButton);
            
            progressBar = new ProgressBar
            {
                title = "Play position",
                lowValue = 0f,
                highValue = 100f,
                value = 0f
                
            };

            progressBar.style.backgroundColor = Color.green;
            progressBar.style.color = Color.green;
            
            //init and start progress update
            AsyncUpdateProgressBar();
            
            root.Add(progressBar);
            
            videoSlider = new Slider
            {
                name = "videoSlider",
                lowValue = 0.25f,
                highValue = 1f,
                value = 1f
            };

            videoSlider.value = videoController.PlaySpeed;
            
            // Get the resolved width of the root element
            float rootWidth = rootVisualElement.resolvedStyle.width;
            videoSlider.style.width = rootWidth- 100; // Set a width for the slider, adjust for padding
            root.Add(videoSlider);
            
            sliderLabel = new Label("Video speed"); // Initial label text (adjust format)
            sliderLabel.name = "sliderLabel";
            sliderLabel.text = "Video speed : " + videoSlider.value;
            root.Add(sliderLabel);
            
            //when click inside progress bar
            root.RegisterCallback<PointerDownEvent>(evt =>
            {
                if (RectContainsPoint(progressBar.layout, evt.localPosition))
                {
                    Vector2 clickPosition = evt.localPosition;
                    
                    float t = Mathf.InverseLerp(progressBar.layout.xMin, progressBar.layout.xMax, evt.localPosition.x);
                    videoController.PercentSeek( t );
                }
            });
            
            //when slider value change
            videoSlider.RegisterValueChangedCallback(evt =>
            {
                videoSlider.value = evt.newValue;
                videoController.PlaySpeed = evt.newValue;
                sliderLabel.text = "Video speed : " + evt.newValue;
            });
        }

        public void GetVideoPlayerController()
        {
            if (videoController == null)
            {
                GameObject selectedGameObject = Selection.activeGameObject;
                
                VideoController tmpVideoPlayerController = selectedGameObject?.GetComponent<VideoController>();

                if (tmpVideoPlayerController == null || tmpVideoPlayerController != videoController)
                {
                    Debug.LogWarning("No Video Controller selected.");
                    videoController = FindObjectOfType<VideoController>();
                    if (videoController == null)
                    {
                        Debug.LogWarning("No Video Controller found.");
                    }
                }
                else
                {
                    videoController = tmpVideoPlayerController;
                    Debug.LogWarning("Video Controller found and selected.");
                }
            }
        }
        
        private bool RectContainsPoint(Rect rect, Vector2 point)
        {
            return point.x >= rect.xMin && point.x <= rect.xMax &&
                   point.y >= rect.yMin && point.y <= rect.yMax;
        }
        
        private void AsyncUpdateProgressBar()
        {
            if (videoController)
            {
                isPlaying = true;
                progressBarSchedule?.Resume();
                progressBarSchedule = progressBar.schedule.Execute(() =>
                {
                    progressBar.value = 100f * videoController.GetNormalizedSeek();

                    if (progressBar.value >= 100f)
                    {
                        isPlaying = false;
                    }
                
                    Debug.Log(progressBar.value);
                }).Every(75).Until(() => !isPlaying); // Continue until flag is false                
            }
        }
        
        private void AsyncFreezeProgressBar()
        {
            if (videoController)
            {
                isPlaying = false;
                progressBarSchedule?.Pause();
            }
        }
        
        private void PlayClick()
        {
            AsyncUpdateProgressBar();
            if (videoController)
            {
                videoController.PlayVideo();
            }
            
        }
        
        private void PauseClick()
        {
            AsyncFreezeProgressBar();
            if (videoController)
            {
                videoController.PauseVideo();
            }
        }
    }
    
    
}