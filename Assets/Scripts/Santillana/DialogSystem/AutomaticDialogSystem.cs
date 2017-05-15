using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDC.Dialogs
{
    public class AutomaticDialogSystem : MonoBehaviour
    {
        private Dialog[] dialogArr;
        private int currentIndex;

        public void Initialize()
        {
            dialogArr = GetComponentsInChildren<Dialog>();

            for(int i = 0; i < dialogArr.Length; i++)
            {
                dialogArr[i].Initialize();

                if(dialogArr[i].autoOpen)
                {
                    dialogArr[i].onEndHideEvent = OnEndDialogCallback;
                }
            }
        }

        public void Init()
        {
            currentIndex = 0;
            StartDialogWithIndex(currentIndex);
        }

        public void Finish()
        {
            for(int i = 0; i < dialogArr.Length; i++)
            {
                dialogArr[i].Hide(false);
            }
        }

        private void StartDialogWithIndex(int index)
        {
            for(int i = 0; i < dialogArr.Length; i++)
            {
                if(dialogArr[i].showOrder == index && dialogArr[i].autoOpen)
                {
                    dialogArr[i].Show();
                    break;
                }
            }
        }

        private void OnEndDialogCallback()
        {
            StartDialogWithIndex(++currentIndex);
        }
    }
}

