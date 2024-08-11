using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class MenuNavigation
    {
        public List<PauseMenuNavigationButton> pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
        public List<PauseMenuNavigationButton> filteredPauseMenuNavigationButtonList;
        public PauseMenuNavigationButton selectedNavigationButton;
        public GameObject pauseMenuNavigationHolder;
        public int navigationButtonsIndex;

        public float navigationDelay = 0.35f;
        public float navigationDelayTimer;
        //public Delegate onSelectedNavigationButtonChangeDelegate;
        public Action callBack;

        public MenuNavigation()
        {
            pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
        }
        

        public PauseMenuNavigationButton SelectedNavigationButton
        {
            get => selectedNavigationButton;

            set
            {
                if (selectedNavigationButton == value)
                {
                    return;
                }

                selectedNavigationButton = value;

                PauseMenuNavigationUpdate();


                selectedNavigationButton.selector.SetActive(true);

                OnSelectedNavigationButtonChange();
            }
        }

        public void PauseMenuNavigationUpdate()
        {
            foreach (PauseMenuNavigationButton pauseMenuNavigationButton in pauseMenuNavigationButtons)
            {
                if (SelectedNavigationButton != pauseMenuNavigationButton)
                {
                    pauseMenuNavigationButton.selector.SetActive(false);
                }
                else
                {
                    pauseMenuNavigationButton.selector.SetActive(true);
                }
            }

            selectedNavigationButton.selector.SetActive(true);
        }
        
        public void OnSelectedNavigationButtonChange()
        {
            if (callBack != null)
            {
                callBack(); 
            }
            
        }

        public void OnSelectedNavigationButtonChange(Delegate method, params object[] args)
        {
            method.DynamicInvoke(args);
          
        }

        public void NavigationInputUpdate()
        {
            if (navigationDelayTimer <= 0)
            {
                if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical Axis") < 0)
                {
                    navigationDelayTimer = navigationDelay;
                    if (navigationButtonsIndex + 1 > pauseMenuNavigationButtons.Count - 1)
                    {
                        navigationButtonsIndex = 0;
                    }
                    else
                    {
                        navigationButtonsIndex++;
                    }

                    SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
                }

                if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical Axis") > 0)
                {
                    navigationDelayTimer = navigationDelay;
                    if (navigationButtonsIndex == 0)
                    {
                        navigationButtonsIndex = pauseMenuNavigationButtons.Count - 1;
                    }
                    else
                    {
                        navigationButtonsIndex--;
                    }

                    SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
                }
            }
            else
            {
                navigationDelayTimer -= Time.unscaledDeltaTime;
            }

            if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Menu Vertical Button"))
            {
                if (navigationButtonsIndex + 1 > pauseMenuNavigationButtons.Count - 1)
                {
                    navigationButtonsIndex = 0;
                }
                else
                {
                    navigationButtonsIndex++;
                }

                SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
            }
            else if (GameManager.Instance.playerInput.GetButtonDown("Navigate Menu Vertical Button"))
            {
                navigationDelayTimer = navigationDelay;
                if (navigationButtonsIndex == 0)
                {
                    navigationButtonsIndex = pauseMenuNavigationButtons.Count - 1;
                }
                else
                {
                    navigationButtonsIndex--;
                }

                SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
            }
        }

        public void SetFirstItem()
        {

            if (pauseMenuNavigationButtons.Count < 1)
            {
                return;
            }
            
            navigationButtonsIndex = 0;
            
            SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
        }

        public void ResetNavigationList()
        {
            pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
        }

        public void Add(PauseMenuNavigationButton pauseMenuNavigationButton)
        {
            pauseMenuNavigationButtons.Add(pauseMenuNavigationButton);
        }
        
    }
}