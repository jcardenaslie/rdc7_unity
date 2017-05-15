using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDC.Window
{
    public class UIElement : MonoBehaviour 
    {
        public bool ignoreActivate = false;

        public virtual void OnWindowActivate()
        {
            
        }
    }
}
