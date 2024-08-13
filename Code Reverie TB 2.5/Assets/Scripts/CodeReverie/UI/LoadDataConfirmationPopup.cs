using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class LoadDataConfirmationPopup : SerializedMonoBehaviour
    {
        public MenuNavigation menuNavigation;
        public PauseMenuNavigationButton confirmationButton;
        public PauseMenuNavigationButton exitButton;
        public int gameSlot;

        public void Awake()
        {
            
            menuNavigation = new MenuNavigation();
            
            menuNavigation.Add(confirmationButton);
            menuNavigation.Add(exitButton);
            
            //menuNavigation.pauseMenuNavigationButtons = GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            
        }

        private void OnEnable()
        {
            menuNavigation.SetFirstItem();
        }

        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
            {
                Confirm();
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {
                Cancel();
            }
            
            menuNavigation.NavigationInputUpdate();
        }


        public void LoadGame()
        {
            Debug.Log("SHould not be working here 222");
            if (DataPersistenceManager.Instance.SaveFileExist(gameSlot))
            {
                
                DataPersistenceManager.Instance.LoadGame(gameSlot);
                GameSceneManager.Instance.fromLoadedData = true;

                if (ES3.KeyExists("currentScene", $"{gameSlot}/SaveFile.es3"))
                {
                    string scene = ES3.Load<string>("currentScene", $"{gameSlot}/SaveFile.es3");
                    SceneManager.LoadScene(scene);
                    gameObject.SetActive(false);
                }
                
            }
        }

        void Cancel()
        {
            gameObject.SetActive(false);
        }
        
        private void Confirm()
        {
            
            if (menuNavigation.SelectedNavigationButton == confirmationButton)
            {
                Debug.Log("working here");
                LoadGame();
            }
            if (menuNavigation.SelectedNavigationButton == exitButton)
            {
                Cancel();
            }
            
            
            
            //menuNavigation.SelectedNavigationButton.GetComponent<LoadSlotButton>().LoadGame();
        }
        
        
    }
}