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
            if (commandMenuNavigation.SelectedNavigationButton == commandAttackSelect)
            {
               // Debug.Log("Attack Action. Need to Set Target enemy window now");
                BattleManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Attack;
                BattleManager.Instance.SetSelectableTargets();
                CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu();
            }
            else if (commandMenuNavigation.SelectedNavigationButton == commandBreakSelect)
            {
                // Debug.Log("Attack Action. Need to Set Target enemy window now");
                BattleManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Break;
                BattleManager.Instance.SetSelectableTargets();
                CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu();
                
            }
        }
    }
}