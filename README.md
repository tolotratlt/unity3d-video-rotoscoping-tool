# Unity3d video rotoscoping tool

Play video in render texture for animation & motion capture.

This repository provides a lightweight tool for playing video footage within Unity 3D's render texture.
This allows you to seamlessly integrate video playback into your projects:

* Overlay video content onto your 3D scenes.
* Use video as a reference for animation and motion capture.
* Facilitate rotoscoping techniques with tools like [UMotion](https://www.soxware.com/umotion/) ([UMotion Comunity](https://assetstore.unity.com/packages/tools/animation/umotion-community-animation-editor-95986), [UMotion Pro](https://assetstore.unity.com/packages/tools/animation/umotion-pro-animation-editor-95991), [Pose Editor](https://assetstore.unity.com/packages/tools/animation/pose-editor-146667)).

**Features:**

* Play video files within a render texture.
* Control playback: play, pause, and seek.
* Customize video properties: playback speed.
* Integrate the render texture into your scenes using RawImage (Canvas UI) or 3D meshes (Quad Mesh).

**Getting Started:**

1. Clone or download the repository.
2. Place the RotoscopyTool folder into your Unity project.
3. Add the RotoscopVideoPlayer prefab to your scene.
4. Assign your video to the Video Clip property of the Video Player component within the RotoscopVideoPlayer prefab.
5. Use the RotoscopyVideoRenderTexture as the texture for a RawImage or 3D mesh.
6. Access the tool's editor for additional options:
* Select a GameObject containing the "Video Controller" component.
* Go to "Tools > Show Video Editor Player Editor". This menu won't activate if you don't select a GameObject containing the "Video Controller" component.
7. In the opened Editor Window controller, click play, pause to control the video.
8. Click inside the progress bar to seek within the video. Seeking within the video currently works best while playing.
9. Adjust video speed from 25% to 100% using the slider.
10. The tool functions both in the Unity editor and in-game.


# .
**Additional Information:**

* This project is under development and open to contributions.
* Feel free to use and modify the code for your own purposes.
* If you encounter any issues or have suggestions, please create an issue on the repository.


Enjoy!