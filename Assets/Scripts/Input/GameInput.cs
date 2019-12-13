using CityBuilder.GameEntities;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.Input
{
    public class GameInput : MonoBehaviour
    {
        
     
        /// <summary>
        /// Checks and returns all the raycast interactions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <returns></returns>
        /// 
        public static List<T> RaycastInteraction<T>(Vector3 position, bool isScreenPos = true)
        {
            Vector3 startPos = (isScreenPos) ? Camera.main.ScreenToWorldPoint(position) : position;
            List<T> interactions = new List<T>();
            Debug.DrawRay(startPos, Camera.main.transform.forward * 100.0f, Color.red, 0.1f);
            RaycastHit[] hits = Physics.RaycastAll(startPos, Camera.main.transform.forward, Mathf.Infinity);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.GetComponent<CityGrid>())
                {
                    CityGrid grid = hits[i].collider.gameObject.GetComponent<CityGrid>();
                    Vector3 tilepos  =grid.GetNearestTilePosition(hits[i].point);
                    GridTile tile = grid.GetTileByPosition(tilepos);
                    //Debug.LogError("Tile [" + tile.Row.ToString() + "," + tile.Column.ToString() + "] at position: " + tilepos);
                    if(!tile.IsAvailable)
                    {
                        T interactableObject = tile.PlacedBuilding.gameObject.GetComponent<T>();
                        if (interactableObject != null)
                        {
                            interactions.Add(interactableObject);
                        } // end if
                    } // end if
                } // end if
            } // end for
            return interactions;
        }

        public static bool IsValidPosition(Vector3 position, int width, int height, bool isScreenPos = true)
        {
            bool isValidPosition = false;
            Vector3 startPos = (isScreenPos) ? Camera.main.ScreenToWorldPoint(position) : position;
            Debug.DrawRay(startPos, Camera.main.transform.forward * 100.0f, Color.red, 0.1f);
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenToWorldPoint(position), Camera.main.transform.forward, Mathf.Infinity);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.GetComponent<CityGrid>())
                {
                    CityGrid grid = hits[i].collider.gameObject.GetComponent<CityGrid>();
                    isValidPosition = grid.IsGridSpaceAvailable(hits[i].point, width, height);
                } // end if
            } // end for
            return isValidPosition;
        }

        public static Vector3 CalculatePositionInGame(Vector3 position, bool isScreenPos = true)
        {

            Vector3 gridPosition = (isScreenPos) ? Camera.main.ScreenToWorldPoint(position) :position;
            Vector3 startPos = gridPosition;
            Debug.DrawRay(startPos, Camera.main.transform.forward * 100.0f, Color.red, 0.1f);
            RaycastHit[] hits = Physics.RaycastAll(startPos, Camera.main.transform.forward, Mathf.Infinity);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.GetComponent<CityGrid>())
                {
                    CityGrid grid = hits[i].collider.gameObject.GetComponent<CityGrid>();
                    
                    Vector3 tilepos = grid.GetNearestTilePosition(hits[i].point);
                    GridTile tile = grid.GetTileByPosition(tilepos);
                    gridPosition = tilepos;
                } // end if
            } // end for
            gridPosition.y = 0;
            return gridPosition;
        }
    }
}


