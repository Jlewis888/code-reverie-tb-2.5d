using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class InteractiveCommandMenu : CommandMenuManager
    {
        public InteractableMenuNavigationButton interactableMenuNavigationButtonPF;
        public GameObject interactableMenuNavigationButtonHolder;
        public List<Interactable> interactables = new List<Interactable>();

        private void Awake()
        {
            commandMenuNavigation = new CommandMenuNavigation();
            // commandMenus = new List<GameObject>();
            // commandMenus.Add(commandSelectMenuManager.gameObject);
        }

        private void OnEnable()
        {
            SetInteractableButtons();
            commandMenuNavigation.SetFirstItem();
            //commandMenuNavigation = new CommandMenuNavigation();
            //ToggleCommandAction();
        }

        private void OnDisable()
        {
            commandMenuNavigation.Reset();
        }

        private void Update()
        {
            if (interactables.Count <= 0)
            {
                CanvasManager.Instance.hudManager.commandMenu.ToggleCommandMenuPromptOn();
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
            
            commandMenuNavigation.NavigationInputUpdate();
        }

        public void SetInteractableButtons()
        {
            foreach (Interactable interactable in interactables)
            {
                InteractableMenuNavigationButton interactableMenuNavigationButton =
                    Instantiate(interactableMenuNavigationButtonPF, interactableMenuNavigationButtonHolder.transform);

                interactableMenuNavigationButton.interactable = interactable;
                interactableMenuNavigationButton.nameText.text = interactable.interactables[0].interactableMessage;
                commandMenuNavigation.Add(interactableMenuNavigationButton);
            }
        }

        public void AddInteractableButton(Interactable interactable)
        {
            interactables.Add(interactable);
            
            InteractableMenuNavigationButton interactableMenuNavigationButton =
                Instantiate(interactableMenuNavigationButtonPF, interactableMenuNavigationButtonHolder.transform);

            interactableMenuNavigationButton.interactable = interactable;
            interactableMenuNavigationButton.nameText.text = interactable.interactables[0].interactableMessage;
            commandMenuNavigation.Add(interactableMenuNavigationButton);
            commandMenuNavigation.CommandMenuNavigationUpdate();
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
            
        }
    }
}