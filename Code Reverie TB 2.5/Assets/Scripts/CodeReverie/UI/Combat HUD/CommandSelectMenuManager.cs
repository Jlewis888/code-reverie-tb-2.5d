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
        public CommandMenuNavigationButton commandAttackSelect;
        public CommandMenuNavigationButton commandBreakSelect;
        public CommandMenuNavigationButton commandDefendSelect;
        public CommandMenuNavigationButton commandSkillsSelect;
        public CommandMenuNavigationButton commandItemsSelect;
        public CommandMenuNavigationButton commandMoveSelect;
        
        private void Awake()
        {
            commandMenuNavigation = new CommandMenuNavigation();
            //commandMenuNavigation.Add(commandActionSelect);
            commandMenuNavigation.Add(commandAttackSelect);
            commandMenuNavigation.Add(commandBreakSelect);
            commandMenuNavigation.Add(commandDefendSelect);
            commandMenuNavigation.Add(commandSkillsSelect);
            commandMenuNavigation.Add(commandItemsSelect);
            commandMenuNavigation.Add(commandMoveSelect);
            commandMenuNavigation.scrollRect = scrollRect;
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
            // if (commandMenuNavigation.SelectedNavigationButton == commandActionSelect)
            // {
            //    // Debug.Log("Attack Action. Need to Set Target enemy window now");
            //     // BattleManager.Instance.selectedPlayerCharacter.characterBattleActionState =
            //     //     CharacterBattleActionState.Attack;
            //     // BattleManager.Instance.SetSelectableTargets();
            //    
            //     CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandMenu.combatCommandMenu.ToggleActionMenu();
            // }
            
            if (commandMenuNavigation.SelectedNavigationButton == commandAttackSelect)
            {
                // Debug.Log("Attack Action. Need to Set Target enemy window now");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Attack;
                CombatManager.Instance.SetSelectableTargets();
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandMenu.ToggleTargetMenu();
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandBreakSelect)
            {
                // Debug.Log("Attack Action. Need to Set Target enemy window now");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Break;
                CombatManager.Instance.SetSelectableTargets();
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandMenu.ToggleTargetMenu();
                
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
                
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandMenu.ToggleSkillMenu();
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandItemsSelect)
            {

                if (PlayerManager.Instance.inventory.HasCombatItem())
                {
                    CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Item;
                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandMenu.ToggleItemMenu();
                }
                
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandMoveSelect)
            {
                
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandMenu.ToggleMoveActionMenu();
                Debug.Log("Move Action. Need to allow player to move character to a location");
            }
        }
    }
}