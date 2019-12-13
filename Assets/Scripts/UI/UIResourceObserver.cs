using CityBuilder.Controllers;
using CityBuilder.GameEntities;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.UI
{
    /// <summary>
    /// UI class for the resource text displayed on the ui
    /// It reference the player and suscribe his methods to
    /// the player resource modify events
    /// </summary>
    public class UIResourceObserver : MonoBehaviour
    {
        /// <summary>
        /// UI Text for the gold amount
        /// </summary>
        [SerializeField]
        private Text goldAmount;
        /// <summary>
        /// UI Text for the wood amount
        /// </summary>
        [SerializeField]
        private Text woodAmount;
        /// <summary>
        /// UI Text for the steel amount
        /// </summary>
        [SerializeField]
        private Text steelAmount;
        /// <summary>
        /// Reference to the game controller
        /// </summary>
        private GameController controller;

        /// <summary>
        /// Initialize and suscribe to all player events
        /// </summary>
        private void Awake()
        {
            
        }

        public void Initialize(GameController gameController)
        {
            controller = gameController;
            controller.GamePlayer.AddOnGoldModifyAction(SetGoldText);
            controller.GamePlayer.AddOnWoodModifyAction(SetWoodText);
            controller.GamePlayer.AddOnSteelModifyAction(SetSteelText);
            SetGoldText(controller.GamePlayer.TotalGold);
        }

        /// <summary>
        /// Set the gold text with the given amount
        /// </summary>
        /// <param name="amount">Gold amount to be set on the UI Text</param>
        private void SetGoldText(int amount)
        {
            goldAmount.text = GetTextAmountFormat(amount);
        }
        /// <summary>
        /// Set the wood text with the given amount
        /// </summary>
        /// <param name="amount">Wood amount to be set on the UI Text</param>
        private void SetWoodText(int amount)
        {
            woodAmount.text = GetTextAmountFormat(amount);
        }
        /// <summary>
        /// Set the steel text with the given amount
        /// </summary>
        /// <param name="amount">Steel amount to be set on the UI Text</param>
        private void SetSteelText(int amount)
        {
            steelAmount.text = GetTextAmountFormat(amount);
        }
        /// <summary>
        /// Obtain a string with the correct UI format
        /// </summary>
        /// <param name="amount">Amount to be set on the string format</param>
        /// <returns></returns>
        private string GetTextAmountFormat(int amount)
        {
            return "[" + amount.ToString() + "]";
        }

        /// <summary>
        /// Removes the suscribed methods from the player events
        /// </summary>
        private void OnDestroy()
        {
            controller.GamePlayer.RemoveOnGoldModifyAction(SetGoldText);
            controller.GamePlayer.RemoveOnWoodModifyAction(SetWoodText);
            controller.GamePlayer.RemoveOnSteelModifyAction(SetSteelText);
        }

    }
}
