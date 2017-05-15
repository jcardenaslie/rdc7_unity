using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace RDC.Video
{
    public class VideoStream : MonoBehaviour 
    {
        public RawImage rawImage;
        public VideoClip videoClip;
        public Text totalTimeText;
        public Text currentTimeText;
        public Slider timeSlider;

        private VideoPlayer videoPlayer;
        private VideoSource videoSource;
        private AudioSource audioSource;  
        private FluxAnimation m_FluxAnimation;

        void Start()
        {
            Application.runInBackground = true;
            m_FluxAnimation = GetComponent<FluxAnimation>();
            MoveAway();
            StartCoroutine(InitVideo());
        }

        public void MoveAway()
        {
            transform.position = new Vector3(9999, 0, 0);
        }

        public void ResetPosition()
        {
            transform.position = new Vector3(0, 0, 0);
        }

        public void Open()
        {
            m_FluxAnimation.StartOpeningAnimation();
        }

        public void Close()
        {
            Stop();
            m_FluxAnimation.StartClosingAnimation();
        }

        IEnumerator InitVideo()
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
            audioSource = gameObject.AddComponent<AudioSource>();

            videoPlayer.playOnAwake = false;
            audioSource.playOnAwake = false;

            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.EnableAudioTrack(0, true);
            videoPlayer.SetTargetAudioSource(0, audioSource);
            videoPlayer.clip = videoClip;
            videoPlayer.Prepare();

            WaitForSeconds waitTime = new WaitForSeconds(1);
            while (!videoPlayer.isPrepared)
            {
                yield return waitTime;
                break;
            }

            rawImage.texture = videoPlayer.texture;
            totalTimeText.text = getFormattedTime(videoPlayer.clip.length);
        }

        public void Play()
        {
            videoPlayer.Play();
            audioSource.Play();
        }

        public void Pause()
        {
            videoPlayer.Pause();
            audioSource.Pause();
        }

        public void Stop()
        {
            videoPlayer.Stop();
            audioSource.Stop();
        }

        void Update()
        {
            if (videoPlayer != null)
            {
                currentTimeText.text = getFormattedTime(videoPlayer.time);
                timeSlider.value = (float)(videoPlayer.time / (int)videoPlayer.clip.length);
            }
        }

        public void SetVideoTimeBySliderClick()
        {
            float distanceFromPivot = Input.mousePosition.x - timeSlider.transform.position.x;
            float sliderWidth = timeSlider.GetComponent<RectTransform>().sizeDelta.x * UIUtility.Singleton.GetCanvasScaleFactor().x;

            if (videoPlayer != null)
            {
                videoPlayer.time = (distanceFromPivot / sliderWidth) * (int)videoPlayer.clip.length;
            }
        }

        private string getFormattedTime(double time)
        {
            int timeInt = (int)time;
            int minutes = timeInt / 60;
            int seconds = timeInt % 60;

            string minutesToString = minutes.ToString();

            if (minutes < 10)
            {
                minutesToString = "0" + minutesToString;
            }

            string secondsToString = seconds.ToString();

            if (seconds < 10)
            {
                secondsToString = "0" + secondsToString;
            }

            return minutesToString + ":" + secondsToString;
        }
    }
}

