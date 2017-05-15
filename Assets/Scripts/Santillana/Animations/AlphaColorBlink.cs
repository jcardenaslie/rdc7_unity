using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlphaColorBlink : MonoBehaviour
{
    public float totalTime = 5;
    public float blinkSpeed = 3;
    public UnityEvent onAnimationEnd;

    public void Animate()
    {
        RDC.Animations.UIAnimation.Singleton.StartAnimation(
            new RDC.Animations.UIAnimation.BlinkAlphaColor(
                gameObject,
                totalTime,
                delegate
                {
                    if(onAnimationEnd != null)
                    {
                        onAnimationEnd.Invoke();
                    }
                },
                blinkSpeed
            )
        );
    }
}
