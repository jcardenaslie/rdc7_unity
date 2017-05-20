using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

namespace RDC
{
    public class UIDraggableV2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 deltaMouseAndTransform;
        private Vector3 startPosition;
        private bool bDragEnabled = true;

        public float restoreMovementSpeed = 0.4f;
        public bool allowMultipleDrags = false;
        public GameObject[] objectiveList;

        // al comenzar el drag setea la posicion inicial del objeto, la cual es en donde fue agarrado
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            if (bDragEnabled)
            {
                Debug.Log("OnBeginDrag: drag enabled");
                deltaMouseAndTransform = Input.mousePosition - transform.position;
                startPosition = transform.position;

                //OnBeginDrag();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            if (bDragEnabled)
            {
                Debug.Log("OnDrag: drag enabled");
                transform.position = Input.mousePosition - deltaMouseAndTransform;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            if (bDragEnabled)
            {
                Debug.Log("OnEndDrag: drag enabled");

                GameObject objectiveTargetted = null;

                bDragEnabled = false;

                for (int i = 0; i < objectiveList.Length; i++)
                {
                    if (UIUtility.Singleton.Contains(objectiveList[i].transform, UIUtility.Singleton.GetCorrectedMousePosition()))
                    {
                        Debug.Log("OnEndDrag: utility");
                        
                        objectiveTargetted = objectiveList[i];

                        PintarObjetos(objectiveTargetted);
                        

                        break;
                    }
                }

                if (objectiveTargetted != null)
                {
                    Debug.Log("OnEndDrag: has target");

                    this.transform.position = objectiveTargetted.transform.position;
                    OnDragEndCallback(gameObject, objectiveTargetted);

                    if (allowMultipleDrags)
                    {
                        Debug.Log("OnEndDrag: has target: multipleDrag");

                        bDragEnabled = true;
                    }
                }
                else
                {
                    Debug.Log("OnEndDrag: restore position");
                    RestorePosition();
                }
            }
        }

        private void PintarObjetos(GameObject obj)
        {
            Image[] objetoPintable = obj.GetComponent<ColorContainer>().GetImagenPintable();
            
            for (int i = 0; i < objetoPintable.Length; i++) {
                objetoPintable[i].color = GetComponent<ItemColor>().GetColor();
            }
        }

        protected void RestorePosition()
        {
            Debug.Log("RestorePosition");

            Animations.UIAnimation.Singleton.StartAnimation(
                new Animations.UIAnimation.LinearMovement(
                    this.gameObject, 
                    restoreMovementSpeed,
                    delegate 
                    {
                        bDragEnabled = true;
                        OnDragEndCallback(gameObject, null);
                    },
                    startPosition,
                    true)
            );
        }

        public virtual void OnDragEndCallback(GameObject target, GameObject objectiveTargetted)
        {
            Debug.Log("OnEndDragCallback");

        }

        public virtual void OnBeginDrag()
        {
            Debug.Log("Virtual OnBegin Drag");

        }

        protected void SetStartPosition(Vector3 newStartPosition)
        {
            Debug.Log("SetStartPosition");

            startPosition = newStartPosition;
        }
    }
}


