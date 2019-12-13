using System;
using UnityEngine;

namespace CityBuilder.GameEntities
{
    /// <summary>
    /// Data representation of a Tile in the grid
    /// </summary>
    [Serializable]
    public class GridTile
    {
        /// <summary>
        /// Position of the tile in the rows
        /// </summary>
        private int row;
        /// <summary>
        /// Position of the tile in the column
        /// </summary>
        private int column;

        private Vector3 position;

        private CityBuilding placedBuilding;
        /// <summary>
        /// Grid Tile constructor
        /// </summary>
        /// <param name="row">Tile row</param>
        /// <param name="column">Tile column</param>
        public GridTile(int row, int column, Vector3 position)
        {
            this.row = row;
            this.column = column;
            this.position = position;
            placedBuilding = null;
        }
        /// <summary>
        /// Accessor for the tile row
        /// </summary>
        public int Row
        {
            get { return row; }
        }
        /// <summary>
        /// Accessor for the tile column
        /// </summary>
        public int Column
        {
            get { return column; }
        }

        public Vector3 Position
        {
            get { return position; }
        }
        /// <summary>
        /// Accessor for the Building Reference in this tile
        /// </summary>
        public CityBuilding PlacedBuilding
        {
            set { placedBuilding = value; }
            get { return placedBuilding; }
        }

        public bool IsAvailable
        {
            get { return placedBuilding == null; }
        }

    }
}


