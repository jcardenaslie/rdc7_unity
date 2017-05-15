using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RDC.NavigationMenu
{
    public class Menu : MonoBehaviour
    {
        private bool isOpen = false;

        public delegate void OnClose();
        public OnClose onClose;
        public FluxAnimation[] fluxAnimations;
        public UnitList unitList;
        public UIButton toggleButton;

        public void Toggle()
        {
            if (!isOpen)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        public bool Open()
        {
            if (!isOpen)
            {
                for (int i = 0; i < fluxAnimations.Length; i++)
                {
                    fluxAnimations[i].StartOpeningAnimation();
                }

                unitList.Expand();    
                isOpen = true;

                return true;
            }

            return false;
        }

        public void Close()
        {
            if (isOpen)
            {
                for (int i = 0; i < fluxAnimations.Length; i++)
                {
                    fluxAnimations[i].StartClosingAnimation();
                }

                unitList.Collapse();

                isOpen = false;

                if (onClose != null)
                {
                    onClose();
                }
            }
        }
    }
}
