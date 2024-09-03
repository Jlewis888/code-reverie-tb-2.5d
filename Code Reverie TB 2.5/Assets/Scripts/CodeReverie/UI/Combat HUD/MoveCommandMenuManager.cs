using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class MoveCommandMenuManager : CommandMenuManager
    {
        //public CommandMenuNavigation commandMenuNavigation;
        public CommandMenuNavigationButton commandActionSelect;
        public CommandMenuNavigationButton commandDefendSelect;



        private void Awake()
        {
            commandMenuNavigation = new CommandMenuNavigation();
            commandMenuNavigation.Add(commandActionSelect);
            commandMenuNavigation.Add(commandDefendSelect);

        }

        private void OnEnable()
        {
            CombatManager.Instance.movePlayerObject.characterBattleManager = CombatManager.Instance.selectedPlayerCharacter;
            CombatManager.Instance.movePlayerObject.Init();
            CombatManager.Instance.movePlayerObject.gameObject.SetActive(true);
            CombatManager.Instance.movePlayerObject.GetComponent<AnimationManager>().ChangeAnimationState("run");
            CameraManager.Instance.SetSelectedPlayerWeight(CombatManager.Instance.movePlayerObject.characterBattleManager, 10f, 3f);
          
        }

        private void OnDisable()
        {
            CombatManager.Instance.movePlayerObject.gameObject.SetActive(false);
        }

        private void Update()
        {
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {
                CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.ToggleCommandAction();
            }

            //commandMenuNavigation.NavigationInputUpdate();
        }


        public void ConfirmAction()
        {
            CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                CharacterBattleActionState.Move;
            
            CombatManager.Instance.ConfirmAction();
        }
    }
}