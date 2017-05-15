using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RDC
{
    public class UIUtility : MonoBehaviour 
    {
        private static UIUtility singleton;

        public static UIUtility Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = FindObjectOfType<UIUtility>();
                }

                return singleton;
            }
        }

        public Vector2 GetCorrectedMousePosition()
        {
            return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        public Vector2 GetCanvasScaleFactor()
        {
            Rect rect = GetComponent<RectTransform>().rect;
            float canvasScaleFactorX = Screen.width / rect.width;
            float canvasScaleFactorY = Screen.height / rect.height;

            return new Vector2(canvasScaleFactorX, canvasScaleFactorY);
        }

        public bool Contains(Transform target, Vector2 point)
        {
            Rect rect = GetComponent<RectTransform>().rect;
            Rect targetRect = target.GetComponent<RectTransform>().rect;

            float canvasScaleFactorX = Screen.width / rect.width;
            float canvasScaleFactorY = Screen.height / rect.height;
            float width = targetRect.width * canvasScaleFactorX;
            float height = targetRect.height * canvasScaleFactorY;
            float width_2 = width * 0.5f;
            float height_2 = height * 0.5f;

            bool topLeft = (point.x >= (target.position.x - width_2)) && (point.y >= (target.position.y - height_2));
            bool bottomRight = (point.x <= (target.position.x + width_2)) && (point.y <= (target.position.y + height_2));

            return topLeft && bottomRight;
        }
    }
}


