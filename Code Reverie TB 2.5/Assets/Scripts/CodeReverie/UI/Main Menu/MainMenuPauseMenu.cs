using System;
using System.Linq;

namespace CodeReverie
{
    public class MainMenuPauseMenu : PauseMenu
    {
        
        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            pauseMenuNavigation.pauseMenuNavigationButtons = pauseMenuNavigationHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            pauseMenuNavigation.SetFirstItem();
        }

        private void OnEnable()
        {
            //pauseMenuNavigation.SetFirstItem();
        }


        private void Update()
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
        
        
        public void Confirm()
        {
            if (pauseMenuNavigation.SelectedNavigationButton.canNavigate)
            {
                EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(pauseMenuNavigation.SelectedNavigationButton
                    .pauseMenuNavigationState);
            }
        }
    }
}