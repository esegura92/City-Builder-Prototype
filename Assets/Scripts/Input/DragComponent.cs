using CityBuilder.GameEntities;
using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder.Input
{
    /// <summary>
    /// Component in charge of handling all the drag events for the game object
    /// </summary>
    public class DragComponent : MonoBehaviour, IDraggable
    {
        /// <summary>
        /// Drag begin event
        /// </summary>
        [SerializeField]
        private UnityEvent onDragBegin;
        /// <summary>
        /// Drag event
        /// </summary>
        [SerializeField]
        private UnityEvent onDrag;
        /// <summary>
        /// Drag end event
        /// </summary>
        [SerializeField]
        private UnityEvent onDragEnd;
        /// <summary>
        /// Range outside the object where the drag is still valid
        /// </summary>
        [SerializeField]
        private float offset = 0;
        private Vector2 initialDragPosition;
        private bool dragStarted;
        private Camera camera;
        /// <summary>
        /// Accessor for the drag begin event
        /// </summary>
        public UnityEvent OnDragBegin
        {
            get { return onDragBegin; }
        }
        /// <summary>
        /// Accessor for the drag event
        /// </summary>
        public UnityEvent OnDrag
        {
            get { return onDrag; }
        }
        /// <summary>
        /// Accessor for the drag end event
        /// </summary>
        public UnityEvent OnDragEnd
        {
            get { return onDragEnd; }
        }

        private void Awake()
        {
            camera = Camera.main;
        }
        /// <summary>
        /// Implementation of the Begin drag event, invokes the onDragBegin event, if there is any
        /// </summary>
        /// <param name="position"></param>
        public virtual void BeginDrag(Vector2 position)
        {
            initialDragPosition = position;
            if (Vector3.Distance(initialDragPosition, position) >= offset)
            {
                transform.position = GameInput.CalculatePositionInGame(position);

            } // end if
            onDragBegin?.Invoke();
        }
        /// <summary>
        /// Implementation of the drag event, invokes the onDrag event, if there is any
        /// </summary>
        /// <param name="position"></param>
        public virtual void Drag(Vector2 position)
        {
            transform.position = GameInput.CalculatePositionInGame(position);
            onDrag?.Invoke();
        }
        /// <summary>
        /// Implementation of the end drag event, invokes the onDragEnd event, if there is any
        /// </summary>
        /// <param name="position"></param>
        public virtual void EndDrag(Vector2 position)
        {
            if (GameInput.IsValidPosition(position, GetComponent<CityBuilding>().Width, GetComponent<CityBuilding>().Height))
            {
                transform.position = GameInput.CalculatePositionInGame(position);
            } // end if
            else
            {
                transform.position = GameInput.CalculatePositionInGame(initialDragPosition);
            } // end else
                
            onDragEnd?.Invoke();
        }
        /// <summary>
        /// Implementation of the getSuscribed method, returns the current game object
        /// </summary>
        /// <returns></returns>
        public virtual GameObject GetSuscribedGameObject()
        {
            return gameObject;
        }
    }
}
