using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CommandSelectMenuManager : CommandMenuManager
    {
        //public CommandMenuNavigation commandMenuNavigation;
        public CommandMenuNavigationButton commandActionSelect;
        public CommandMenuNavigationButton commandDefendSelect;
        public CommandMenuNavigationButton commandSkillsSelect;
        public CommandMenuNavigationButton commandItemsSelect;
        public CommandMenuNavigationButton commandMoveSelect;
        
        private void Awake()
        {
            commandMenuNavigation = new CommandMenuNavigation();
            commandMenuNavigation.Add(commandActionSelect);
            commandMenuNavigation.Add(commandDefendSelect);
            commandMenuNavigation.Add(commandSkillsSelect);
            commandMenuNavigation.Add(commandItemsSelect);
            commandMenuNavigation.Add(commandMoveSelect);

        }

        private void OnEnable()
        {
            commandMenuNavigation.SetFirstItem();
        }

        private void Update()
        {
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
            
           

            commandMenuNavigation.NavigationInputUpdate();
        }


        public void ConfirmAction()
        {
            if (commandMenuNavigation.SelectedNavigationButton == commandActionSelect)
            {
               // Debug.Log("Attack Action. Need to Set Target enemy window now");
                // BattleManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                //     CharacterBattleActionState.Attack;
                // BattleManager.Instance.SetSelectableTargets();
               
                CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleActionMenu();
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandDefendSelect)
            {
                //Debug.Log("Defend Action. Restart Character and up defense for turn or until hit");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Defend;
                CombatManager.Instance.ConfirmAction();
                
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandSkillsSelect)
            {
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Skill;
               // Debug.Log("Skill Action. Need to set Skill window now");
               //BattleManager.Instance.SetSelectableTargets();
                
                CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleSkillMenu();
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandItemsSelect)
            {
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Item;
                CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleItemMenu();
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandMoveSelect)
            {
                
                CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleMoveActionMenu();
                Debug.Log("Move Action. Need to allow player to move character to a location");
            }
        }
    }
}