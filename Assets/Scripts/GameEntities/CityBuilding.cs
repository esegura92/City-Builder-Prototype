using CityBuilder.Data;
using CityBuilder.GameEntities.Modules;
using CityBuilder.Input;
using CityBuilder.Resources;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.GameEntities
{
    /// <summary>
    /// City building representation as an Unity Object
    /// It needs to be constructed first but can be placed and moved
    /// and some of them produce resources
    /// </summary>
    public class CityBuilding : MonoBehaviour
    {
        [SerializeField]
        private Transform barReference;

        private Action<CityBuilding> onDragBeginEvent;
        /// <summary>
        /// Building drag event 
        /// </summary>
        private Action<CityBuilding> onDragEvent;
        /// <summary>
        /// Building drag end event 
        /// </summary>
        private Action<CityBuilding> onDragEndEvent;
        /// <summary>
        /// Building click event
        /// </summary>
        private Action<CityBuilding> onClickEvent;
        /// <summary>
        /// Tap component of the building
        /// </summary>
        [SerializeField]
        private TapComponent tapComponent;
        /// <summary>
        /// Drag component of the building
        /// </summary>
        [SerializeField]
        private DragComponent dragComponent;
        /// <summary>
        /// Module list of the building
        /// </summary>
        private List<BaseModule> modules;
        /// <summary>
        /// Data of the building
        /// </summary>
        private CityBuildingData data;
        /// <summary>
        /// Reference to the owner player
        /// </summary>
        private Player owner;

        /// <summary>
        /// Reference to the current tiles of the building
        /// </summary>
        List<GridTile> currentTiles = new List<GridTile>();
        public List<GridTile> CurrentTiles
        {
            set
            {
                currentTiles = value;
            }
            get
            {
                return currentTiles;
            }
        }

        public Transform BarReference
        {
            get { return barReference; }
        }
        /// <summary>
        /// Accessor for the building id
        /// </summary>
        public int Id
        {
            get { return data.Id; }
        }
        /// <summary>
        /// Accessor for the building name
        /// </summary>
        public string Name
        {
            get { return data.name; }
        }
        /// <summary>
        /// Accessor for the building cost
        /// </summary>
        public List<ResourceAmount> Cost
        {
            get { return data.Cost; }
        }
        /// <summary>
        /// Accessor for the construction seconds 
        /// </summary>
        public int ConstructionSeconds
        {
            get { return data.ConstructionSeconds; }
        }
        /// <summary>
        /// Accessor for the building width
        /// </summary>
        public int Width
        {
            get { return data.Width; }
        }
        /// <summary>
        /// Accessor for the building height
        /// </summary>
        public int Height
        {
            get { return data.Height; }
        }
        /// <summary>
        /// Accessor for the onwer player
        /// </summary>
        public Player OwnerPlayer
        {
            get { return owner; }
        }

        /// <summary>
        /// Start is called before the first frame update
        /// Initializes the module list and set the events and components
        /// </summary>
        void Awake()
        {//checo si el tile tiene algun objeto
            modules = new List<BaseModule>();
            if (!tapComponent)
            {
                tapComponent = gameObject.AddComponent<TapComponent>();
                tapComponent.OnPointerClick.AddListener(OnClick);
            } // end if

            if (!dragComponent)
            {
                dragComponent = GetComponent<DragComponent>();
                dragComponent.OnDragBegin.AddListener(OnDragBegin);
                dragComponent.OnDrag.AddListener(OnDrag);
                dragComponent.OnDragEnd.AddListener(OnDragEnd);
            }// end if

        }

        void ClearTiles()
        {
            foreach (var tile in currentTiles)
            {
                tile.PlacedBuilding = null;
            }
            currentTiles.Clear();
        }
        /// <summary>
        /// Set the building data and start the construction
        /// </summary>
        /// <param name="data"></param>
        public void Initialize(CityBuildingData data, Player owner, Vector3 position)
        {
            this.data = data;
            this.owner = owner;
            ConstructionModule constructionModule = gameObject.AddComponent<ConstructionModule>();
            constructionModule.Initialize(data, this);
            constructionModule.AddFinishConstructionAction(LoadModules);
            
            onDragBeginEvent += owner.GameMode.Controller.GameGrid.RemoveBuildingFromTiles;
            onDragEndEvent += owner.GameMode.Controller.GameGrid.AddBuildingToTiles;
            dragComponent.EndDrag(position);
            owner.AddBuilding(this);
            this.gameObject.name += data.Name;
        }
        /// <summary>
        /// Set all the modules to the list and add them as children components
        /// </summary>
        private void LoadModules()
        {
            for (int i = 0; i < data.ModulesData.Count; i++)
            {
                GameObject moduleInstance = Instantiate(data.ModulesData[i].ModulePrefab, transform);
                BaseModule moduleComponent = moduleInstance.GetComponent<BaseModule>();
                moduleComponent.Initialize(data.ModulesData[i], this);
                modules.Add(moduleComponent);
            } /// end for
        }
        /// <summary>
        /// On click event, invokes the event if there is any
        /// </summary>
        public void OnClick()
        {
            owner.GameMode.OnBuildingSelected(this);
            onClickEvent?.Invoke(this);
        }
        public void OnDragBegin()
        {
            transform.position = owner.GameMode.Controller.GameGrid.GetNearestTilePosition(transform.position);
            ClearTiles();
            onDragBeginEvent?.Invoke(this);
        }
        /// <summary>
        /// Drag event, invokes the event if there is any
        /// </summary>
        public void OnDrag()
        {
            transform.position = owner.GameMode.Controller.GameGrid.GetNearestTilePosition(transform.position);
            onDragEvent?.Invoke(this);
        }
        /// <summary>
        /// Drag end event, invokes the event if there is any
        /// </summary>
        public void OnDragEnd()
        {
            transform.position = owner.GameMode.Controller.GameGrid.GetNearestTilePosition(transform.position);
            onDragEndEvent?.Invoke(this);
        }

        private void OnDestroy()
        {
            onDragBeginEvent -= owner.GameMode.Controller.GameGrid.RemoveBuildingFromTiles;
            onDragEndEvent -= owner.GameMode.Controller.GameGrid.AddBuildingToTiles;
        }
    }
}
