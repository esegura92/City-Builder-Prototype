using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder.Input
{
    /// <summary>
    /// Component in charge of handling all the tap events for the game object
    /// </summary>
    public class TapComponent : MonoBehaviour, ITappable
    {
        /// <summary>
        /// Pointer enter event
        /// </summary>
        [SerializeField]
        private UnityEvent onPointerEnter;
        /// <summary>
        /// Pointer exit event
        /// </summary>
        [SerializeField]
        private UnityEvent onPointerExit;
        /// <summary>
        /// Pointer down event
        /// </summary>
        [SerializeField]
        private UnityEvent onPointerDown;
        /// <summary>
        /// Pointer click event
        /// </summary>
        [SerializeField]
        private UnityEvent onPointerClick;
        /// <summary>
        /// Accessor for the pointer enter event
        /// </summary>
        public UnityEvent OnPointerEnter
        {
            get { return onPointerEnter; }
        }
        /// <summary>
        /// Accessor for the pointer exit event
        /// </summary>
        public UnityEvent OnPointerExit
        {
            get { return onPointerExit; }
        }
        /// <summary>
        /// Accessor for the pointer down event
        /// </summary>
        public UnityEvent OnPointerDown
        {
            get { return onPointerDown; }
        }
        /// <summary>
        /// Accessor for the pointer click event
        /// </summary>
        public UnityEvent OnPointerClick
        {
            get { return onPointerClick; }
        }

        void Awake()
        {
            /*
            onPointerEnter = new UnityEvent();
            onPointerExit = new UnityEvent();
            onPointerClick = new UnityEvent();
            onPointerDown = new UnityEvent();
            */
        }
        /// <summary>
        /// Implementation of the pointer click event, invokes the pointer click event, if there is any
        /// </summary>
        public virtual void PointerClick()
        {
            onPointerClick?.Invoke();
        }
        /// <summary>
        /// Implementation of the pointer down event, invokes the pointer down event, if there is any
        /// </summary>
        public virtual void PointerDown()
        {
            onPointerDown?.Invoke();
        }
        /// <summary>
        /// Implementation of the pointer enter event, invokes the pointer enter event if there is any
        /// </summary>
        public virtual void PointerEnter()
        {
            onPointerEnter?.Invoke();
        }
        /// <summary>
        /// Implementation of the pointer exit event, invokes the pointer exit event if there is any
        /// </summary>
        public virtual void PointerExit()
        {
            OnPointerExit?.Invoke();
        }
    }
}
