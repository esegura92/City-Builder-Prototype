using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.GameEntities
{
    /// <summary>
    /// Grid where the player place their buildings
    /// </summary>
    public class CityGrid : MonoBehaviour
    {
        /// <summary>
        /// Grid scale based in Unity units
        /// </summary>
        [SerializeField]
        private float scale;
        /// <summary>
        /// Width of the grid
        /// </summary>
        [SerializeField]
        private float width;
        /// <summary>
        /// Height of the grid
        /// </summary>
        [SerializeField]
        private float height;
        /// <summary>
        /// Grid game object
        /// </summary>
        [SerializeField]
        private GameObject gridObject;
        /// <summary>
        /// Dictionary of grit tiles with their respective indexes
        /// </summary>
        private Dictionary<string, GridTile> tiles;
        /// <summary>
        /// Accessor for the grid scale
        /// </summary>
        public float Scale
        {
            get { return scale; }
        }
        /// <summary>
        /// Accessor for the grid width
        /// </summary>
        public float Width
        {
            get { return width; }
        }
        /// <summary>
        /// Accessor for the grid height
        /// </summary>
        public float Height
        {
            get { return height; }
        }

        /// <summary>
        /// Set the reference to the gridObject if it's not placed
        /// </summary>
        private void Awake()
        {
            tiles = new Dictionary<string, GridTile>();
            if(!gridObject)
            {
                gridObject = gameObject;
            }
        }

        /// <summary>
        /// Start is called before the first frame update
        /// We set the grid dimensions here
        /// </summary>
        private void Start()
        {
            SetGridDimentions();
        }
        /// <summary>
        /// Sets the grid dimensions based on the scale
        /// </summary>
        private void SetGridDimentions()
        {
            gridObject.transform.localScale = new Vector3(width, gridObject.transform.localScale.y, height);
            for(int row = 0; row < width; row++)
            {
                for(int col = 0; col < height; col++ )
                {
                    GridTile tile = new GridTile(row, col, CalculateTilePosition(row, col));

                    string tileKey = GenerateTileDictionaryKey(row, col);
                    tiles.Add(tileKey, tile);
                } // end for
            } // end for
        }

        private Vector3 CalculateTilePosition(int row, int column)
        {
            float x = (scale * (row - (width / 2))) + transform.position.x;
            float z = (scale * (column - (height / 2))) + transform.position.z;
            return new Vector3(x, transform.position.y, z);
        }

        public void AddBuildingToTiles(CityBuilding building)
        {
            Vector3 buildingPosition = building.transform.position;
            GridTile centerTile = GetTileByPosition(buildingPosition);
            List<string> neighborsKeys = GetNeightborsTilesKeys(centerTile, building.Width, building.Height);
            List<GridTile> buildingTiles = new List<GridTile>();
            for (int i = 0; i < neighborsKeys.Count; i++)
            {
                //Debug.LogError("building added on tiles " + neighborsKeys[i]);
                GridTile tile = tiles[neighborsKeys[i]];
                tile.PlacedBuilding = building;
                buildingTiles.Add(tile);
            } // end for
            building.CurrentTiles = buildingTiles;
        }

        public void RemoveBuildingFromTiles(CityBuilding building)
        {
            Vector3 buildingPosition = building.transform.position;
            //Vector3 tilePos = GetNearestTilePosition(buildingPosition);
            GridTile centerTile = GetTileByPosition(buildingPosition);
            List<string> neighborsKeys = GetNeightborsTilesKeys(centerTile, building.Width, building.Height);
            for (int i = 0; i < neighborsKeys.Count; i++)
            {
                tiles[neighborsKeys[i]].PlacedBuilding = null;
            } // end for


        }

        public bool IsGridSpaceAvailable(Vector3 position, int width, int height)
        {
            bool result = true;
            GridTile centerTile = GetTileByPosition(position);
            //Debug.LogError(GenerateTileDictionaryKey(centerTile.Row, centerTile.Column) + "Pos " + position);
            List<string> neighborsKeys = GetNeightborsTilesKeys(centerTile, width, height);
            if(neighborsKeys.Count > 0)
            {
                for(int i = 0; i < neighborsKeys.Count && result; i++)
                {
                    if(!tiles[neighborsKeys[i]].IsAvailable)
                    {
                        result = false;
                    } // end if
                } // end for
            }
            else
            {
                Debug.LogError("muere aqui sin vecinos");
                result = false;
            }
            
            return result;
        }
        /// <summary>
        /// Return the neighboor keys, if the list its empty it cant get neighboors cause the tile is close to the border and the dimensions are bigger
        /// </summary>
        /// <param name="centerTile"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private List<string> GetNeightborsTilesKeys(GridTile centerTile, int width, int height)
        {
            List<string> keys = new List<string>();

            int minRowIndex = centerTile.Row;
            int minColIndex = centerTile.Column;
            int maxRowIndex = centerTile.Row + width;
            int maxColIndex = centerTile.Column + height;
            //Debug.LogError(GenerateTileDictionaryKey(minRowIndex, minColIndex) + GenerateTileDictionaryKey(maxRowIndex, maxColIndex));
            bool keyExists = true;

            for (int i = minRowIndex; i < maxRowIndex && keyExists; i++)
            {
                for (int j = minColIndex; j < maxColIndex && keyExists; j++)
                {
                    string key = GenerateTileDictionaryKey(i, j);
                    if (tiles.ContainsKey(key))
                    {
                        keys.Add(key);
                    } // end if
                    else
                    {
                        keyExists = false;
                        keys.Clear();
                    }
                } // end for
            } // end for

            return keys;
        }
        /// <summary>
        /// Obtains a tile based on a position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GridTile GetTileByPosition(Vector3 position)
        {
            float distance = 1000000.0f;
            string tileKey = "";
            foreach (KeyValuePair<string, GridTile> tile in tiles)
            {
                float currentDistance = Vector3.Distance(tile.Value.Position, position);
                if(currentDistance < distance)
                {
                    distance = currentDistance;
                    tileKey = tile.Key;
                } // end if
            }
            /*
            int row = Mathf.RoundToInt(((transform.position.x - position.x) / scale) - (width / 2));
            int column = Mathf.RoundToInt(((transform.position.z - position.z) / scale) - (height / 2));
            Debug.LogError("The real" + GenerateTileDictionaryKey(row, column));
            if (row < 0) row = 0;
            if (column < 0) column = 0;
            if (row > width) row = Mathf.RoundToInt(width);
            if (column > height) column = Mathf.RoundToInt(height);
            string tileKey = GenerateTileDictionaryKey(row, column);
            */
            return tiles[tileKey];
        }
        /// <summary>
        /// Get the nearest position of a tile given a position position
        /// </summary>
        /// <param name="position">Current position</param>
        /// <returns>Nearest tile position</returns>
        public Vector3 GetNearestTilePosition(Vector3 position)
        {
            GridTile nearestTile = GetTileByPosition(position);
            //float xPos = Mathf.RoundToInt(transform.position.x - (scale * (nearestTile.Row * (width / 2))));
            //float zPos = Mathf.RoundToInt(transform.position.z - (scale * (nearestTile.Row * (height / 2))));
            return nearestTile.Position;
        }
            
        /// <summary>
        /// Generates the dictionary key format with the row and column
        /// </summary>
        /// <param name="row">Tile row</param>
        /// <param name="column">Tile column</param>
        /// <returns></returns>
        private string GenerateTileDictionaryKey(int row, int column)
        {
            //Debug.LogError("ASked key " + "[" + row.ToString() + "," + column.ToString() + "]");
            return "[" + row.ToString() + "," + column.ToString() + "]";
        }

        void OnDrawGizmos()
        {
            if (tiles != null)
            {
                int i = 0;
                foreach (var tile in tiles)
                {
                    if (!tile.Value.IsAvailable)
                    {
                        if(i == 0)
                            Gizmos.color = Color.red;
                        if (i == 1)
                            Gizmos.color = Color.blue;
                        if (i == 2)
                            Gizmos.color = Color.black;
                        if (i == 3)
                            Gizmos.color = Color.gray;
                        if (i == 4)
                            Gizmos.color = Color.green;
                        if (i == 5)
                            Gizmos.color = Color.magenta;
                        Gizmos.DrawSphere(tile.Value.Position, 2);
                        i++;
                    }
                } // end for
            } // end if
                
        }

    }
}
