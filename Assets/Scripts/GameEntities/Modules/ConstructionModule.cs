using CityBuilder.Controllers;
using CityBuilder.Timers;
using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder.GameEntities.Modules
{
    /// <summary>
    /// Module in charge of the construction phase of a building
    /// </summary>
    public class ConstructionModule : BaseModule, IConstructable, ITimed
    {
        /// <summary>
        /// Construction seconds since the construction started
        /// </summary>
        private int passedConstructionSeconds;
        /// <summary>
        /// Start construction event
        /// </summary>
        [SerializeField]
        private UnityAction onConstructionStart;
        /// <summary>
        /// Finish construction event
        /// </summary>
        [SerializeField]
        private UnityAction onConstructionFinish;
        [SerializeField]
        private UnityAction onConstructionExecute;
        /// <summary>
        /// Accessor for the construction seconds that have passed
        /// </summary>
        public int PassedConstructionSeconds
        {
            get { return passedConstructionSeconds; }
        }

        /// <summary>
        /// Accesor for the construction time
        /// </summary>
        [SerializeField]
        public int ConstructionTime
        {
            get { return OwnerBuilding.ConstructionSeconds; }
        }

        /// <summary>
        /// Implementation of the Initialize method, set the module owner and starts the construction
        /// </summary>
        /// <param name="data"></param>
        /// <param name="owner"></param>
        public override void Initialize(ScriptableObject data, CityBuilding owner)
        {
            this.owner = owner;
            StartConstruction();

        }
        /// <summary>
        /// Implementation of the finish construction event, invokes the event if there is any
        /// and destroy itself
        /// </summary>
        public virtual void FinishConstruction()
        {
            passedConstructionSeconds = owner.ConstructionSeconds;
            onConstructionFinish?.Invoke();
            Destroy(this);
        }
        /// <summary>
        /// Implementation of the start construction event, invokes the event if there is any
        /// </summary>
        public void StartConstruction()
        {
            passedConstructionSeconds = 0;

            TimerController.Instance.StartTimer(this, owner.ConstructionSeconds);
            owner.OwnerPlayer.GameMode.DisplayConstructionBar(owner);
            onConstructionStart?.Invoke();
        }
        /// <summary>
        // Implementation of the execute construction method, invoke the construction execute method if there is any suscribed
        /// </summary>
        public void ExecuteConstruction()
        {
            onConstructionExecute?.Invoke();
        }
        /// <summary>
        /// Implementation of the timer begin interface method. Does nothing at the moment
        /// </summary>
        public void OnTimerBegin()
        {
            //throw new System.NotImplementedException();
        }
        /// <summary>
        /// Implementation of the timer end interface method. Finishes the construction
        /// </summary>
        public void OnTimerEnd()
        {
            FinishConstruction();
        }
        /// <summary>
        /// Implementation of the timer update interface method, calculate the passed seconds
        /// and calls the execute construction method
        /// </summary>
        /// <param name="remainingSeconds"></param>
        /// <param name="originalTime"></param>
        public void OnTimerUpdate(float remainingSeconds, float originalTime)
        {
            passedConstructionSeconds = Mathf.RoundToInt(originalTime - remainingSeconds);
            ExecuteConstruction();
        }

        public void AddStartConstructionAction(UnityAction action)
        {
            onConstructionStart += action;
        }

        public void AddExecuteConstructionAction(UnityAction action)
        {
            onConstructionExecute += action;
        }

        public void AddFinishConstructionAction(UnityAction action)
        {
            onConstructionFinish += action;
        }

        public void RemoveStartConstructionAction(UnityAction action)
        {
            onConstructionStart -= action;
        }

        public void RemoveExecuteConstructionAction(UnityAction action)
        {
            onConstructionExecute -= action;
        }

        public void RemoveFinishConstructionAction(UnityAction action)
        {
            onConstructionFinish -= action;
        }

        void OnDestroy()
        {
            onConstructionStart = null;
            onConstructionExecute = null;
            onConstructionFinish = null;
        }
    }
}
