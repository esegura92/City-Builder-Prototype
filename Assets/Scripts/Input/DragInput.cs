using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilder.Input
{
    public class DragInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        Dictionary<int, List<IDraggable>> draggedObjects;

        // Start is called before the first frame update
        private void Awake()
        {
            draggedObjects = new Dictionary<int, List<IDraggable>>();
        }

        private bool IsObjectUnderDrag(IDraggable draggable)
        {
            bool objectUnderDrag = false;
            foreach (KeyValuePair<int, List<IDraggable>> dragables in draggedObjects)
            {
                if (dragables.Value.Contains(draggable))
                    objectUnderDrag = true;
            }
            return objectUnderDrag;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            int actualPointersCount = draggedObjects.Count;
            if (actualPointersCount == 0)
            {
                List<IDraggable> selectedDraggables = GameInput.RaycastInteraction<IDraggable>(eventData.position);
                List<IDraggable> draggableListTarget = null;
                if (draggedObjects.ContainsKey(eventData.pointerId))
                {
                    draggableListTarget = draggedObjects[eventData.pointerId];
                } // end if
                else
                {
                    draggableListTarget = new List<IDraggable>();
                    draggedObjects.Add(eventData.pointerId, draggableListTarget);
                } // end if

                foreach (IDraggable draggable in selectedDraggables)
                {

                    if (!IsObjectUnderDrag(draggable))
                    {
                        draggableListTarget.Add(draggable);
                        draggable.BeginDrag(eventData.position);
                    } // end if
                } // end foreach
            } // end if
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (draggedObjects.ContainsKey(eventData.pointerId))
            {
                List<IDraggable> draggables = draggedObjects[eventData.pointerId];
                foreach (IDraggable draggable in draggables)
                {
                    draggable.Drag(eventData.position);
                } // end foreach
            } // end if
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (draggedObjects.ContainsKey(eventData.pointerId))
            {
                List<IDraggable> draggables = draggedObjects[eventData.pointerId];
                foreach (IDraggable draggable in draggables)
                {
                    draggable.EndDrag(eventData.position);
                } // end foreach
                draggables.Clear();
                draggedObjects.Remove(eventData.pointerId);
            } // end if
        }
    }
}

