using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ActionCommandSelectMenuManager : CommandMenuManager
    {
        
        public CommandMenuNavigationButton commandAttackSelect;
        public CommandMenuNavigationButton commandBreakSelect;


        private void Awake()
        {
            commandMenuNavigation = new CommandMenuNavigation();
            commandMenuNavigation.Add(commandAttackSelect);
            commandMenuNavigation.Add(commandBreakSelect);
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
            
            // if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            // {
            //     CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleCommandAction();
            // }
            
            commandMenuNavigation.NavigationInputUpdate();

            
        }


        public void ConfirmAction()
        {
            if (commandMenuNavigation.SelectedNavigationButton == commandAttackSelect)
            {
               // Debug.Log("Attack Action. Need to Set Target enemy window now");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Attack;
                CombatManager.Instance.SetSelectableTargets();
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu(this);
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandBreakSelect)
            {
                // Debug.Log("Attack Action. Need to Set Target enemy window now");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Break;
                CombatManager.Instance.SetSelectableTargets();
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu(this);
                
            }
        }
    }
}