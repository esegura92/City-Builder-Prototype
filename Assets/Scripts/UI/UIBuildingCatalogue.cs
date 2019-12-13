using CityBuilder.Data;
using CityBuilder.GameEntities;
using CityBuilder.GameModes;
using CityBuilder.Input;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.UI
{
    /// <summary>
    /// Building catalogue class, storage a list of building items and display it on the screen
    /// </summary>
    public class UIBuildingCatalogue : MonoBehaviour
    {
        /// <summary>
        /// Const path to the buildings data list
        /// </summary>
        private const string PathToBuildingsDatas = "Datas/Buildings";
        /// <summary>
        /// Parent of the catalogue items
        /// </summary>
        [SerializeField]
        RectTransform itemsParent;
        /// <summary>
        /// Reference to the game grid to make so calculation operations
        /// </summary>
        private CityGrid grid;
        /// <summary>
        /// Reference prefab to instantiate catalogue items
        /// </summary>
        [SerializeField]
        private GameObject catalogueItemPrefab;
        /// <summary>
        /// Reference to the game mode
        /// </summary>
        [SerializeField]
        private BuildGameMode gameMode;
        /// <summary>
        /// Building items list
        /// </summary>
        private List<UICatalogueItem> items;
        /// <summary>
        /// Reference to the main camera to calculate some positions
        /// </summary>
        private Camera mainCamera;
        /// <summary>
        /// Initialize elements like the list and the main camera
        /// </summary>
        private void Awake()
        {
            items = new List<UICatalogueItem>();
            mainCamera = Camera.main;
        }

        public void Initialize(BuildGameMode mode)
        {
            this.gameMode = mode;
            grid = mode.Controller.GameGrid;
            BuildCatalogue();
        }
        /// <summary>
        /// Builds the entire catalog
        /// </summary>
        private void BuildCatalogue()
        {
            CityBuildingData[] availableBuildings = UnityEngine.Resources.LoadAll<CityBuildingData>(PathToBuildingsDatas);
            for(int i = 0; i < availableBuildings.Length; i++)
            {
                CreateCatalogueItem(availableBuildings[i]);
            } // end for
        }
        /// <summary>
        /// Creates an catalogue item game object and adds his comonent to the items list
        /// </summary>
        /// <param name="buildingData"></param>
        private void CreateCatalogueItem(CityBuildingData buildingData)
        {
            GameObject catalogueItemInstance = Instantiate(catalogueItemPrefab);
            UICatalogueItem catalogueItem = catalogueItemInstance.GetComponent<UICatalogueItem>();
            catalogueItem.Initialize(buildingData, this);
            catalogueItemInstance.transform.SetParent(itemsParent, false);
            items.Add(catalogueItem);
        }
        /// <summary>
        /// Calls the building creator and set the position on the grid, then reset the
        /// building catalogue item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="eventPosition"></param>
        public void CreateBuilding(UICatalogueItem item, Vector3 eventPosition)
        {
            gameMode.CreateBuilding(item.BuildingData, eventPosition);
        }

        /// <summary>
        /// /// Calculate the grid position based on the camera position that we receive,
        /// because we are in a 3d enviroment is fundamental to do this
        /// </summary>
        /// <param name="eventPosition">Position on the time of th event</param>
        /// <returns>Grid nearest position</returns>
        public Vector3 CalculateGridPosition(Vector3 eventPosition)
        {
            Vector3 gridPos = GameInput.CalculatePositionInGame(eventPosition);
            
            return grid.GetNearestTilePosition(gridPos);
        }

    }
}


