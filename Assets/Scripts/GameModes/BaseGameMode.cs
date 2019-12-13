using CityBuilder.Controllers;
using CityBuilder.GameEntities;
using CityBuilder.GameEntities.Modules;
using CityBuilder.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.GameModes
{
    public abstract class BaseGameMode : MonoBehaviour, IModabble
    {
        protected GameController owner;
        protected Player gamePlayer;

        protected CityBuilding selectedBuilding;
        public GameController Controller
        {
            get { return owner; }
        }

        public Player GamePlayer
        {
            get { return gamePlayer; }
        }

        public abstract void AddGameModeListeners();
        public abstract void RemoveGameModeListeners();
        public abstract void SetGameModeUI();
        public virtual void Initialize(GameController gameController)
        {
            owner = gameController;
            gamePlayer = owner.GamePlayer;
            gamePlayer.SetGameMode(this);
        }

        public void SetSelectedBuilding(CityBuilding building)
        {
            if(selectedBuilding != null)
            {
                OnBuildingDiselected(selectedBuilding);
            } // end if

            selectedBuilding = building;
            if(selectedBuilding != null)
            {
                OnBuildingSelected(selectedBuilding);
            } // end if
        }
        
        public void DisplayConstructionBar(CityBuilding building)
        {
            //Vector3 screenPos = owner.GameCamera.WorldToScreenPoint(building.BarReference.position);
            GameObject constructionBarInstance = Instantiate(owner.ConstructionBarPrefab);
            constructionBarInstance.transform.SetParent(owner.MainCanvas.transform.GetChild(1), false);
            constructionBarInstance.transform.SetAsFirstSibling();
            //constructionBarInstance.transform.position = screenPos;
            UIConstructionProgressBarButton bar = constructionBarInstance.GetComponent<UIConstructionProgressBarButton>();
            bar.Initialize(building, building.GetComponent<ConstructionModule>());
        }

        public abstract void OnBuildingSelected(CityBuilding cityBuilding);
        public abstract void OnBuildingDiselected(CityBuilding cityBuilding);
    }
}

