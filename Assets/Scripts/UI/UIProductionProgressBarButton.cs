using CityBuilder.GameEntities;
using CityBuilder.GameEntities.Modules;
using CityBuilder.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.UI
{
    /// <summary>
    /// UI Class that handles the production progress bar
    /// Inherits from progress bar button
    /// </summary>
    public class UIProductionProgressBarButton : UIProgressBarButton
    {
        [SerializeField]
        private Sprite goldSprite;
        [SerializeField]
        private Sprite woodSprite;
        [SerializeField]
        private Sprite steelSprite;
        [SerializeField]
        private Image icon;
        /// <summary>
        /// Production module related to the bar behaviour
        /// </summary>
        private ProductionModule linkedModule;

        /// <summary>
        /// Implementation of the initialize method, checks some production properties and 
        /// add some events
        /// </summary>
        /// <param name="building">Building owner of the bar</param>
        /// <param name="module">Owner module of the bar</param>
        public override void Initialize(CityBuilding building, BaseModule module)
        {
            base.Initialize(building, module);
            linkedModule = module as ProductionModule;
            SetIcon(linkedModule.Resource);
            if (linkedModule.InProduction)
            {
                HideButton();
            } // end if
            else
            {
                RestartProgressBar();
                ShowButton();
            }
            startProgressButton.onClick.AddListener(linkedModule.StartProduction);
            linkedModule.OnProductionStart.AddListener(HideButton);
            linkedModule.OnProductionExecute.AddListener(UpdateProgress);
            linkedModule.OnProductionFinish.AddListener(RestartProgressBar);
            linkedModule.OnProductionFinish.AddListener(ShowButton);
            if(linkedModule.IsAutomatic)
            {
                linkedModule.OnProductionFinish.AddListener(HideButton);
                startProgressButton.onClick.AddListener(linkedModule.StartProduction);
            } // end if
                

        }

        private void SetIcon(ResourceType type)
        {
            switch(type)
            {
                case ResourceType.Gold:
                    icon.sprite = goldSprite;
                    break;
                case ResourceType.Wood:
                    icon.sprite = woodSprite;
                    break;
                case ResourceType.Steel:
                    icon.sprite = steelSprite;
                    break;
            }
                
        }
        /// <summary>
        /// Hides the progressbar button
        /// </summary>
        public override void Hide()
        {
            if(linkedModule != null)
            {
                startProgressButton.onClick.RemoveListener(linkedModule.StartProduction);
            }
            
            this.gameObject.SetActive(false);
        }
        /// <summary>
        /// Show the progress bar button
        /// </summary>
        public override void Show()
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// Updates the bar based on the passed seconds of the production building
        /// </summary>
        protected override void UpdateProgress()
        {
            int passedSeconds = linkedModule.SecondsInProduction;
            int totalSeconds = linkedModule.ProductionTime;
            float fillAmount = ((passedSeconds * 100.0f) / totalSeconds) / 100.0f;
            fillImage.fillAmount = fillAmount;
        }
        
        /// <summary>
        /// Removes all the event references before it gets destroyed
        /// </summary>
        private void OnDestroy()
        {
            if(linkedModule != null)
            {
                startProgressButton.onClick.RemoveListener(linkedModule.StartProduction);
                linkedModule.OnProductionStart.RemoveListener(HideButton);
                linkedModule.OnProductionExecute.RemoveListener(UpdateProgress);
                linkedModule.OnProductionFinish.RemoveListener(RestartProgressBar);
                linkedModule.OnProductionFinish.RemoveListener(ShowButton);

            }
            
        }
    }
}


