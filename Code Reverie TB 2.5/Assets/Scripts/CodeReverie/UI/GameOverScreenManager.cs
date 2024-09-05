using System;
using TransitionsPlus;
using UnityEngine.PlayerLoop;

namespace CodeReverie
{
    public class GameOverScreenManager : PauseMenu
    {
        public PauseMenuNavigationButton retryFromLastBattleButton;
        public PauseMenuNavigationButton lastSavePointButton;
        public PauseMenuNavigationButton returnToTitleScreenButton;


        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            
            pauseMenuNavigation.Add(retryFromLastBattleButton);
            pauseMenuNavigation.Add(lastSavePointButton);
            pauseMenuNavigation.Add(returnToTitleScreenButton);
        }

        private void OnEnable()
        {
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 1);
            
            TransitionAnimator transitionAnimator = TransitionAnimator.Start(
                TransitionType.DoubleWipe, // transition type
                duration: 2f,
                rotation: 90f,
                invert: true
            );
            pauseMenuNavigation.SetFirstItem();
        }

        private void Update()
        {
            pauseMenuNavigation.NavigationInputUpdate();
        }
    }
}