using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace CityBuilder.GameEntities.Modules
{
    /// <summary>
    /// Interface that handles the production events
    /// </summary>
    public interface IProductable
    {
        /// <summary>
        /// Accessor for the inproduction flag
        /// </summary>
        bool InProduction
        {
            get;
        }
        /// <summary>
        /// Accessor for the start production event
        /// </summary>
        UnityEvent OnProductionStart { get; }
        /// <summary>
        /// Accessor for the finish production event
        /// </summary>
        UnityEvent OnProductionFinish { get; }

        UnityEvent OnProductionExecute { get; }
        /// <summary>
        /// Start resource production event
        /// </summary>
        void StartProduction();
        /// <summary>
        /// Finish resource production event
        /// </summary>
        void FinishProduction();
        /// <summary>
        /// Execute the resource production event
        /// </summary>
        void ExecuteProduction();
        
    }
}
