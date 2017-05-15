using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDC.Modal
{
    public class ModalManager : MonoBehaviour 
    {
        private Dictionary<string, Modal> modalHashMap;

        public void Initialize()
        {
            modalHashMap = new Dictionary<string, Modal>();
            Modal[] modalArr = GetComponentsInChildren<Modal>();

            for (int i = 0; i < modalArr.Length; i++)
            {
                modalHashMap.Add(modalArr[i].name, modalArr[i]);
            }
        }

        public void OpenModal(string name)
        {
            if (modalHashMap.ContainsKey(name))
            {
                modalHashMap[name].Open();
            }
        }
    }
}
