using UnityEngine.Events;

namespace CityBuilder.GameEntities.Modules
{
    /// <summary>
    /// Interface that handles the construction events
    /// </summary>
    public interface IConstructable
    {
 
        /// <summary>
        /// Start construction event
        /// </summary>
        void StartConstruction();
        /// <summary>
        /// Finish construction event
        /// </summary>
        void FinishConstruction();
        /// <summary>
        /// Execute construction event
        /// </summary>
        void ExecuteConstruction();

        void AddStartConstructionAction(UnityAction action);
        void AddExecuteConstructionAction(UnityAction action);
        void AddFinishConstructionAction(UnityAction action);
        void RemoveStartConstructionAction(UnityAction action);
        void RemoveExecuteConstructionAction(UnityAction action);
        void RemoveFinishConstructionAction(UnityAction action);
    }
}
