using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class TitleScreenManager : MenuManager
    {
        public MenuNavigation menuNavigation;
        public TitleScreenState titleScreenState;
        public GameObject titleGameObject;
        public TMP_Text pressAnyButtonText;
        public bool inSelectStartMenuOptions;
        public GameObject menuOptionsHolder;
        public PauseMenuNavigationButton newGameButton;
        public PauseMenuNavigationButton continueGameButton;
        public PauseMenuNavigationButton settingsButton;
        public PauseMenuNavigationButton quitGameButton;

        public List<GameObject> startMenuPanels;
        public GameObject welcomePanel;
        public GameObject loadGamePanel;
        public GameObject settingsPanel;

        public SceneField persistentData;
        public SceneField newGameScene;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Title Screen")
            {
                SceneManager.LoadSceneAsync(persistentData, LoadSceneMode.Additive);
            }
            
            menuNavigation = new MenuNavigation();
            
            menuNavigation.Add(continueGameButton);
            menuNavigation.Add(newGameButton);
            menuNavigation.Add(settingsButton);
            menuNavigation.Add(quitGameButton);
            
            menuNavigation.SetFirstItem();
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
            
            // menuOptionsHolder.SetActive(false);
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 1);
            
            
            
            
            
            

            startMenuPanels = new List<GameObject>();
            
            startMenuPanels.Add(welcomePanel);
            startMenuPanels.Add(loadGamePanel);
            startMenuPanels.Add(settingsPanel);
            
            
            SetActiveMenuPanel(welcomePanel);
            SoundManager.Instance.PlayMusic("Tears of Apathy");
            
            //SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
        }

        private void Update()
        {


            switch (titleScreenState)
            {
                case TitleScreenState.Menu:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }
            
                    menuNavigation.NavigationInputUpdate();
                    break;
            }

            
            
            
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


            if (GameManager.Instance.playerInput.GetButtonDown("Pause"))
            {
                SetActiveMenuPanel(welcomePanel);
            }
            
        }
        
        private void Confirm()
        {
            if (menuNavigation.SelectedNavigationButton == newGameButton)
            {
                NewGame();
            }
            
            if (menuNavigation.SelectedNavigationButton == continueGameButton)
            {
                Continue();
            }
            
            if (menuNavigation.SelectedNavigationButton == settingsButton)
            {
                Settings();
            }
            
            if (menuNavigation.SelectedNavigationButton == quitGameButton)
            {
                Quit();
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
            SoundManager.Instance.PlayButtonClick1();
            DataPersistenceManager.Instance.NewGame();
            GameSceneManager.Instance.fromLoadedData = true;
            // SceneTransitionManager.Instance.isTransitioningScenes = true;
            SceneManager.LoadScene(newGameScene);
        }


        public void Continue()
        {
            SoundManager.Instance.PlayButtonClick1();
            titleScreenState = TitleScreenState.LoadGame;
            SetActiveMenuPanel(loadGamePanel);
        }

        public void Settings()
        {
            SoundManager.Instance.PlayButtonClick1();
            titleScreenState = TitleScreenState.Settings;
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