using System;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SaveDataConfirmationPopup : SerializedMonoBehaviour
    {
        public MenuNavigation menuNavigation;
        public PauseMenuNavigationButton confirmationButton;
        public PauseMenuNavigationButton exitButton;
        public int gameSlot;
        public TMP_Text saveMessageText;
        
        
        public void Awake()
        {
            menuNavigation = new MenuNavigation();
            menuNavigation.Add(confirmationButton);
            menuNavigation.Add(exitButton);
            //menuNavigation.pauseMenuNavigationButtons = GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            
            //menuNavigation.SetFirstItem();
            
            // confirmationButton.onClick.AddListener(SaveGame);
            // exitButton.onClick.AddListener(() =>
            // {
            //     Debug.Log("Close the popup");
            //     gameObject.SetActive(false);
            // });
        }

        private void OnEnable()
        {
            menuNavigation.SetFirstItem();
            
            if (DataPersistenceManager.Instance.SaveFileExist(gameSlot))
            {
                saveMessageText.text = "Overwrite existing game?";
            }
            else
            {
                saveMessageText.text = "Save your game in this slot?";
            }
        }

        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
            {
                Confirm();
            }
            
            menuNavigation.NavigationInputUpdate();
        }


        public void SaveGame()
        {
            DataPersistenceManager.Instance.SaveGame(gameSlot);
            gameObject.SetActive(false);
        }
        
        private void Confirm()
        {
            
            if (menuNavigation.SelectedNavigationButton == confirmationButton)
            {
                SaveGame();
            }
            else
            {
                gameObject.SetActive(false);
            }
            
            
            
            //menuNavigation.SelectedNavigationButton.GetComponent<LoadSlotButton>().LoadGame();
        }
    }
}