using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RDC.Window
{
    public class UIActivate : MonoBehaviour
    {
        private UIFadeTransition[] transitionElementsArr;
        public float maxTransitionTime = 3;

        public void Initialize()
        {
            Image[] imageArr = GetComponentsInChildren<Image>();
            transitionElementsArr = new UIFadeTransition[imageArr.Length];

            for(int i = 0; i < imageArr.Length; i++)
            {
                UIElement currentElement = imageArr[i].gameObject.GetComponent<UIElement>();

                if (currentElement != null && currentElement.ignoreActivate)
                {
                    continue;
                }

                transitionElementsArr[i] = imageArr[i].gameObject.AddComponent<UIFadeTransition>();
                transitionElementsArr[i].SetMaxTransitionTime(maxTransitionTime);
            }
        }

        public void Activate()
        {
            for(int i = 0; i < transitionElementsArr.Length; i++)
            {
                if (transitionElementsArr[i] != null)
                {
                    transitionElementsArr[i].FadeIn();
                }
            }
        }

        public void Deactivate()
        {
            for (int i = 0; i < transitionElementsArr.Length; i++)
            {
                if (transitionElementsArr[i] != null)
                {
                    transitionElementsArr[i].FadeOut();
                }
            }
        }
    }
}

