using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RDC
{
    public class UIButton : 
    RDC.Window.UIElement, 
    IPointerClickHandler, 
    IPointerEnterHandler, 
    IPointerExitHandler
    {
        private Image normal;
        private Sprite startSprite;
        private AudioSource m_AudioSource;

        public Image hover;
        public Image tooltip;
        public bool blinkAtWindowActive = true;
        public bool showTooltip = true;
        public float blinkTime = 3;
        public float blinkSpeed = 3;
        public Sprite switchSpriteOnClick;
        public AudioClip onClickAudio;
        public UnityEvent onPointerClick;

        void Awake()
        {
            normal = GetComponent<Image>();
            startSprite = normal.sprite;
            m_AudioSource = GetComponent<AudioSource>();
            SetNormalState();
        }

        public void SwitchSprite()
        {
            if(switchSpriteOnClick != null)
            {
                if(normal.sprite == switchSpriteOnClick)
                {
                    normal.sprite = startSprite;
                }
                else
                {
                    normal.sprite = switchSpriteOnClick;
                }
            }

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SwitchSprite();
            PlayOnClickAudio();

            if(onPointerClick != null)
            {
                onPointerClick.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetHighlightState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetNormalState();
        }

        private void SetNormalState()
        {
            if(hover != null && hover.sprite != null)
            {
                Color tmpColor = hover.color;
                tmpColor.a = 0;
                hover.color = tmpColor;
            }

            tooltip.gameObject.SetActive(false);
        }

        private void SetHighlightState()
        {
            if(hover != null && hover.sprite != null)
            {
                Color tmpColor = hover.color;
                tmpColor.a = 1;
                hover.color = tmpColor;
            }

            if(showTooltip)
            {
                tooltip.gameObject.SetActive(true);
            }
        }

        public override void OnWindowActivate()
        {
            base.OnWindowActivate();

            if (blinkAtWindowActive)
            {
                Animations.UIAnimation.Singleton.StartAnimation(new Animations.UIAnimation.BlinkAlphaColor(hover.gameObject, blinkTime, null, blinkSpeed));
            }
        }

        private void PlayOnClickAudio()
        {
            if (onClickAudio != null && m_AudioSource != null)
            {
                m_AudioSource.clip = onClickAudio;
                m_AudioSource.Play();
            }
        }
    }
}


