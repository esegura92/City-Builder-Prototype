using System;
using UnityEngine;

namespace CityBuilder.Resources
{
    /// <summary>
    /// A resource entity that contains a resource type and an amount
    /// </summary>
    [Serializable]
    public class ResourceAmount
    {
        /// <summary>
        /// Type of resource
        /// </summary>
        [SerializeField]
        private ResourceType type;
        /// <summary>
        /// Amount of the resource
        /// </summary>
        [SerializeField]
        private int amount;
        /// <summary>
        /// Accessor for the type attribute
        /// </summary>
        public ResourceType Type
        {
            get { return type; }
        }
        /// <summary>
        /// Accessor for the amount attribute
        /// </summary>
        public int Amount
        {
            set { amount = value; }
            get { return amount; }
        }
    }
}
