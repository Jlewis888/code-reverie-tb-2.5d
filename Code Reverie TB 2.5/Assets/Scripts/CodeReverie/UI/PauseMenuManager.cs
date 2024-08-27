using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeReverie
{
    public class PauseMenuManager : MenuManager
    {
        public PauseMenuNavigationState pauseMenuNavigationState;
        public PauseMenuNavigationState pauseMenuNavigationPreviousState;
        public GameObject pauseMenuNavigationHolder;
       
        
        private PauseMenu selectedPauseMenu;
        private List<PauseMenu> pauseMenus;

        public CurrentPartyMenuManager currentPartyMenuManager;
        public MenuNavigation pauseMenuNavigation;

        public GameObject characterSidePanel;
        

        private void Awake()
        {
            // pauseMenuNavigation = new MenuNavigation();
            // pauseMenuNavigation.pauseMenuNavigationButtons = pauseMenuNavigationHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            //pauseMenuNavigation.onSelectedNavigationButtonChangeDelegate = OnSelectedNavigationButtonChange;
            //pauseMenuNavigation.callBack = OnSelectedNavigationButtonChange;
        }
        
      

        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onPauseMenuNavigationStateChange += OnPauseMenuNavigationStateChange;
            EventManager.Instance.generalEvents.toggleCharacterSidePanelUI += ToggleCharacterSidePanelUI;
            
            //pauseMenuNavigation.SetFirstItem();
            
            List<PauseMenu> pauseMenus = transform.GetComponentsInChildren<PauseMenu>(true).ToList();

            foreach (PauseMenu pauseMenu in pauseMenus)
            {
                if (pauseMenu.GetComponent<MainMenuPauseMenu>())
                {
                    if (pauseMenu.GetComponent<MainMenuPauseMenu>().pauseMenuNavigation != null)
                    {
                        pauseMenu.GetComponent<MainMenuPauseMenu>().pauseMenuNavigation.SetFirstItem();
                    }
                }
                
                pauseMenu.SetListeners();
            }
            characterSidePanel.SetActive(true);
            currentPartyMenuManager.SetParty();
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.Menu);
            EventManager.Instance.generalEvents.onPauseMenuNavigationStateChange -= OnPauseMenuNavigationStateChange;
            EventManager.Instance.generalEvents.toggleCharacterSidePanelUI -= ToggleCharacterSidePanelUI;
            
            List<PauseMenu> pauseMenus = GetComponentsInChildren<PauseMenu>(true).ToList();

            foreach (PauseMenu pauseMenu in pauseMenus)
            {
                pauseMenu.UnsetListeners();
            }
        }
        
        
        public PauseMenu SelectedPauseMenu
        {
            get => selectedPauseMenu;

            set
            {
                if (selectedPauseMenu != value)
                {
                    selectedPauseMenu = value;
                }

                
                
                //SetMenuManagers();
                //onMenuChange?.Invoke(selectedMenuManager);

            }
        }
        
        
        public void SetMenuManagers()
        {
            
            SelectedPauseMenu.gameObject.SetActive(true);
            
            foreach (PauseMenu pauseMenu in pauseMenus)
            {
                if (pauseMenu != SelectedPauseMenu)
                {
                    pauseMenu.gameObject.SetActive(false);
                }
            }
        }

        public void OnPauseMenuNavigationStateChange(PauseMenuNavigationState pauseMenuNavigationState)
        {
            if (pauseMenuNavigationState == PauseMenuNavigationState.Menu)
            {
                EventManager.Instance.generalEvents.ToggleCharacterSidePanelUI(true);
            }
            
            this.pauseMenuNavigationState = pauseMenuNavigationState;
        }
        
        public void ToggleCharacterSidePanelUI(bool setActive)
        {
            characterSidePanel.SetActive(setActive);
        }
        
    }
}