using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    public class SettingsMenuManager : PauseMenu
    {
        
        
        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            pauseMenuNavigation.pauseMenuNavigationButtons = pauseMenuNavigationHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
        }

        private void OnEnable()
        {
            pauseMenuNavigation.SetFirstItem();
        }


        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {
                
                if (SceneManager.GetActiveScene().name == "Title Screen")
                {
                    CanvasManager.Instance.pauseMenuManager.gameObject.SetActive(false);
                    TitleScreenManager.Instance.titleScreenState = TitleScreenState.Menu;
                    TitleScreenManager.Instance.SetActiveMenuPanel(TitleScreenManager.Instance.welcomePanel);
                    //EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.System);
                    return;
                }
                
                //skillsMenuNavigationState = SkillsMenuNavigationState.Main;
                EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.Menu);
            }
            
            
            pauseMenuNavigation.NavigationInputUpdate();
        }
    }
}