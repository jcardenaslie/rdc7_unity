using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDC
{
    public class Notepad : MonoBehaviour 
    {
        private static Notepad singleton;
        private FluxAnimation m_FluxAnimation;

        public static Notepad Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = FindObjectOfType<Notepad>();
                }

                return singleton;
            }
        }

        void Start()
        {
            m_FluxAnimation = GetComponent<FluxAnimation>();
        }

        public void Open()
        {
            m_FluxAnimation.StartOpeningAnimation();
        }

        public void Close()
        {
            m_FluxAnimation.StartClosingAnimation();
            Window.UIManager.Singleton.NotifyNotepadClosed();
        }
    }
}

