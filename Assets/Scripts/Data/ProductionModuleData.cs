using CityBuilder.Resources;
using System;
using UnityEngine;

namespace CityBuilder.Data
{
    /// <summary>
    /// Contains all the information or data of a game building as an Unity Scriptable Object
    /// </summary>
    [CreateAssetMenu(fileName = "ProductionModuleData", menuName = "BuildingData/Production Module Data")]
    [Serializable]
    public class ProductionModuleData : BaseModuleData
    {
        [Header("Production Data")]
        /// <summary>
        /// Resource type that this building produces
        /// </summary>
        [SerializeField]
        private ResourceType producedResource;
        /// <summary>
        /// Time that the building requires to produce the building
        /// </summary>
        [SerializeField]
        private int productionTime;
        /// <summary>
        /// Amount of resources that the building produces in a range of time
        /// </summary>
        [SerializeField]
        private int producedAmount;
        /// <summary>
        /// Tell us if the resource production is automatic or manual
        /// </summary>
        [SerializeField]
        private bool isAutomatedProduction;

        /// <summary>
        /// Produced Resource Property.
        /// </summary>
        public ResourceType ProducedResource
        {
            get { return producedResource; }
        }

        public int ProductionTime
        {
            get { return productionTime; }
        }

        public int ProducedAmount
        {
            get { return producedAmount; }
        }

        public bool IsAutomatedProduction
        {
            get { return isAutomatedProduction; }
        }


    }
}
