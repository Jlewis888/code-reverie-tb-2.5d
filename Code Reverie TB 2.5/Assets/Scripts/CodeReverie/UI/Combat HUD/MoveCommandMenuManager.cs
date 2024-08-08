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
            BattleManager.Instance.movePlayerObject.characterBattleManager = BattleManager.Instance.selectedPlayerCharacter;
            BattleManager.Instance.movePlayerObject.Init();
            BattleManager.Instance.movePlayerObject.gameObject.SetActive(true);
            BattleManager.Instance.movePlayerObject.GetComponent<AnimationManager>().ChangeAnimationState("run");
            CameraManager.Instance.SetSelectedPlayerWeight(BattleManager.Instance.movePlayerObject.characterBattleManager, 10f, 3f);
          
        }

        private void OnDisable()
        {
            BattleManager.Instance.movePlayerObject.gameObject.SetActive(false);
        }

        private void Update()
        {
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }

            //commandMenuNavigation.NavigationInputUpdate();
        }


        public void ConfirmAction()
        {
            BattleManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                CharacterBattleActionState.Move;
            
            BattleManager.Instance.ConfirmAction();
        }
    }
}