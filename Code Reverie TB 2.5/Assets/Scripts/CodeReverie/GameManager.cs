using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    [DefaultExecutionOrder(-120)]
    public class GameManager : ManagerSingleton<GameManager>, IDataPersistence
    {

        public Player playerInput;
        public int playerId = 0;
        public bool isPaused;
        public bool inventoryActive;
        public ControlSchemeType currentControlScheme;
        public bool newGame;

        protected override void Awake()
        {
            base.Awake();
            currentControlScheme = ControlSchemeType.KeyboardMouse;
            playerInput = ReInput.players.GetPlayer(0);


            if (SceneManager.GetActiveScene().name != "Title Screen")
            {
                playerInput.controllers.maps.SetAllMapsEnabled(false);
                playerInput.controllers.maps.SetMapsEnabled(true, 0);
            }
            else
            {
                playerInput.controllers.maps.SetAllMapsEnabled(false);
                playerInput.controllers.maps.SetMapsEnabled(true, 1);
            }

           
            
        }

        // Start is called before the first frame update
        void Start()
        {
            // playerInput = ReInput.players.GetPlayer(playerId);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (playerInput.GetButtonDown("Pause"))
            {
                SetPauseState();
            }
            
            
            if (SceneManager.GetActiveScene().name != "Title Screen")
            {
                GetComponent<DisplayTime>().timerIsRunning = true;
            }
            else
            {
                GetComponent<DisplayTime>().timerIsRunning = false;
            }

        }
        
        
        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        
        public void SetPauseState()
        {
            isPaused = !isPaused;
            EventManager.Instance.generalEvents.OnGamePause(isPaused);
            // gamePaused = !gamePaused;
            // gamePausedAction?.Invoke(gamePaused);
            
            if (isPaused)
            {
                
                Time.timeScale = 0;
                playerInput.controllers.maps.SetAllMapsEnabled(false);
                playerInput.controllers.maps.SetMapsEnabled(true, 1);
                
            }
            else
            {

                if (CombatManager.Instance != null)
                {
                    Time.timeScale = 1;
                    playerInput.controllers.maps.SetAllMapsEnabled(false);
                    playerInput.controllers.maps.SetMapsEnabled(true, 2);
                }
                else
                {
                    Time.timeScale = 1;
                    playerInput.controllers.maps.SetAllMapsEnabled(false);
                    playerInput.controllers.maps.SetMapsEnabled(true, 0);
                }
                
                
            }
        }

        public void LoadData(string dataSlot)
        {
            
        }

        public void SaveData(string dataSlot)
        {
            
        }
    }
}
