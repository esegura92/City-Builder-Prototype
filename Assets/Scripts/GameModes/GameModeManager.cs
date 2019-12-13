using CityBuilder.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.GameModes
{
    
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField]
        private GameMode currentGameMode;
        [SerializeField]
        private GameObject gameModeContainer;
        [SerializeField]
        private GameController owner;
        // Start is called before the first frame update
        public GameMode CurrentGameMode
        {
            get { return currentGameMode; }
        }

        void Awake()
        {
            
        }

        public BaseGameMode SetGameMode(GameMode gameMode)
        {
            BaseGameMode mode = null ;
            Destroy(gameModeContainer.GetComponent<BaseGameMode>());
            switch(gameMode)
            {
                case GameMode.BuildMode:
                    {
                        mode = gameModeContainer.AddComponent<BuildGameMode>();
                    }
                    break;
                case GameMode.RegularMode:
                    mode = gameModeContainer.AddComponent<RegularGameMode>();
                    break;
            } // end switch
            return mode;
        }
    }
}


