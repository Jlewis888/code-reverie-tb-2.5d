using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class TargetCommandMenuManager : CommandMenuManager
    {
        public GameObject targetCommandMenuNavigationButtonHolder;
        public TargetCommandMenuNavigationButton targetCommandMenuNavigationButtonPF;
        
        
        private void Awake()
        {

            if (commandMenuNavigation == null)
            {
                SetCommandNavigation();
            }
        }
        
        
        private void OnEnable()
        {
            SetTargetableEnemyNavigationButtons();
            commandMenuNavigation.SetFirstItem();
            EventManager.Instance.combatEvents.OnPlayerSelectTarget(commandMenuNavigation.SelectedNavigationButton.GetComponent<TargetCommandMenuNavigationButton>().characterBattleManager);
        }

        private void Update()
        {
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {
                Debug.Log("Go to previous menu");
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.TogglePrevMenu();
            }
            
            commandMenuNavigation.NavigationInputUpdate();
            
        }

        public void SetCommandNavigation()
        {
            commandMenuNavigation = new CommandMenuNavigation();
            commandMenuNavigation.callBack = () =>
            {
                //CameraManager.Instance.UpdateCamera(commandMenuNavigation.SelectedNavigationButton.GetComponent<TargetCommandMenuNavigationButton>().characterBattleManager.transform);
                CameraManager.Instance.SetSelectedPlayerWeight(commandMenuNavigation.SelectedNavigationButton.GetComponent<TargetCommandMenuNavigationButton>().characterBattleManager, 1000f, 2f);
                EventManager.Instance.combatEvents.OnPlayerSelectTarget(commandMenuNavigation.SelectedNavigationButton.GetComponent<TargetCommandMenuNavigationButton>().characterBattleManager);
            };
        }

        public void SetInitialEnemyNavigationButtons()
        {
            
            if (commandMenuNavigation == null)
            {
                SetCommandNavigation();
            }
            
            commandMenuNavigation.ClearNavigationList();
            Clear();
            foreach (CharacterBattleManager characterBattleManager in CombatManager.Instance.enemyUnits)
            {
                TargetCommandMenuNavigationButton targetCommandMenuNavigationButton =
                    Instantiate(targetCommandMenuNavigationButtonPF, targetCommandMenuNavigationButtonHolder.transform);

                targetCommandMenuNavigationButton.characterBattleManager = characterBattleManager;
                targetCommandMenuNavigationButton.nameText.text = characterBattleManager.GetComponent<CharacterUnitController>()
                    .character.info.characterName;
                commandMenuNavigation.Add(targetCommandMenuNavigationButton);
            }
            
            //SelectedNavigationButton = commandMenuNavigationButtons[navigationButtonsIndex];
        }

        public void SetTargetableEnemyNavigationButtons()
        {
            if (commandMenuNavigation == null)
            {
                SetCommandNavigation();
            }
            
            commandMenuNavigation.ClearNavigationList();
            Clear();
            foreach (CharacterBattleManager characterBattleManager in CombatManager.Instance.selectableTargets)
            {
                TargetCommandMenuNavigationButton targetCommandMenuNavigationButton =
                    Instantiate(targetCommandMenuNavigationButtonPF, targetCommandMenuNavigationButtonHolder.transform);

                targetCommandMenuNavigationButton.characterBattleManager = characterBattleManager;
                targetCommandMenuNavigationButton.nameText.text = characterBattleManager.GetComponent<CharacterUnitController>()
                    .character.info.characterName;
                commandMenuNavigation.Add(targetCommandMenuNavigationButton);
            }
        }

        public void Clear()
        {
            foreach (Transform child in targetCommandMenuNavigationButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void ConfirmAction()
        {
            //EventManager.Instance.combatEvents.OnPlayerSelectTarget(SelectedNavigationButton.GetComponent<TargetCommandMenuNavigationButton>().characterBattleManager);
            CombatManager.Instance.ConfirmAction();
        }
        
    }
}