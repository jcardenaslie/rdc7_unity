using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RDC.Window
{
    public class UIFadeTransition : MonoBehaviour
    {
        private Image m_Image;
        private float fMaxTime = 2;
        private float fCurrentTime = 0;
        private bool bPlayingTransition = false;
        private FadeTransitionType currentTransitionType;
        private FadeState currentFadeState = FadeState.FadeOut;

        public enum FadeTransitionType
        {
            FadeIn,
            FadeOut
        }

        public enum FadeState
        {
            FadeIn,
            FadeOut
        }

        void Start()
        {
            m_Image = GetComponent<Image>();

            if(currentFadeState == FadeState.FadeOut)
            {
                m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 0);
            }
        }

        public void SetMaxTransitionTime(float maxTransitionTime)
        {
            fMaxTime = Random.Range(0.5f, maxTransitionTime);
        }

        public void FadeIn()
        {
            fCurrentTime = 0;
            bPlayingTransition = true;
            currentTransitionType = FadeTransitionType.FadeIn;
        }

        public void FadeOut()
        {
            fCurrentTime = 0;
            bPlayingTransition = true;
            currentTransitionType = FadeTransitionType.FadeOut;
        }

        void Update()
        {
            if(bPlayingTransition)
            {
                fCurrentTime += Time.deltaTime;
                float t = fCurrentTime / fMaxTime;

                if(t > 1)
                {
                    bPlayingTransition = false;
                }
                else
                {
                    switch(currentTransitionType)
                    {
                        case FadeTransitionType.FadeIn:
                            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, t);
                            break;

                        case FadeTransitionType.FadeOut:
                            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 1 - t);
                            break;
                    }
                }
            }
        }
    }
}


