using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RDC.Animations
{
    public class AlterSize : MonoBehaviour 
    {
        public float totalTime = 1;
        public Vector2 finalSize;
        public UnityEvent onAnimationStart;
        public UnityEvent onAnimationEnd;

        public void Animate()
        {
            if (onAnimationStart != null)
            {
                onAnimationStart.Invoke();
            }

            RDC.Animations.UIAnimation.Singleton.StartAnimation(
                new RDC.Animations.UIAnimation.AlterSize(
                    gameObject,
                    totalTime,
                    delegate()
                    {
                        if(onAnimationEnd != null)
                        {
                            onAnimationEnd.Invoke();
                        }
                    },
                    finalSize
                )
            );
        }
    }
}

