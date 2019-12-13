using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder.Input
{
    /// <summary>
    /// Interface that handles all the input events
    /// </summary>
    public interface IDraggable
    {
        /// <summary>
        /// Accessor for the drag started event
        /// </summary>
        UnityEvent OnDragBegin { get; }
        /// <summary>
        /// Accessor for the drag event
        /// </summary>
        UnityEvent OnDrag { get; }
        /// <summary>
        /// Accessor for the drag end event
        /// </summary>
        UnityEvent OnDragEnd { get; }
        /// <summary>
        /// Obtain the suscribed game object to this interface
        /// </summary>
        /// <returns>The suscribed game object</returns>
        GameObject GetSuscribedGameObject();
        /// <summary>
        /// Begin drag event
        /// </summary>
        /// <param name="position">Current position at the execution of the event</param>
        void BeginDrag(Vector2 position);
        /// <summary>
        /// Drag event
        /// </summary>
        /// <param name="position">Current position at the execution of the event</param>
        void Drag(Vector2 position);
        /// <summary>
        /// Drag end event
        /// </summary>
        /// <param name="position">Current position at the execution of the event</param>
        void EndDrag(Vector2 position);

    }
}
