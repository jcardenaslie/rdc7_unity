using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace RDC.Window
{
    public class UIWindow : MonoBehaviour
    {
        public delegate void UIWindowCallback(UIWindow m_UIWindow);
        public List<UIWindowCallback> onOpenStart = new List<UIWindowCallback>();
        public List<UIWindowCallback> onOpenEnd = new List<UIWindowCallback>();
        public List<UIWindowCallback> onCloseStart = new List<UIWindowCallback>();
        public List<UIWindowCallback> onCloseEnd = new List<UIWindowCallback>();
        public FluxAnimation[] m_FluxAnimationObjectsOpenStart;
        public FluxAnimation[] m_FluxAnimationObjectsOpenEnd;
        public FluxAnimation[] m_FluxAnimationObjectsCloseStart;
        public FluxAnimation[] m_FluxAnimationObjectsCloseEnd;
        public bool isActive = false;
        public int index = 0;
        public Button nextButton;
        public Button prevButton;
        public Button finishButton;
        public UnityEvent onOpen;
        public UnityEvent onClose;
        public UnityEvent onNotepadClosed;

        private FluxAnimation m_FluxAnimation;
        private Image[] imageArr;
        private UIElement[] uiElementArr;
        private UIActivate m_UIActivate;
        private RDC.Modal.ModalManager m_ModalManager;
        private RDC.Dialogs.AutomaticDialogSystem m_AutomaticDialogSystem;

        public void Initialize()
        {
            m_FluxAnimation = GetComponent<FluxAnimation>();
            m_FluxAnimation.onOpenStart.AddListener(OnOpenStart);
            m_FluxAnimation.onOpenEnd.AddListener(OnOpenEnd);
            m_FluxAnimation.onCloseStart.AddListener(OnCloseStart);
            m_FluxAnimation.onCloseEnd.AddListener(OnCloseEnd);

            m_UIActivate = GetComponentInChildren<UIActivate>();
            m_UIActivate.Initialize();

            m_AutomaticDialogSystem = GetComponent<RDC.Dialogs.AutomaticDialogSystem>();
            m_AutomaticDialogSystem.Initialize();

            m_ModalManager = GetComponent<RDC.Modal.ModalManager>();
            m_ModalManager.Initialize();

            imageArr = GetComponentsInChildren<Image>();
            uiElementArr = GetComponentsInChildren<UIElement>();

            MoveWindowAway();

            StartCallback();
        }

        public void SetIndex(int index)
        {
            this.index = index;

            prevButton.onClick.AddListener(delegate
                {
                    UIManager.Singleton.GoTo(index - 1);
                });

            nextButton.onClick.AddListener(delegate
                {
                    UIManager.Singleton.GoTo(index + 1);
                });
        }

        public virtual void OnOpenStart()
        {
            for (int i = 0; i < m_FluxAnimationObjectsOpenStart.Length; i++)
            {
                m_FluxAnimationObjectsOpenStart[i].StartOpeningAnimation();
            }

            for (int i = 0; i < onOpenStart.Count; i++)
            {
                onOpenStart[i](this);
            }
        }

        public virtual void OnOpenEnd()
        {
            for (int i = 0; i < m_FluxAnimationObjectsOpenEnd.Length; i++)
            {
                m_FluxAnimationObjectsOpenEnd[i].StartOpeningAnimation();
            }

            for (int i = 0; i < onOpenEnd.Count; i++)
            {
                onOpenEnd[i](this);
            }

            if(m_AutomaticDialogSystem != null)
            {
                m_AutomaticDialogSystem.Init();
            }

            for (int i = 0; i < uiElementArr.Length; i++)
            {
                uiElementArr[i].OnWindowActivate();
            }

            if(onOpen != null)
            {
                onOpen.Invoke();
            }

            if(onClose != null)
            {
                onOpen.Invoke();
            }
        }

        public virtual void OnCloseStart()
        {
            for (int i = 0; i < m_FluxAnimationObjectsCloseStart.Length; i++)
            {
                m_FluxAnimationObjectsCloseStart[i].StartClosingAnimation();
            }

            for (int i = 0; i < onCloseStart.Count; i++)
            {
                onCloseStart[i](this);
            }
        }

        public virtual void OnCloseEnd()
        {
            for (int i = 0; i < m_FluxAnimationObjectsCloseEnd.Length; i++)
            {
                m_FluxAnimationObjectsCloseEnd[i].StartClosingAnimation();
            }

            for (int i = 0; i < onCloseEnd.Count; i++)
            {
                onCloseEnd[i](this);
            }

            MoveWindowAway();
        }

        public virtual void Open()
        {
            ResetPositionToOrigin();

            if(m_FluxAnimation == null)
            {
                m_FluxAnimation = GetComponent<FluxAnimation>();
            }

            if(m_FluxAnimation != null)
            {
                m_FluxAnimation.StartOpeningAnimation();
            }

            if(m_UIActivate != null)
            {
                m_UIActivate.Activate();
            }
            else
            {
                Debug.Log("m_UIActivate is null");
            }

            isActive = true;
        }

        public virtual void Close()
        {
            m_FluxAnimation.StartClosingAnimation();

            if (m_UIActivate != null)
            {
                m_UIActivate.Deactivate();
            }

            if (m_AutomaticDialogSystem != null)
            {
                m_AutomaticDialogSystem.Finish();
            }

            isActive = false;
        }

        private void ActivateRaycastTarget()
        {
            for (int i = 0; i < imageArr.Length; i++)
            {
                imageArr[i].raycastTarget = true;
            }
        }

        private void DeactivateRaycastTarget()
        {
            for (int i = 0; i < imageArr.Length; i++)
            {
                imageArr[i].raycastTarget = false;
            }
        }

        private void MoveWindowAway()
        {
            transform.position = new Vector3(9999, 9999, 0);
        }

        private void ResetPositionToOrigin()
        {
            transform.position = new Vector3(0, 0, 0);
        }

        public virtual void UpdateCallback()
        {

        }

        public virtual void StartCallback()
        {

        }

        public void OpenModal(string name)
        {
            m_ModalManager.OpenModal(name);
        }

        void Update()
        {
            if(isActive)
            {
                UpdateCallback();
            }
        }
    }

}

