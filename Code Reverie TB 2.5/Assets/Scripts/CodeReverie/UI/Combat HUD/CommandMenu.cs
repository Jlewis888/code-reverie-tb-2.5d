using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CommandMenu : SerializedMonoBehaviour
    {
        
        public GameObject commandMenuPrompt;
        public CombatCommandMenu combatCommandMenu;
        public InteractiveCommandMenu interactiveCommandMenuHolder;
        
        private void OnEnable()
        {
            ToggleCommandMenuHolderOff();
            EventManager.Instance.combatEvents.onPlayerTurn += ToggleCommandMenuHolderOn;
        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onPlayerTurn -= ToggleCommandMenuHolderOn;
        }


        public void ToggleCombatCommandMenuHolderOn()
        {
            commandMenuPrompt.SetActive(false);
            combatCommandMenu.gameObject.SetActive(true);
            interactiveCommandMenuHolder.gameObject.SetActive(false);
        }
        
        public void ToggleInteractiveCommandMenuHolderOn()
        {
            commandMenuPrompt.SetActive(false);
            combatCommandMenu.gameObject.SetActive(false);
            interactiveCommandMenuHolder.gameObject.SetActive(true);
        }
        
        public void ToggleCommandMenuPromptOn()
        {
            commandMenuPrompt.SetActive(true);
            combatCommandMenu.gameObject.SetActive(false);
            interactiveCommandMenuHolder.gameObject.SetActive(false);
        }
        
        
        public void ToggleCommandMenuHolderOn(CharacterBattleManager characterBattleManager = null)
        {
            //combatCommandMenu.characterBattleManager = characterBattleManager;
            ToggleCombatCommandMenuHolderOn();
        }
        
        public void ToggleCommandMenuHolderOff(CharacterBattleManager characterBattleManager = null)
        {
            //combatCommandMenu.characterBattleManager = characterBattleManager;
            ToggleCommandMenuPromptOn();

        }

        public void SetCharacterSkillDetails()
        {
            
        }
    }
}