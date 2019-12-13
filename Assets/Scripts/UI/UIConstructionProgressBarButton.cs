using CityBuilder.GameEntities;
using CityBuilder.GameEntities.Modules;
using UnityEngine;

namespace CityBuilder.UI
{
    /// <summary>
    /// UI Class that handles the production construction bar
    /// Inherits from progress bar button
    /// </summary>
    public class UIConstructionProgressBarButton : UIProgressBarButton
    {
        /// <summary>
        /// Production module related to the bar behaviour
        /// </summary>
        private ConstructionModule linkedModule;
        /// <summary>
        /// Implementation of the initialize method, checks some production properties and 
        /// add some events
        /// </summary>
        /// <param name="building">Building owner of the bar</param>
        /// <param name="module">Owner module of the bar</param>
        public override void Initialize(CityBuilding building, BaseModule module)
        {
            base.Initialize(building, module);
            linkedModule = module as ConstructionModule;
            UpdateProgress();
            linkedModule.AddExecuteConstructionAction(UpdateProgress);
            linkedModule.AddFinishConstructionAction(UpdateProgress);
            linkedModule.AddFinishConstructionAction(Hide);

        }
        /// <summary>
        /// Hides the button because its no use in here
        /// </summary>
        private void Awake()
        {
            HideButton();
        }
        /// <summary>
        /// Calls the destroy component event
        /// </summary>
        public override void Hide()
        {
            Destroy(gameObject);
        }
        /// <summary>
        /// Shows the game object
        /// </summary>
        public override void Show()
        {
            this.gameObject.SetActive(true);
        }
        /// <summary>
        /// Implementation of the progress bar update methods
        /// updates it in base of the passed construction seconds
        /// </summary>
        protected override void UpdateProgress()
        {
            int passedSeconds = linkedModule.PassedConstructionSeconds;
            int totalSeconds = linkedModule.ConstructionTime;
            float fillAmount = ((passedSeconds * 100.0f) / totalSeconds) / 100.0f;
            fillImage.fillAmount = fillAmount;
        }
        /// <summary>
        /// Unsuscribe from event to avoid nullreferences
        /// </summary>
        private void OnDestroy()
        {
            linkedModule.RemoveExecuteConstructionAction(UpdateProgress);
            linkedModule.RemoveFinishConstructionAction(UpdateProgress);
            linkedModule.RemoveFinishConstructionAction(Hide);
        }
    }
}


