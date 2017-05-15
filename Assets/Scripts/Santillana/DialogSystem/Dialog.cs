using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RDC.Dialogs
{
    public class Dialog : MonoBehaviour
    {
        public int showOrder = 0;
        public float duration = 3;
        public bool hideDialog = true;
        public bool autoOpen = true;
        public AudioClip audio;
        public UnityEvent OnDialogEnd; 
        public delegate void OnEndShowEvent();
        public delegate void OnEndHideEvent();
        public OnEndShowEvent onEndShowEvent;
        public OnEndHideEvent onEndHideEvent;

        private FluxAnimation fluxAnimation;
        private AudioSource m_AudioSource;
        private bool callOnEndHideEvent = true;

        public void Initialize()
        {
            fluxAnimation = GetComponent<FluxAnimation>();
            fluxAnimation.onOpenEnd.AddListener(OnEndShowCallback);
            fluxAnimation.onCloseEnd.AddListener(OnEndHideCallback);

            m_AudioSource = GetComponent<AudioSource>();

            if (audio != null)
            {
                duration = audio.length;
            }
        }

        public void Show()
        {
            fluxAnimation.StartOpeningAnimation();
            PlayAudio();
        }

        public void Hide(bool callOnEndHideEvent = true)
        {
            CancelInvoke();
            this.callOnEndHideEvent = callOnEndHideEvent;
            fluxAnimation.StartClosingAnimation();
        }

        private void InternalHide()
        {
            Hide();
        }

        private void OnEndShowCallback()
        {
            if(onEndShowEvent != null)
            {
                onEndShowEvent();
            }

            if (hideDialog)
            {
                Invoke("InternalHide", duration);
            }
            else
            {
                OnEndHideCallback();
            }
        }

        private void OnEndHideCallback()
        {
            if (callOnEndHideEvent)
            {
                if(onEndHideEvent != null)
                {
                    onEndHideEvent();
                }

                if (OnDialogEnd != null)
                {
                    OnDialogEnd.Invoke();
                }
            }
        }

        private void PlayAudio()
        {
            if(this.audio != null && m_AudioSource != null)
            {
                m_AudioSource.clip = this.audio;
                m_AudioSource.Play();
            }
        }
    }
}


