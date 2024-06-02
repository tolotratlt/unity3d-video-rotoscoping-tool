using UnityEngine.Video;

namespace RotoscopyTool {

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoController : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        
        void Start()
        {
            if( TryGetComponent(out VideoPlayer vplayer) )
            {
                videoPlayer = vplayer;
                videoPlayer.isLooping = true;
                
            }
            
        }

        
        void Update()
        {
            
        }

        public void PlayVideo()
        {
            if (videoPlayer)
            {
                videoPlayer.Play();
            }
        }

        public void PauseVideo()
        {
            if (videoPlayer)
            {
                videoPlayer.Pause();
            }
        }

        //takes a value between 0 to 1
        public void PercentSeek(float pct)
        {
            if (videoPlayer)
            {
                float frame = videoPlayer.frameCount * pct;
                videoPlayer.frame = (long)frame;
            }
        }

        //return value between 0 and 1
        public float GetNormalizedSeek()
        {
            if (videoPlayer == null)
            {
                return 0.0f;
            }

            return (float)videoPlayer.frame / videoPlayer.frameCount;
        }
        
        public float PlaySpeed
        {
            get
            {
                if (videoPlayer == null)
                {
                    return 1f;
                }

                return videoPlayer.playbackSpeed;
            }
            set
            {
                if (videoPlayer == null)
                {
                    return;
                }

                videoPlayer.playbackSpeed = value;
            }
        }
    }

}