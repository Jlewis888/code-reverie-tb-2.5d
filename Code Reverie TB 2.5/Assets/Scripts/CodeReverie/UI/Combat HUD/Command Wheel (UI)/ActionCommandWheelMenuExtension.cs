using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeReverie
{
    
    public class ActionCommandWheelMenuExtension : CommandWheelExtension
    { 
        
        public CommandWheelOption attackCommandWheelOption; 
        public CommandWheelOption breakCommandWheelOption; 
        public CommandWheelOption defendCommandWheelOption;
        public CommandWheelOption skillsSelectCommandWheelOption;
        public CommandWheelOption itemSelectCommandWheelOption;
        public CommandWheelOption moveCommandWheelOption;
        

        public override void InitExtension()
        {
            
        }


        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
        }

        public void ConfirmAction()
        {
            if (commandWheel.selectedCommandWheelOption == attackCommandWheelOption)
            {
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Attack;
                CombatManager.Instance.SetSelectableTargets();
                
            } 
            else if (commandWheel.selectedCommandWheelOption == breakCommandWheelOption)
            {
             
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Break;
                CombatManager.Instance.SetSelectableTargets();
               
            }
            else if (commandWheel.selectedCommandWheelOption == defendCommandWheelOption)
            {
               
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Defend;
                EventManager.Instance.combatEvents.OnPlayerTurnEnd();
                CombatManager.Instance.ConfirmAction();
               
            }
            else if (commandWheel.selectedCommandWheelOption == skillsSelectCommandWheelOption)
            {
               //todo check if character has skills
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Skill;
                EventManager.Instance.combatEvents.OnSkillCommandWheelSelect();
            }
            else if (commandWheel.selectedCommandWheelOption == itemSelectCommandWheelOption)
            {
                
                if (PlayerManager.Instance.inventory.HasCombatItem())
                {
                    CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Item;
                    
                    EventManager.Instance.combatEvents.OnItemCommandWheelSelect();
                    
                }
            }
            else if (commandWheel.selectedCommandWheelOption == moveCommandWheelOption)
            {
                // CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleMoveActionMenu();
                // CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandWheelPanelManager
                //     .previousCommandWheelPanel = GetComponentInParent<CommandWheelPanel>();
                
                EventManager.Instance.combatEvents.OnPlayerTurnEnd();
                CombatManager.Instance.movePlayerObject.characterBattleManager = CombatManager.Instance.selectedPlayerCharacter;
                CombatManager.Instance.movePlayerObject.Init();
                CombatManager.Instance.movePlayerObject.gameObject.SetActive(true);
                //CombatManager.Instance.movePlayerObject.GetComponent<AnimationManager>().ChangeAnimationState("run");
                CameraManager.Instance.SetSelectedPlayerWeight(CombatManager.Instance.movePlayerObject.characterBattleManager, 10f, 3f);
                CombatManager.Instance.combatManagerState = CombatManagerState.PlayerMove;

            }
        }
        
    }
}