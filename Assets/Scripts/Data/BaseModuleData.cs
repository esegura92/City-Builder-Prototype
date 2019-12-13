using UnityEngine;

namespace CityBuilder.Data
{
    /// <summary>
    /// Base Class of a Module Data
    /// </summary>
    public abstract class BaseModuleData : ScriptableObject
    {
        /// <summary>
        /// Prefab that contains the component module
        /// </summary>
        [SerializeField]
        private GameObject modulePrefab;
        /// <summary>
        /// Public accesor of the component module
        /// </summary>
        public GameObject ModulePrefab
        {
            get { return modulePrefab; }
        }
    }
}


