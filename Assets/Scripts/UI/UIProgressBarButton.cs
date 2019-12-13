using CityBuilder.GameEntities;
using CityBuilder.GameEntities.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.UI
{
    /// <summary>
    /// Base class for the UI progress bar button,
    /// this entity has a button that when pressed
    /// starts a progress bar
    /// </summary>
    public abstract class UIProgressBarButton : MonoBehaviour
    {
        /// <summary>
        /// Button that triggers the actions
        /// </summary>
        [SerializeField]
        protected Button startProgressButton;
        /// <summary>
        /// Progress bar background image
        /// </summary>
        [SerializeField]
        protected Image backgroundImage;
        /// <summary>
        /// Progress bar fill image
        /// </summary>
        [SerializeField]
        protected Image fillImage;
        /// <summary>
        /// Building that interacts with the button
        /// </summary>
        protected CityBuilding ownedBuilding;
        /// <summary>
        /// Set the building reference and can be overritten
        /// </summary>
        /// <param name="building">Building reference for the button</param>
        /// <param name="module">Module that contains the button actions for the bar</param>
        public virtual void Initialize(CityBuilding building, BaseModule module)
        {
            ownedBuilding = building;
            fillImage.fillAmount = 0;
        }
        /// <summary>
        /// Show the game object to the screen
        /// </summary>
        public abstract void Show();
        /// <summary>
        /// Hides the game object from the screen
        /// </summary>
        public abstract void Hide();
        /// <summary>
        /// Updates the progress bar
        /// </summary>
        protected abstract void UpdateProgress();
        /// <summary>
        /// Hides the start progress button
        /// </summary>
        protected void HideButton()
        {
            startProgressButton.enabled = false;
        }
        /// <summary>
        /// Shows the start progress button
        /// </summary>
        protected void ShowButton()
        {
            startProgressButton.enabled = true;
        }
        /// <summary>
        /// Resets the progress bar to zero
        /// </summary>
        protected void RestartProgressBar()
        {
            fillImage.fillAmount = 0;
        }

        

    }
}


