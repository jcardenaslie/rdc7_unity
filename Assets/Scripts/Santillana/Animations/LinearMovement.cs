using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RDC.Animations
{
    public class LinearMovement : MonoBehaviour
    {
        public float totalTime = 1;
        public Vector3 endPosition;
        public UnityEvent onAnimationEnd;

        public void Animate()
        {
            RDC.Animations.UIAnimation.Singleton.StartAnimation(
                new RDC.Animations.UIAnimation.LinearMovement(
                    gameObject,
                    totalTime,
                    delegate()
                    {
                        if(onAnimationEnd != null)
                        {
                            onAnimationEnd.Invoke();
                        }
                    },
                    endPosition
                )
            );
        }
    }
}

