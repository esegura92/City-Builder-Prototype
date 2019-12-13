using CityBuilder.Data;
using CityBuilder.Controllers;
using CityBuilder.Resources;
using UnityEngine;
using UnityEngine.Events;
using CityBuilder.Timers;

namespace CityBuilder.GameEntities.Modules
{
    /// <summary>
    /// Module in charge of the resource production of a building
    /// </summary>
    public class ProductionModule : BaseModule, IProductable, ITimed
    {
        /// <summary>
        /// Production data of the module
        /// </summary>
        private ProductionModuleData data;

        private bool inProduction;
        /// <summary>
        /// Seconds the module has been in production
        /// </summary>
        private int secondsInProduction;
        /// <summary>
        /// Start production event
        /// </summary>
        [SerializeField]
        private UnityEvent onProductionStart;
        /// <summary>
        /// Finish production event
        /// </summary>
        [SerializeField]
        private UnityEvent onProductionFinish;
        /// <summary>
        /// Production execution event
        /// </summary>
        [SerializeField]
        private UnityEvent onProductionExecute;
        /// <summary>
        /// Accessor for the production time
        /// </summary>
        public int ProductionTime
        {
            get { return data.ProductionTime; }
        }
        /// <summary>
        /// Accessor for the production amount
        /// </summary>
        public int ProductionAmount
        {
            get { return data.ProducedAmount; }
        }
        /// <summary>
        /// Accessor for the automatic production property
        /// </summary>
        public bool IsAutomatic
        {
            get { return data.IsAutomatedProduction; }
        }
        /// <summary>
        /// Accessor for the resource type that's being produced
        /// </summary>
        public ResourceType Resource
        {
            get { return data.ProducedResource; }
        }

        public int SecondsInProduction
        {
            get { return secondsInProduction; }
        }

        /// <summary>
        /// Accessor for the start production event
        /// </summary>
        public UnityEvent OnProductionStart
        {
            get { return onProductionStart; }
        }
        /// <summary>
        /// Accessor for the finish production event
        /// </summary>
        public UnityEvent OnProductionFinish
        {
            get { return onProductionFinish; }
        }
        /// <summary>
        /// Accessor for the On production execute event
        /// </summary>
        public UnityEvent OnProductionExecute
        {
            get { return onProductionExecute; }
        }
        /// <summary>
        /// Accessor for the in production flag
        /// </summary>
        public bool InProduction
        {
            get { return inProduction; }
        }
        /// <summary>
        /// Implementation of the finish production event, invokes the event if there is any
        /// </summary>
        public virtual void FinishProduction()
        {
            secondsInProduction = 0;
            inProduction = false;
            onProductionFinish?.Invoke();
            owner.OwnerPlayer.AddResource(data.ProducedResource, data.ProducedAmount);
            if(data.IsAutomatedProduction)
            {
                StartProduction();
            } // end if
        }
        /// <summary>
        /// Implementation of the start production event, calls the timer event to start the event
        /// and invokes the event if there is any suscribed
        /// </summary>
        public virtual void StartProduction()
        {
            inProduction = true;
            TimerController.Instance.StartTimer(this, data.ProductionTime);
            onProductionStart?.Invoke();
        }
        /// <summary>
        /// Implementation of the execute production event, calls the event if there is any suscribed
        /// </summary>
        public void ExecuteProduction()
        {
            onProductionExecute?.Invoke();
        }
        /// <summary>
        /// Implementation of the initialize method, cast the scriptable object as a production 
        /// module data and set the owner building
        /// </summary>
        /// <param name="data">Production module data </param>
        /// <param name="owner">Owner building of this module</param>
        public override void Initialize(ScriptableObject data, CityBuilding owner)
        {
            this.data = data as ProductionModuleData;
            this.owner = owner;
            if(IsAutomatic)
            {
                StartProduction();
            } // end if
        }
        /// <summary>
        /// Event that triggers when the timer starts
        /// </summary>
        public void OnTimerBegin()
        {
            //throw new System.NotImplementedException();
        }
        /// <summary>
        /// Event that triggers when the timer ends
        /// </summary>
        public void OnTimerEnd()
        {
            FinishProduction();
        }
        /// <summary>
        /// Event that triggers every determined time, updates the seconds in production
        /// </summary>
        /// <param name="remainingSeconds">Remainind seconds to finish</param>
        /// <param name="originalTime"></param>
        public void OnTimerUpdate(float remainingSeconds, float originalTime)
        {
            secondsInProduction = Mathf.RoundToInt(originalTime - remainingSeconds);
            ExecuteProduction();
        }

        
    }
}
