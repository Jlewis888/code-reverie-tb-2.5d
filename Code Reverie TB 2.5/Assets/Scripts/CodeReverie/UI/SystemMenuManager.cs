using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SystemMenuManager : MenuManager
    {
        public GameObject titleGameObject;
        public TMP_Text pressAnyButtonText;
        public bool inSelectStartMenuOptions;
        public GameObject menuOptionsHolder;
        // public PauseMenuNavigationButton newGameButton;
        // public PauseMenuNavigationButton continueGameButton;
        // public PauseMenuNavigationButton settingsButton;
        // public PauseMenuNavigationButton quitGameButton;
        
        public Button saveGameButton;
        public Button loadGameButton;
        public Button settingsButton;
        public Button quitGameButton;

        public List<GameObject> startMenuPanels;


        public GameObject saveGamePanel;
        public GameObject loadGamePanel;
        public GameObject settingsPanel;
        

        private void Awake()
        {
            // menuOptionsHolder.SetActive(false);
            // GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            // GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 1);
            //
            
           
            saveGameButton.onClick.AddListener(SaveGame);
            loadGameButton.onClick.AddListener(LoadGame);
            settingsButton.onClick.AddListener(Settings);
            quitGameButton.onClick.AddListener(Quit);

            startMenuPanels = new List<GameObject>();
            
            
            startMenuPanels.Add(saveGamePanel);
            startMenuPanels.Add(loadGamePanel);
            startMenuPanels.Add(settingsPanel);
            
            
            SetActiveMenuPanel(saveGamePanel);
            
            //SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
        }

        private void Start()
        {
            // DataPersistenceManager.Instance.LoadGame("0");
            //
            // if (!DataPersistenceManager.Instance.HasGameData())
            // {
            //     continueGameButton.gameObject.SetActive(false);
            //     pauseMenuNavigationButtons.Remove(continueGameButton);
            // }
        }

        private void Update()
        {

            if (!inSelectStartMenuOptions)
            {
                // if (GameManager.Instance.playerInput.GetAnyButtonDown() || Input.anyKeyDown)
                // {
                //     //inSelectStartMenuOptions = true;
                //     MoveTitleObject();
                // }
            }
            else
            {
                // pressAnyButtonText.gameObject.SetActive(false);
                // menuOptionsHolder.gameObject.SetActive(true);
                
                // if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                // {
                //     OnMenuOptionSelect();
                // }
                
                // if (navigationDelayTimer <= 0)
                // {
                //     if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical") < 0)
                //     {
                //         navigationDelayTimer = navigationDelay;
                //         if (navigationButtonsIndex + 1 > pauseMenuNavigationButtons.Count - 1)
                //         {
                //             navigationButtonsIndex = 0;
                //         }
                //         else
                //         {
                //             navigationButtonsIndex++;
                //         }
                //
                //         SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
                //     }
                //
                //     if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical") > 0)
                //     {
                //         navigationDelayTimer = navigationDelay;
                //         if (navigationButtonsIndex == 0)
                //         {
                //             navigationButtonsIndex = pauseMenuNavigationButtons.Count - 1;
                //         }
                //         else
                //         {
                //             navigationButtonsIndex--;
                //         }
                //
                //         SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
                //     }
                // }
                // else
                // {
                //     navigationDelayTimer -= Time.unscaledDeltaTime;
                // }
            }
            
        }


        public void MoveTitleObject()
        {
            LeanTween.cancel(titleGameObject);
            titleGameObject.transform.LeanMoveLocalY(titleGameObject.transform.localPosition.y + 233, 0.1f).setOnComplete(
                () =>
                {
                    inSelectStartMenuOptions = true;
                });
        }


        public void NewGame()
        {
            //Debug.Log("Start New Game");
            DataPersistenceManager.Instance.NewGame();
            GameSceneManager.Instance.fromLoadedData = true;
            // SceneTransitionManager.Instance.isTransitioningScenes = true;
            SceneManager.LoadScene("Wetlands Town");
        }

        public void SaveGame()
        {
            SetActiveMenuPanel(saveGamePanel);
        }

        public void LoadGame()
        {
            SetActiveMenuPanel(loadGamePanel);
        }

        public void Continue()
        {
            //SceneManager.LoadScene("LoadSaveScene");
            Debug.Log("Continue Game");
        }

        public void Settings()
        {
            //SceneManager.LoadScene("SettingsScene");
            SetActiveMenuPanel(settingsPanel);
        }

        public void Quit()
        {
           
            Application.Quit();
        }


        public void SetActiveMenuPanel(GameObject panel)
        {
            foreach (var menuPanel in startMenuPanels)
            {
                if (panel == menuPanel)
                {
                    menuPanel.SetActive(true);
                }
                else
                {
                    menuPanel.SetActive(false);
                }
            }
        }


        // public void OnMenuOptionSelect()
        // {
        //     if (selectedNavigationButton == continueGameButton)
        //     {
        //         ContinueButton();
        //     } 
        //     else if (selectedNavigationButton == newGameButton)
        //     {
        //        NewGameButton();
        //     }
        //     else if (selectedNavigationButton == settingsButton)
        //     {
        //         SettingButton();
        //     }
        //     else if (selectedNavigationButton == quitGameButton)
        //     {
        //         QuitButton();
        //     }
        // }
    }
}