using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class InteractiveCommandMenu : CommandMenuManager
    {
        public InteractableMenuNavigationButton interactableMenuNavigationButtonPF;
        public GameObject interactableMenuNavigationButtonHolder;
        public List<Interactable> interactables = new List<Interactable>();
        public List<IInteractable> iInteractables = new List<IInteractable>();
        public CommandMenuUIScrollToSelection commandMenuUIScrollToSelection;
        public ScrollRect scroll;

        private void Awake()
        {
            commandMenuNavigation = new CommandMenuNavigation();
            commandMenuNavigation.scrollRect = scroll;
            //commandMenuNavigation.callBack = scroll.GetComponent<ScrollRectExtension>().SnapTo(commandMenuNavigation.SelectedNavigationButton.GetComponent<RectTransform>());
            // commandMenus = new List<GameObject>();
            // commandMenus.Add(commandSelectMenuManager.gameObject);
        }

        private void OnEnable()
        {
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 2);
            SetInteractableButtons();
            commandMenuNavigation.SetFirstItem();
            //commandMenuNavigation = new CommandMenuNavigation();
            //ToggleCommandAction();
        }

        private void OnDisable()
        {
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(false, 2);
            foreach (Transform child in interactableMenuNavigationButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            commandMenuNavigation.Reset();
            interactables = new List<Interactable>();
            iInteractables = new List<IInteractable>();
        }

        private void Update()
        {
            if (interactables.Count <= 0)
            {
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCommandMenuPromptOn();
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
            
            
            // if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Combat Vertical"))
            // {
            //     commandMenuUIScrollToSelection.Move(MoveDirection.Down);
            //     commandMenuUIScrollToSelection.UpdateVerticalScrollPosition(MoveDirection.Down);
            // }
            // else if (GameManager.Instance.playerInput.GetButtonDown("Navigate Combat Vertical"))
            // {
            //     commandMenuUIScrollToSelection.Move(MoveDirection.Up);
            //     commandMenuUIScrollToSelection.UpdateVerticalScrollPosition(MoveDirection.Up);
            // }
            
            
            commandMenuNavigation.NavigationInputUpdateButtonDown();
        }

        public void SetInteractableButtons()
        {
            foreach (Interactable interactable in interactables)
            {
                // InteractableMenuNavigationButton interactableMenuNavigationButton =
                //     Instantiate(interactableMenuNavigationButtonPF, interactableMenuNavigationButtonHolder.transform);
                //
                // interactableMenuNavigationButton.interactable = interactable;
                // interactableMenuNavigationButton.nameText.text = interactable.interactables[0].interactableMessage;
                // commandMenuNavigation.Add(interactableMenuNavigationButton);
                
                foreach (IInteractable iInteractable in interactable.interactables)
                {
                    if (!iInteractable.CanInteract)
                    {
                        continue;
                    }
                    
                    iInteractables.Add(iInteractable);
                    InteractableMenuNavigationButton interactableMenuNavigationButton =
                        Instantiate(interactableMenuNavigationButtonPF, interactableMenuNavigationButtonHolder.transform);

                    interactableMenuNavigationButton.interactable = interactable;
                    interactableMenuNavigationButton.iInteractable = iInteractable;
                    interactableMenuNavigationButton.nameText.text = iInteractable.interactableMessage;
                    commandMenuNavigation.Add(interactableMenuNavigationButton);
                    commandMenuNavigation.CommandMenuNavigationUpdate();
                }
            }
        }

        public void AddInteractableButton(Interactable interactable)
        {
            interactables.Add(interactable);

            foreach (IInteractable iInteractable in interactable.interactables)
            {
                
                if (!iInteractable.CanInteract)
                {
                    continue;
                }
                
                iInteractables.Add(iInteractable);
                InteractableMenuNavigationButton interactableMenuNavigationButton =
                    Instantiate(interactableMenuNavigationButtonPF, interactableMenuNavigationButtonHolder.transform);

                interactableMenuNavigationButton.interactable = interactable;
                interactableMenuNavigationButton.iInteractable = iInteractable;
                interactableMenuNavigationButton.nameText.text = iInteractable.interactableMessage;
                commandMenuNavigation.Add(interactableMenuNavigationButton);
                commandMenuNavigation.CommandMenuNavigationUpdate();
            }
        }

        public void RemoveInteractableButton(Interactable interactable)
        {
            interactables.Remove(interactable);

            CommandMenuNavigationButton commandMenuNavigationButtonToRemove = null;

            foreach (CommandMenuNavigationButton commandMenuNavigationButton in commandMenuNavigation
                         .commandMenuNavigationButtons)
            {
                if (commandMenuNavigationButton.GetComponent<InteractableMenuNavigationButton>().interactable ==
                    interactable)
                {
                    foreach (IInteractable iInteractable in interactable.interactables)
                    {
                        if (iInteractables.Contains(iInteractable))
                        {
                            iInteractables.Remove(iInteractable);
                        }
                        
                    }
                    
                    commandMenuNavigationButtonToRemove = commandMenuNavigationButton;
                    break;
                }
            }

            if (commandMenuNavigationButtonToRemove != null)
            {
                
                commandMenuNavigation.Remove(commandMenuNavigationButtonToRemove);
                Destroy(commandMenuNavigationButtonToRemove.gameObject);
            }
            
            commandMenuNavigation.CommandMenuNavigationUpdate();
            
        }
        
        public void RemoveInteractableButton(IInteractable interactable)
        {
            iInteractables.Remove(interactable);
            CommandMenuNavigationButton commandMenuNavigationButtonToRemove = null;

            foreach (CommandMenuNavigationButton commandMenuNavigationButton in commandMenuNavigation
                         .commandMenuNavigationButtons)
            {
                if (commandMenuNavigationButton.GetComponent<InteractableMenuNavigationButton>().iInteractable == interactable)
                {
                    commandMenuNavigationButtonToRemove = commandMenuNavigationButton;
                    break;
                }
            }

            if (commandMenuNavigationButtonToRemove != null)
            {
                
                commandMenuNavigation.Remove(commandMenuNavigationButtonToRemove);
                Destroy(commandMenuNavigationButtonToRemove.gameObject);
            }
            
            commandMenuNavigation.CommandMenuNavigationUpdate();
            
        }
        
        
        public void ConfirmAction()
        {

            IInteractable interactable = commandMenuNavigation.SelectedNavigationButton
                .GetComponent<InteractableMenuNavigationButton>().iInteractable;
            
            interactable.Interact(
               () =>
               {
                   if (!interactable.CanInteract)
                   {
                       Debug.Log("SHould remove button now");
                       RemoveInteractableButton(interactable);
                       
                       if (iInteractables.Count <= 0)
                       {
                           CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCommandMenuPromptOn();
                       }
                   }
               });
        }
    }
}