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
        public CharacterBattleManager characterBattleManager;
        public Image characterPortrait;
        public TMP_Text characterName;
        
        public GameObject commandMenuPrompt;
        public GameObject commandMenuHolder;
        
        public List<GameObject> commandMenus;
        public CommandSelectMenuManager commandSelectMenuManager;
        public ActionCommandSelectMenuManager actionCommandSelectMenu;
        public SkillCommandMenuManager skillCommandMenuManager;
        public ItemCommandMenuManager itemCommandMenuManager;
        public TargetCommandMenuManager targetCommandMenuManager;
        public MoveCommandMenuManager moveCommandMenuManager;


        private void Awake()
        {
            commandMenus = new List<GameObject>();
            commandMenus.Add(commandSelectMenuManager.gameObject);
            commandMenus.Add(skillCommandMenuManager.gameObject);
            commandMenus.Add(targetCommandMenuManager.gameObject);
            commandMenus.Add(itemCommandMenuManager.gameObject);
            commandMenus.Add(actionCommandSelectMenu.gameObject);
            commandMenus.Add(moveCommandMenuManager.gameObject);
        }

        private void OnEnable()
        {
            ToggleCommandMenuHolderOff();
            ToggleCommandAction();
            EventManager.Instance.combatEvents.onPlayerTurn += ToggleCommandMenuHolderOn;
        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onPlayerTurn -= ToggleCommandMenuHolderOn;
        }


        public void ToggleCommandMenuHolderOn()
        {
            commandMenuPrompt.SetActive(false);
            commandMenuHolder.SetActive(true);
            ToggleCommandAction();
        }
        
        public void ToggleCommandMenuHolderOff()
        {
            commandMenuPrompt.SetActive(true);
            commandMenuHolder.SetActive(false);
            ToggleCommandAction();
        }
        
        public void ToggleCommandMenuHolderOn(CharacterBattleManager characterBattleManager = null)
        {
           ToggleCommandMenuHolderOn();
        }
        
        public void ToggleCommandMenuHolderOff(CharacterBattleManager characterBattleManager = null)
        {
            ToggleCommandMenuHolderOff();
          
        }

        public void SetCharacterSkillDetails()
        {
            
        }

        public void ToggleCommandMenus(GameObject menu)
        {
            foreach (GameObject commandMenu in commandMenus)
            {
                if (commandMenu == menu)
                {
                    commandMenu.SetActive(true);
                }
                else
                {
                    commandMenu.SetActive(false);
                }
            }
        }

        public void ToggleCommandAction()
        {
            ToggleCommandMenus(commandSelectMenuManager.gameObject);
        }
        
        public void ToggleActionMenu()
        {
            ToggleCommandMenus(actionCommandSelectMenu.gameObject);
        }
        
        public void ToggleSkillMenu()
        {
            ToggleCommandMenus(skillCommandMenuManager.gameObject);
        }
        
        public void ToggleItemMenu()
        {
            ToggleCommandMenus(itemCommandMenuManager.gameObject);
        }
        
        public void ToggleTargetMenu()
        {
            ToggleCommandMenus(targetCommandMenuManager.gameObject);
        }
        
        public void ToggleMoveActionMenu()
        {
            ToggleCommandMenus(moveCommandMenuManager.gameObject);
        }
        
    }
}