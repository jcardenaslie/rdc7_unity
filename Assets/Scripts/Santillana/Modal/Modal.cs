using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDC.Modal
{
    public class Modal : MonoBehaviour
    {
        public string name;

        private FluxAnimation m_FluxAnimation;

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
        }
    }
}

