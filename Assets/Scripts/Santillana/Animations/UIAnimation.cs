using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RDC.Animations
{
    public class UIAnimation : MonoBehaviour 
    {
        private static UIAnimation singleton;

        public static UIAnimation Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = FindObjectOfType<UIAnimation>();
                }

                return singleton;
            }
        }

        public delegate void OnAnimationEnd();

        private List<AnimationObject> animationObjectList = new List<AnimationObject>();

        public class AnimationObject
        {
            protected float totalTime;
            protected float currentTime;
            protected OnAnimationEnd onAnimationEndCallback;
            protected GameObject target;

            public AnimationObject(GameObject target, float totalTime, OnAnimationEnd onAnimationEndCallback = null)
            {
                this.target = target;
                this.totalTime = totalTime;
                this.onAnimationEndCallback = onAnimationEndCallback;
                this.currentTime = 0;
            }

            protected virtual void ProcessEquation(float t)
            {

            }

            protected virtual void EndAnimation()
            {

            }

            protected virtual float CalculateT()
            {
                return currentTime / totalTime;
            }

            public bool UpdateAnimation()
            {
                currentTime += Time.deltaTime;

                float t = CalculateT();

                if (t >= 1)
                {
                    ProcessEquation(1);
                    EndAnimation();

                    if (onAnimationEndCallback != null)
                    {
                        onAnimationEndCallback();
                    }

                    return false;
                }
                else
                {
                    ProcessEquation(t);
                }

                return true;
            }
        }

        public class LinearMovement : AnimationObject
        {
            private Vector3 startPosition;
            private Vector3 endPosition;
            private bool bBasedOnDistance = false;

            public LinearMovement(GameObject target, float totalTime, OnAnimationEnd onAnimationEndCallback, Vector3 endPosition, bool bBasedOnDistance = false)
                : base(target, totalTime, onAnimationEndCallback)
            {
                this.startPosition = target.transform.position;
                this.endPosition = endPosition;
                this.bBasedOnDistance = bBasedOnDistance;

                if(bBasedOnDistance)
                {
                    this.totalTime *= Vector3.Distance(this.startPosition, this.endPosition) / 1000.0f;
                }
            }

            protected override void ProcessEquation(float t)
            {
                base.ProcessEquation(t);

                float newX = startPosition.x * (1 - t) + endPosition.x * t;
                float newY = startPosition.y * (1 - t) + endPosition.y * t;
                float newZ = startPosition.z * (1 - t) + endPosition.z * t;

                target.transform.position = new Vector3(newX, newY, newZ);
            }
        }

        public class BlinkAlphaColor : AnimationObject
        {
            private Image image;
            private float incrementalFactor;
            private float blinkSpeed;
            private float startingAlpha;

            public BlinkAlphaColor(GameObject target, float totalTime, OnAnimationEnd onAnimationEndCallback, float blinkSpeed = 3)
                : base(target, totalTime, onAnimationEndCallback)
            {
                image = target.GetComponent<Image>();
                incrementalFactor = 0;
                startingAlpha = image.color.a;
                this.blinkSpeed = blinkSpeed;
            }

            protected override void ProcessEquation(float t)
            {
                base.ProcessEquation(t);
                incrementalFactor += Time.deltaTime;
                Color tmpColor = image.color;
                tmpColor.a = Mathf.Abs(Mathf.Sin(incrementalFactor * blinkSpeed));
                image.color = tmpColor;
            }

            protected override void EndAnimation()
            {
                base.EndAnimation();
                Color tmpColor = image.color;
                tmpColor.a = startingAlpha;
                image.color = tmpColor;
            }
        }

        public class AlterSize : AnimationObject
        {
            private Vector2 startSize;
            private Vector2 finalSize;
            private RectTransform rectTransform;

            public AlterSize(GameObject target, float totalTime, OnAnimationEnd onAnimationEndCallback, Vector2 finalSize)
                : base(target, totalTime, onAnimationEndCallback)
            {
                this.rectTransform = target.GetComponent<RectTransform>();
                this.startSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
                this.finalSize = finalSize;
            }

            protected override void ProcessEquation(float t)
            {
                base.ProcessEquation(t);

                float newWidth = startSize.x * (1 - t) + finalSize.x * t;
                float newHeight = startSize.y * (1 - t) + finalSize.y * t;

                rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
            }
        }

        public void StartAnimation(AnimationObject newAnimationObject)
        {
            animationObjectList.Add(newAnimationObject);
        }

        public void Update()
        {
            List<AnimationObject> dropList = new List<AnimationObject>();

            foreach (AnimationObject animObject in animationObjectList)
            {
                if (!animObject.UpdateAnimation())
                {
                    dropList.Add(animObject);
                }
            }

            for (int i = 0; i < dropList.Count; i++)
            {
                animationObjectList.Remove(dropList[i]);
            }
        }
    }

}

