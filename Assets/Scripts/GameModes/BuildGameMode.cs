using System.Collections;
using System.Collections.Generic;
using CityBuilder.Controllers;
using CityBuilder.Data;
using CityBuilder.GameEntities;
using CityBuilder.Input;
using CityBuilder.Resources;
using CityBuilder.UI;
using UnityEngine;

namespace CityBuilder.GameModes
{
    public class BuildGameMode : BaseGameMode
    {
        
        private UIBuildingCatalogue catalogue;
        public override void Initialize(GameController gameController)
        {
            base.Initialize(gameController);
            SetGameModeUI();
            AddGameModeListeners();
        }
        // Start is called before the first frame update
        public override void AddGameModeListeners()
        {
            //throw new System.NotImplementedException();
        }

        public override void RemoveGameModeListeners()
        {
            //throw new System.NotImplementedException();
        }

        public override void SetGameModeUI()
        {
            GameObject buildingCatalogueInstance = Instantiate(owner.CataloguePrefab);
            buildingCatalogueInstance.transform.SetParent(owner.MainCanvas.GetComponent<RectTransform>(), false);
            catalogue = buildingCatalogueInstance.GetComponent<UIBuildingCatalogue>();
            catalogue.Initialize(this);

        }

        private void OnDestroy()
        {
            RemoveGameModeListeners();
            Destroy(catalogue.gameObject);
        }

        public bool CreateBuilding(CityBuildingData buildingData, Vector3 position)
        {
            bool result = false;
            Vector3 worldPos = GameInput.CalculatePositionInGame(position);
            if(CanAffordBuilding(buildingData.Cost) && CanPlaceBuilding(worldPos, buildingData.Width, buildingData.Height))
            {
                GameObject buildingInstance = Instantiate(owner.BuildingPrefab, gamePlayer.transform);
                Instantiate(buildingData.VisualPrefab, buildingInstance.transform);
                CityBuilding building = buildingInstance.GetComponent<CityBuilding>();
                building.Initialize(buildingData, gamePlayer, position);
                result = true;
            } // end if
            return result;
        }

        public bool CanAffordBuilding(List<ResourceAmount> cost)
        {
            bool canAfford = true;

            for(int i = 0; i < cost.Count && canAfford; i++)
            {
                int amount = gamePlayer.GetResourceAmount(cost[i].Type);
                if (amount < cost[i].Amount)
                {
                    canAfford = false;
                } // end if
            } // end for
            return canAfford;
        }

        public bool CanPlaceBuilding(Vector3 buildingPosition, int width, int height)
        {

            return owner.GameGrid.IsGridSpaceAvailable(buildingPosition, width, height);
        }

        public override void OnBuildingSelected(CityBuilding cityBuilding)
        {
            //throw new System.NotImplementedException();
        }

        public override void OnBuildingDiselected(CityBuilding cityBuilding)
        {
            //throw new System.NotImplementedException();
        }
    }
}


