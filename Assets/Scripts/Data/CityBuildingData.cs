using CityBuilder.Resources;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.Data
{
    /// <summary>
    /// Contains all the information or data of a game building as an Unity Scriptable Object
    /// </summary>
    [CreateAssetMenu(fileName = "CityBuilding", menuName = "BuildingData/City Building")]
    [Serializable]
    public class CityBuildingData : ScriptableObject
    {
        [Header("Main Data")]
        /// <summary>
        /// Building identifier
        /// </summary>
        [SerializeField]
        private int id;
        /// <summary>
        /// Building Name
        /// </summary>
        [SerializeField]
        private string buildingName;
        /// <summary>
        /// Total resources cost of the building
        /// </summary>
        [SerializeField]
        private List<ResourceAmount> cost;

        [SerializeField]
        private int constructionSeconds;
        [SerializeField]
        private int width;
        /// <summary>
        /// Size on z coordenates that the building ocupies on the grid
        /// </summary>
        [SerializeField]
        private int height;
        /// <summary>
        /// Visual representation of the Building in Unity
        /// </summary>
        [SerializeField]
        private GameObject visualPrefab;
        [Header("Modules")]
        /// <summary>
        /// Data of all the module's data the building contains
        /// </summary>
        [SerializeField]
        private List<BaseModuleData> modulesData;

        /// <summary>
        /// City Building Id accessor
        /// </summary>
        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// City Building Name accessor
        /// </summary>
        public string Name
        {
            get { return buildingName; }
        }

        /// <summary>
        /// City Building Cost accessor
        /// </summary>
        public List<ResourceAmount> Cost
        {
            get { return cost; }
        }

        /// <summary>
        /// City Building Width accessor
        /// </summary>
        public int Width
        {
            get { return width; }
        }

        /// <summary>
        /// City Building Height accessor
        /// </summary>
        public int Height
        {
            get { return height; }
        }

        /// <summary>
        /// City Building construction seconds accessor
        /// </summary>
        public int ConstructionSeconds
        {
            get { return constructionSeconds; }
        }

        /// <summary>
        /// City Building module's data accessor
        /// </summary>
        public List<BaseModuleData> ModulesData
        {
            get { return modulesData; }
        }

        /// <summary>
        /// City Building visual prefab accessor
        /// </summary>
        public GameObject VisualPrefab
        {
            get { return visualPrefab; }
        }
    }
}
