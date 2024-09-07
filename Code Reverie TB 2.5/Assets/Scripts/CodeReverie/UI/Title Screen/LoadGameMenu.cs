using System;
using System.Linq;
using Sirenix.OdinInspector;

namespace CodeReverie
{
    public class LoadGameMenu : SerializedMonoBehaviour
    {
        public MenuNavigation menuNavigation;
        public LoadGameMenuState loadGameMenuState;

        private void Awake()
        {
            menuNavigation = new MenuNavigation();
            
            menuNavigation.pauseMenuNavigationButtons = GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            
            menuNavigation.SetFirstItem();
        }

        private void Update()
        {

            //todo make better in the future (Maybe use events)
            if (CanvasManager.Instance.loadDataConfirmationPopup.isActiveAndEnabled)
            {
                loadGameMenuState = LoadGameMenuState.LoadGame;
            }
            else
            {
                loadGameMenuState = LoadGameMenuState.Menu;
            }
            
            
            switch (loadGameMenuState)
            {
                case LoadGameMenuState.Menu:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }

                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        gameObject.SetActive(false);
                        TitleScreenManager.Instance.titleScreenState = TitleScreenState.Menu;
                        TitleScreenManager.Instance.SetActiveMenuPanel(TitleScreenManager.Instance.welcomePanel);
                    }

                    menuNavigation.NavigationInputUpdate();
                    break;
            }
            
        }
        
        
        private void Confirm()
        {
            menuNavigation.SelectedNavigationButton.GetComponent<LoadSlotButton>().LoadGame();
        }
    }
}