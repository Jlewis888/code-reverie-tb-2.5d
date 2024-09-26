using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    [RequireComponent(typeof(RadialMenuManager))]
    public class CommandRadialMenuHelper : SerializedMonoBehaviour
    {
        public RadialMenuManager radialMenuManager;
        public RadialMenuOption attackRadialMenuOption;
        public RadialMenuOption breakRadialMenuOption;
        public RadialMenuOption defendRadialMenuOption;
        public RadialMenuOption skillsSelectRadialMenuOption;
        public RadialMenuOption itemSelectRadialMenuOption;
        public RadialMenuOption moveRadialMenuOption;
        
        private void Awake()
        {
            radialMenuManager = GetComponent<RadialMenuManager>();
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
            if (radialMenuManager.selectedRadialMenuOption == attackRadialMenuOption)
            {
                Debug.Log("Attack");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Attack;
                CombatManager.Instance.SetSelectableTargets();
                //CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu(this);
            } 
            else if (radialMenuManager.selectedRadialMenuOption == breakRadialMenuOption)
            {
                Debug.Log("Break");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Break;
                CombatManager.Instance.SetSelectableTargets();
                //CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu(this);
            }
            else if (radialMenuManager.selectedRadialMenuOption == defendRadialMenuOption)
            {
                Debug.Log("Defend");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Defend;
                CombatManager.Instance.ConfirmAction();
            }
            else if (radialMenuManager.selectedRadialMenuOption == skillsSelectRadialMenuOption)
            {
                Debug.Log("Skills");
                CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState =
                    CharacterBattleActionState.Skill;
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleSkillMenu();
            }
            else if (radialMenuManager.selectedRadialMenuOption == itemSelectRadialMenuOption)
            {
                Debug.Log("Items");
                if (PlayerManager.Instance.inventory.HasCombatItem())
                {
                    CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Item;
                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleItemMenu();
                }
            }
            else if (radialMenuManager.selectedRadialMenuOption == moveRadialMenuOption)
            {
                Debug.Log("Move");
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleMoveActionMenu();
                Debug.Log("Move Action. Need to allow player to move character to a location");
            }
        }
        
    }
}