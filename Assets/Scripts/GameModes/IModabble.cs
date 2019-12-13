using CityBuilder.GameEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.GameModes
{
    public interface IModabble
    {
        void AddGameModeListeners();
        void RemoveGameModeListeners();
        void SetGameModeUI();
        void OnBuildingSelected(CityBuilding cityBuilding);
        void OnBuildingDiselected(CityBuilding cityBuilding);
    }
}


