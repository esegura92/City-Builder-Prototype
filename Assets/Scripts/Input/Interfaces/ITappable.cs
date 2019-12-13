using UnityEngine.Events;

namespace CityBuilder.Input
{
    /// <summary>
    /// Interface that handles Tap Input events
    /// </summary>
    public interface ITappable
    {
        /// <summary>
        /// Accessor of the Pointer Enter event
        /// </summary>
        UnityEvent OnPointerEnter { get; }
        /// <summary>
        /// Accessor of the Pointer Exit event
        /// </summary>
        UnityEvent OnPointerExit { get; }
        /// <summary>
        /// Accessor of the Pointer Down Event
        /// </summary>
        UnityEvent OnPointerDown { get; }
        /// <summary>
        /// Accessor of the Pointer Click event
        /// </summary>
        UnityEvent OnPointerClick { get; }

        /// <summary>
        /// Pointer Enter event
        /// </summary>
        void PointerEnter();
        /// <summary>
        /// Pointer Exit event
        /// </summary>
        void PointerExit();
        /// <summary>
        /// Pointer Down event
        /// </summary>
        void PointerDown();
        /// <summary>
        /// Pointer Click event
        /// </summary>
        void PointerClick();
    }
}
