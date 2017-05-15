using UnityEngine;

namespace RDC.Window
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager singleton;

        public static UIManager Singleton
        {
            get
            {
                if(singleton == null)
                {
                    singleton = FindObjectOfType<UIManager>();
                }

                return singleton;
            }
        }

        public enum TransitionType
        {
            OnCloseStart,
            OnCloseEnd
        }

        public TransitionType transitionType = TransitionType.OnCloseStart;

        public int currentWindowIndex = 0;
        private UIWindow[] windowList;

        private int nextWindowIndex = -1;
        private UIWindow.UIWindowCallback m_UIWindowCloseEndCallback;

        void Start()
        {
            windowList = GetComponentsInChildren<UIWindow>();

            for(int i = 0; i < windowList.Length; i++)
            {
                windowList[i].Initialize();
                windowList[i].gameObject.SetActive(true);
                windowList[i].SetIndex(i);
            }

            windowList[0].prevButton.gameObject.SetActive(false);
            windowList[windowList.Length - 1].nextButton.gameObject.SetActive(false);
            windowList[windowList.Length - 1].finishButton.gameObject.SetActive(true);

            m_UIWindowCloseEndCallback = delegate (UIWindow m_UIWindow)
                {
                    windowList[nextWindowIndex].Open();
                    currentWindowIndex = nextWindowIndex;
                    m_UIWindow.onCloseEnd.Remove(m_UIWindowCloseEndCallback);
                };

            windowList[currentWindowIndex].Open();
        }

        public void GoTo(int windowIndex)
        {
            if (windowIndex != currentWindowIndex)
            {
                switch(transitionType)
                {
                    case TransitionType.OnCloseStart:
                        windowList[windowIndex].Open();
                        windowList[currentWindowIndex].Close();
                        currentWindowIndex = windowIndex;
                        break;

                    case TransitionType.OnCloseEnd:
                        nextWindowIndex = windowIndex;
                        windowList[currentWindowIndex].onCloseEnd.Add(m_UIWindowCloseEndCallback);
                        windowList[currentWindowIndex].Close();
                        break;
                }
            }
        }

        public void NotifyNotepadClosed()
        {
            if (currentWindowIndex < 0 || currentWindowIndex >= windowList.Length) return;
            windowList[currentWindowIndex].onNotepadClosed.Invoke();
        }
    }
}


