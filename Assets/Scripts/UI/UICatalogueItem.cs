using CityBuilder.Data;
using CityBuilder.Resources;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CityBuilder.UI
{
    /// <summary>
    /// An UI cataloge item contains reference to a specific building, also has functions to drag
    /// and place the building in the game
    /// </summary>
    public class UICatalogueItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField]
        private Text buildingName;
        [SerializeField]
        private Text goldText;
        [SerializeField]
        private Text woodText;
        [SerializeField]
        private Text steelText;
        /// <summary>
        /// Catalogue owner of this item
        /// </summary>
        private UIBuildingCatalogue owner;
        /// <summary>
        /// Linked building data of this catalogue item
        /// </summary>
        private CityBuildingData data;
        /// <summary>
        /// Rect that handles the visual building position
        /// </summary>
        private Transform visualReferenceRect;
        /// <summary>
        /// Visual reference of the building
        /// </summary>
        private GameObject visualReference;

        public CityBuildingData BuildingData
        {
            get { return data; }
        }

        /// <summary>
        /// Set the building data and owner of the catalogue item
        /// </summary>
        /// <param name="buildingData">Building data linked to the item</param>
        /// <param name="owner">Onwer catalogue of the item</param>
        public void Initialize(CityBuildingData buildingData, UIBuildingCatalogue owner)
        {
            data = buildingData;
            this.owner = owner;
            buildingName.text = buildingData.Name;
            for(int i = 0; i < buildingData.Cost.Count; i++)
            {
                EnableResourceText(buildingData.Cost[i].Type, buildingData.Cost[i].Amount);
            } // end for
        }
        

        private void EnableResourceText(ResourceType type, int cost)
        {
            switch(type)
            {
                case ResourceType.Gold:
                    {
                        goldText.text = cost.ToString();
                        goldText.transform.parent.gameObject.SetActive(true);
                    }
                    break;
                case ResourceType.Wood:
                    {
                        woodText.text = cost.ToString();
                        woodText.transform.parent.gameObject.SetActive(true);
                    }
                    break;
                case ResourceType.Steel:
                    {
                        steelText.text = cost.ToString();
                        steelText.transform.parent.gameObject.SetActive(true);
                    }
                    break;

            }
        }
        /// <summary>
        /// Implementation of the begin drag event, instantiates a visual building for
        /// visual reference on where to place it and initializes the buildin shape rect
        /// that handles the building position
        /// </summary>
        /// <param name="eventData">Relevant data of the event</param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            visualReference = Instantiate(data.VisualPrefab);
            visualReferenceRect = visualReference.transform;
            
            visualReferenceRect.position = owner.CalculateGridPosition(eventData.position);
        }
        /// <summary>
        /// Implementation of the drag event, set the position of the item to the 
        /// nearest tile position on the grid
        /// </summary>
        /// <param name="eventData">Relevant data of the event</param>
        public void OnDrag(PointerEventData eventData)
        {
            // calculate if trying to build a building or move the catalog if move the catalog change to drag of the owner
            visualReferenceRect.position = owner.CalculateGridPosition(eventData.position);
        }

        /// <summary>
        /// Implementation of the End drag event, creates the building and reset the item
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            //catalogue build building
            owner.CreateBuilding(this, eventData.position);
            Reset();
        }
        /// <summary>
        /// Implementation of the pointer click event, does nothing at the moment
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Reset to the default values of the item
        /// </summary>
        public void Reset()
        {
            Destroy(visualReference);
            visualReferenceRect = null;
        }
    }
}


