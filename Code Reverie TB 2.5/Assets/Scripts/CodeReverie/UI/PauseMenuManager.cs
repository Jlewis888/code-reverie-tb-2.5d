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
        public int navigationButtonsIndex;
        
        public float navigationDelay = 0.35f;
        public float navigationDelayTimer;
        
        private PauseMenu selectedPauseMenu;
        private List<PauseMenu> pauseMenus;

        public CurrentPartyMenuManager currentPartyMenuManager;
        public MenuNavigation pauseMenuNavigation;

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            pauseMenuNavigation.pauseMenuNavigationButtons = pauseMenuNavigationHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            //pauseMenuNavigation.onSelectedNavigationButtonChangeDelegate = OnSelectedNavigationButtonChange;
            //pauseMenuNavigation.callBack = OnSelectedNavigationButtonChange;
        }
        
      

        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onPauseMenuNavigationStateChange += OnPauseMenuNavigationStateChange;
            
            pauseMenuNavigation.SetFirstItem();
            
            List<PauseMenu> pauseMenus = transform.GetComponentsInChildren<PauseMenu>(true).ToList();

            foreach (PauseMenu pauseMenu in pauseMenus)
            {
                pauseMenu.SetListeners();
            }
            
            currentPartyMenuManager.SetParty();
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.Menu);
            EventManager.Instance.generalEvents.onPauseMenuNavigationStateChange -= OnPauseMenuNavigationStateChange;
            
            List<PauseMenu> pauseMenus = GetComponentsInChildren<PauseMenu>(true).ToList();

            foreach (PauseMenu pauseMenu in pauseMenus)
            {
                pauseMenu.UnsetListeners();
            }
        }

        private void Update()
        {


            if (pauseMenuNavigationState == PauseMenuNavigationState.Menu)
            {
                if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                {
                    Confirm();
                }
                
                if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                {
                    GameManager.Instance.SetPauseState();
                }
                
                pauseMenuNavigation.NavigationInputUpdate();
                
            }
        }

        public void Confirm()
        {
            if (pauseMenuNavigationState == PauseMenuNavigationState.Menu)
            {

                if (pauseMenuNavigation.SelectedNavigationButton.canNavigate)
                {
                    EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(pauseMenuNavigation.SelectedNavigationButton
                        .pauseMenuNavigationState);
                }
                
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

                
                
                SetMenuManagers();
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
            this.pauseMenuNavigationState = pauseMenuNavigationState;
        }
        
    }
}