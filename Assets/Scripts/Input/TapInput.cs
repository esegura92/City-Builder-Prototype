using CityBuilder.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilder.Input
{
    public class TapInput : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
    {
        [SerializeField]
        private GameController controller;
        public void OnPointerClick(PointerEventData eventData)
        {
            List<ITappable> tappableObjects = GameInput.RaycastInteraction<ITappable>(eventData.position);
            foreach (ITappable tappable in tappableObjects)
            {
                tappable.PointerClick();
            } // end foreach
            if(tappableObjects.Count == 0)
            {
                controller.CurrentGameMode.SetSelectedBuilding(null);
            } // end if
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            List<ITappable> tappableObjects = GameInput.RaycastInteraction<ITappable>(eventData.position);
            foreach (ITappable tappable in tappableObjects)
            {
                tappable.PointerDown();
            } // end foreach
        }
    }
}


