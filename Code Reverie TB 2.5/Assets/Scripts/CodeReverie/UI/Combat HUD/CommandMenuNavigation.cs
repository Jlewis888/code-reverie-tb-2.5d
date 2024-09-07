using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CommandMenuNavigation
    {
        public List<CommandMenuNavigationButton> commandMenuNavigationButtons = new List<CommandMenuNavigationButton>();
        public CommandMenuNavigationButton selectedNavigationButton;
        public int navigationButtonsIndex;
        public float navigationDelay = 0.35f;
        public float navigationDelayTimer;
        public ScrollRect scrollRect;
        public Action callBack;


        public CommandMenuNavigation()
        {
            commandMenuNavigationButtons = new List<CommandMenuNavigationButton>();
        }
        
        
        public CommandMenuNavigationButton SelectedNavigationButton
        {
            get => selectedNavigationButton;

            set
            {
                if (selectedNavigationButton == value)
                {
                    if (scrollRect != null)
                    {
                        if (selectedNavigationButton != null)
                        {
                            scrollRect.GetComponent<ScrollRectExtension>().SnapTo(SelectedNavigationButton.GetComponent<RectTransform>());
                        }
                        
                        
                    }
                    return;
                }

                selectedNavigationButton = value;
                
                CommandMenuNavigationUpdate();
                
                
                selectedNavigationButton.selector.SetActive(true);

                if (scrollRect != null)
                {
                    if (selectedNavigationButton != null)
                    {
                        scrollRect.GetComponent<ScrollRectExtension>().SnapTo(SelectedNavigationButton.GetComponent<RectTransform>());
                    }

                }
                
                OnSelectedNavigationButtonChange();

            }
        }
        
        
        public void CommandMenuNavigationUpdate()
        {
            foreach (CommandMenuNavigationButton commandMenuNavigationButton in commandMenuNavigationButtons)
            {
           
                if (SelectedNavigationButton != commandMenuNavigationButton)
                {
                    commandMenuNavigationButton.selector.SetActive(false);
                }
                else
                {
                    commandMenuNavigationButton.selector.SetActive(true);
                }
            }

            if (selectedNavigationButton != null)
            {
                selectedNavigationButton.selector.SetActive(true); 
            }
            
        }

        public void NavigationInputUpdate()
        {
            if (navigationDelayTimer <= 0)
            {
                if (GameManager.Instance.playerInput.GetAxis("Navigate Combat Vertical Axis") < 0)
                {
                    navigationDelayTimer = navigationDelay;
                    if (navigationButtonsIndex + 1 > commandMenuNavigationButtons.Count - 1)
                    {
                        navigationButtonsIndex = 0;
                    }
                    else
                    {
                        navigationButtonsIndex++;
                    }
                    
                    SelectedNavigationButton = commandMenuNavigationButtons[navigationButtonsIndex];
                }
                else if (GameManager.Instance.playerInput.GetAxis("Navigate Combat Vertical Axis") > 0)
                {
                    navigationDelayTimer = navigationDelay;
                    if (navigationButtonsIndex == 0)
                    {
                        navigationButtonsIndex = commandMenuNavigationButtons.Count - 1;
                    }
                    else
                    {
                        navigationButtonsIndex--;
                    }
                    
                    SelectedNavigationButton = commandMenuNavigationButtons[navigationButtonsIndex];
                }
            }
            else {
                navigationDelayTimer -= Time.unscaledDeltaTime;
            }
            
            if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Combat Vertical"))
            {
               
                //navigationDelayTimer = navigationDelay;
                if (navigationButtonsIndex + 1 > commandMenuNavigationButtons.Count - 1)
                {
                    navigationButtonsIndex = 0;
                }
                else
                {
                    navigationButtonsIndex++;
                }
                    
                SelectedNavigationButton = commandMenuNavigationButtons[navigationButtonsIndex];
            }
            else if (GameManager.Instance.playerInput.GetButtonDown("Navigate Combat Vertical"))
            {
                
                
                //navigationDelayTimer = navigationDelay;
                if (navigationButtonsIndex == 0)
                {
                    navigationButtonsIndex = commandMenuNavigationButtons.Count - 1;
                }
                else
                {
                    navigationButtonsIndex--;
                }
                    
                SelectedNavigationButton = commandMenuNavigationButtons[navigationButtonsIndex];
            }
        }

        public void NavigationInputUpdateButtonDown()
        {
            if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Combat Vertical"))
            {
               
                //navigationDelayTimer = navigationDelay;
                if (navigationButtonsIndex + 1 > commandMenuNavigationButtons.Count - 1)
                {
                    navigationButtonsIndex = 0;
                }
                else
                {
                    navigationButtonsIndex++;
                }
                    
                SelectedNavigationButton = commandMenuNavigationButtons[navigationButtonsIndex];
            }
            else if (GameManager.Instance.playerInput.GetButtonDown("Navigate Combat Vertical"))
            {
                
                
                //navigationDelayTimer = navigationDelay;
                if (navigationButtonsIndex == 0)
                {
                    navigationButtonsIndex = commandMenuNavigationButtons.Count - 1;
                }
                else
                {
                    navigationButtonsIndex--;
                }
                    
                SelectedNavigationButton = commandMenuNavigationButtons[navigationButtonsIndex];
            }
        }
        

        public void SetFirstItem()
        {
            navigationButtonsIndex = 0;

            if (commandMenuNavigationButtons.Count <= 0)
            {
                return;
            }
            
            SelectedNavigationButton = commandMenuNavigationButtons[navigationButtonsIndex];
        }

        public void Add(CommandMenuNavigationButton commandMenuNavigationButton)
        {
            commandMenuNavigationButtons.Add(commandMenuNavigationButton);
        }
        
        public void Remove(CommandMenuNavigationButton commandMenuNavigationButton)
        {
            commandMenuNavigationButtons.Remove(commandMenuNavigationButton);
        }

        public void ClearNavigationList()
        {
            commandMenuNavigationButtons = new List<CommandMenuNavigationButton>();
        }
        
        
        public void OnSelectedNavigationButtonChange()
        {
            if (callBack != null)
            {
                callBack(); 
            }
            
        }

        public void Reset()
        {
            ClearNavigationList();
            navigationButtonsIndex = 0;
            selectedNavigationButton = null;
            callBack = null;
        }
        
    }
}