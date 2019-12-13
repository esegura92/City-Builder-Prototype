using System.Collections;
using System.Collections.Generic;
using CityBuilder.Controllers;
using CityBuilder.GameEntities;
using CityBuilder.GameEntities.Modules;
using CityBuilder.UI;
using UnityEngine;

namespace CityBuilder.GameModes
{
    public class RegularGameMode : BaseGameMode
    {
        private UIProductionProgressBarButton progressBar;
        public override void Initialize(GameController gameController)
        {
            base.Initialize(gameController);
            SetGameModeUI();
            AddGameModeListeners();
        }

        public override void AddGameModeListeners()
        {
            //throw new System.NotImplementedException();
        }

        public override void RemoveGameModeListeners()
        {
            Destroy(progressBar.gameObject);
            progressBar = null;
        }

        public override void SetGameModeUI()
        {
            GameObject progressBarInstance = Instantiate(owner.ProductionButtonBarPrefab);
            progressBarInstance.transform.SetParent(owner.MainCanvas.transform.GetChild(1), false);
            progressBarInstance.transform.SetAsFirstSibling();
            progressBar = progressBarInstance.GetComponent<UIProductionProgressBarButton>();
            progressBar.Hide();
        }

        public override void OnBuildingSelected(CityBuilding cityBuilding)
        {
            if(cityBuilding.GetComponentInChildren<ProductionModule>())
            {
                progressBar.transform.position = owner.GameCamera.WorldToScreenPoint(cityBuilding.BarReference.position);
                progressBar.Initialize(cityBuilding, cityBuilding.GetComponentInChildren<ProductionModule>());
                progressBar.Show();

            } // end if
        }

        public override void OnBuildingDiselected(CityBuilding cityBuilding)
        {
            progressBar?.Hide();
        }

        private void OnDestroy()
        {
            RemoveGameModeListeners();
        }
    }
}


