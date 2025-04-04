﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CombatCommandMenu : SerializedMonoBehaviour
    {
        public CharacterBattleManager characterBattleManager;
        public Image characterPortrait;
        public TMP_Text characterNames;
        
        public List<GameObject> commandMenus;
        public CommandSelectMenuManager commandSelectMenuManager;
        public ActionCommandSelectMenuManager actionCommandSelectMenu;
        public SkillCommandMenuManager skillCommandMenuManager;
        public ItemCommandMenuManager itemCommandMenuManager;
        public TargetCommandMenuManager targetCommandMenuManager;
        public MoveCommandMenuManager moveCommandMenuManager;

        public CommandMenuManager prevCommandMenuManager;

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
            ToggleCommandAction();
        }

        private void OnDisable()
        {
            ToggleActionMenu();
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
        
        public void ToggleTargetMenu(CommandMenuManager commandMenuManager)
        {
            prevCommandMenuManager = commandMenuManager;
            ToggleCommandMenus(targetCommandMenuManager.gameObject);
        }
        
        public void ToggleMoveActionMenu()
        {
            ToggleCommandMenus(moveCommandMenuManager.gameObject);
        }

        public void TogglePrevMenu()
        {
            ToggleCommandMenus(prevCommandMenuManager.gameObject);
        }
        
    }
}