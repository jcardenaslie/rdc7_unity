using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace RDC
{
    public class UIDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 deltaMouseAndTransform;
        private Vector3 startPosition;
        private bool bDragEnabled = true;

        public float restoreMovementSpeed = 0.4f;
        public bool allowMultipleDrags = false;
        public GameObject[] objectiveList;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (bDragEnabled)
            {
                deltaMouseAndTransform = Input.mousePosition - transform.position;
                startPosition = transform.position;

                OnBeginDrag();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (bDragEnabled)
            {
                transform.position = Input.mousePosition - deltaMouseAndTransform;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (bDragEnabled)
            {
                GameObject objectiveTargetted = null;

                bDragEnabled = false;

                for (int i = 0; i < objectiveList.Length; i++)
                {
                    if (UIUtility.Singleton.Contains(objectiveList[i].transform, UIUtility.Singleton.GetCorrectedMousePosition()))
                    {
                        objectiveTargetted = objectiveList[i];
                        break;
                    }
                }

                if (objectiveTargetted != null)
                {
                    this.transform.position = objectiveTargetted.transform.position;
                    OnDragEndCallback(gameObject, objectiveTargetted);

                    if (allowMultipleDrags)
                    {
                        bDragEnabled = true;
                    }
                }
                else
                {
                    RestorePosition();
                }
            }
        }

        protected void RestorePosition()
        {
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
            
        }

        public virtual void OnBeginDrag()
        {

        }

        protected void SetStartPosition(Vector3 newStartPosition)
        {
            startPosition = newStartPosition;
        }
    }
}


