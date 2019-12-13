using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder.GameEntities.Modules
{
    /// <summary>
    /// Base class for all the modules
    /// </summary>
    public abstract class BaseModule : MonoBehaviour
    {
        /// <summary>
        /// Building that contains this module
        /// </summary>
        protected CityBuilding owner;

        /// <summary>
        /// Accessor for the owner building of this module
        /// </summary>
        public CityBuilding OwnerBuilding
        {
            get { return owner; }
        }

        /// <summary>
        /// Should initialize the module
        /// </summary>
        /// <param name="data">Module data</param>
        /// <param name="owner">Reference to the owner building of this module</param>
        public abstract void Initialize(ScriptableObject data, CityBuilding owner);
    }
}
