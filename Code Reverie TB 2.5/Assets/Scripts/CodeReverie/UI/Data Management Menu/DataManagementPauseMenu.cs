using System;
using System.Linq;
using UnityEngine;

namespace CodeReverie
{
    public class DataManagementPauseMenu : PauseMenu
    {
        public DataManagementMenuState dataManagementMenuState;
        public PauseMenuNavigationButton saveButton;
        public PauseMenuNavigationButton loadButton;
        public GameObject savePanel;
        public GameObject saveButtonHolder;
        public GameObject loadPanel;
        public GameObject loadButtonHolder;
        
        
        private MenuNavigation saveMenuNavigation;
        private MenuNavigation loadMenuNavigation;
        
        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            saveMenuNavigation = new MenuNavigation();
            loadMenuNavigation = new MenuNavigation();
            pauseMenuNavigation.pauseMenuNavigationButtons = pauseMenuNavigationHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            pauseMenuNavigation.SetFirstItem();
        }

        private void OnEnable()
        {
            savePanel.SetActive(false);
            loadPanel.SetActive(false);
            //pauseMenuNavigation.SetFirstItem();
        }


        private void Update()
        {


            switch (dataManagementMenuState)
            {
                case DataManagementMenuState.Menu:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }
                
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.Menu);
                    }
                
                    pauseMenuNavigation.NavigationInputUpdate();
                    break;
                case DataManagementMenuState.Save:
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        savePanel.SetActive(false);
                        loadPanel.SetActive(false);
                        dataManagementMenuState = DataManagementMenuState.Menu;
                    }
                    
                    saveMenuNavigation.NavigationInputUpdate();
                    break;
                case DataManagementMenuState.Load:
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        savePanel.SetActive(false);
                        loadPanel.SetActive(false);
                        dataManagementMenuState = DataManagementMenuState.Menu;
                    }
                    loadMenuNavigation.NavigationInputUpdate();
                    break;
                    
            }
            
            
            
        }
        
        
        public void Confirm()
        {
            if (pauseMenuNavigation.SelectedNavigationButton == saveButton)
            {
                saveMenuNavigation.pauseMenuNavigationButtons = saveButtonHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
                saveMenuNavigation.SetFirstItem();
                savePanel.SetActive(true);
                loadPanel.SetActive(false);
                dataManagementMenuState = DataManagementMenuState.Save;
            }
            
            if (pauseMenuNavigation.SelectedNavigationButton == loadButton)
            {
                
                loadMenuNavigation.pauseMenuNavigationButtons = loadButtonHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
                loadMenuNavigation.SetFirstItem();
                
                savePanel.SetActive(false);
                loadPanel.SetActive(true);
                dataManagementMenuState = DataManagementMenuState.Load;
            }
        }
        
       
        
    }
}