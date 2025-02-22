using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CombatActionMenu : SerializedMonoBehaviour
    {
        public CharacterBattleManager characterBattleManager;
        //public CombatCommandMenu combatCommandMenu;
        public Image characterPortrait;
        public TMP_Text characterName;
        
        public List<CommandMenuManager> commandMenus;
        public GameObject actionMenusHolder;
        public CommandSelectMenuManager commandSelectMenuManager;
        public ActionCommandSelectMenuManager actionCommandSelectMenu;
        public SkillCommandMenuManager skillCommandMenuManager;
        public ItemCommandMenuManager itemCommandMenuManager;
        public TargetCommandMenuManager targetCommandMenuManager;
        public MoveCommandMenuManager moveCommandMenuManager;

        public CommandMenuManager activeCommandMenuManager;
        public CommandMenuManager prevCommandMenuManager;
        
        private void Awake()
        {
            commandMenus = new List<CommandMenuManager>();
            commandMenus.Add(commandSelectMenuManager);
            commandMenus.Add(skillCommandMenuManager);
            commandMenus.Add(targetCommandMenuManager);
            commandMenus.Add(itemCommandMenuManager);
            commandMenus.Add(actionCommandSelectMenu);
            commandMenus.Add(moveCommandMenuManager);
        }
        
        
        
        private void OnEnable()
        {
            ToggleCommandMenuHolderOff();
            EventManager.Instance.combatEvents.onPlayerTurn += ToggleActionMenuHolderOn;
            EventManager.Instance.combatEvents.onPlayerTurnEnd += ToggleCommandMenuHolderOff;
          
        }

        private void OnDisable()
        {
            //EventManager.Instance.combatEvents.onPlayerTurn -= ToggleCommandMenuHolderOn;
        }


        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {

                if (ActiveCommandMenuManager == targetCommandMenuManager)
                {
                    EventManager.Instance.combatEvents.OnPlayerSelectTargetEnd(null);
                    TogglePrevMenu();
                }
                else
                {
                    ActiveCommandMenuManager = commandSelectMenuManager;
                }
                
            }
        }


        public void ToggleCombatCommandMenuHolderOn()
        {
            actionMenusHolder.gameObject.SetActive(true);
        }

        public void ToggleActionMenuHolderOn(CharacterBattleManager characterBattleManager = null)
        {
            ToggleCombatCommandMenuHolderOn();
            ActiveCommandMenuManager = commandSelectMenuManager;
            this.characterBattleManager = characterBattleManager;
            characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>().character.GetCharacterPortrait();
            characterName.text = characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName;
        }
        
        
        public void ToggleCommandMenuHolderOn(CharacterBattleManager characterBattleManager = null)
        {
            //combatCommandMenu.characterBattleManager = characterBattleManager;
            ToggleCombatCommandMenuHolderOn();
        }
        
        public void ToggleCommandMenuHolderOff()
        {
            actionMenusHolder.gameObject.SetActive(false);
            ActiveCommandMenuManager = null;
            
            
        }

        public void SetCharacterSkillDetails()
        {
            
        }

        public CommandMenuManager ActiveCommandMenuManager
        {
            get { return activeCommandMenuManager; }
            set
            {
                SetCurrentMenuAsPrev();
                
                
                if (value != activeCommandMenuManager || value == null)
                {
                    activeCommandMenuManager = value;
                    
                    SetCommandMenus();
                }
            }
        }
        
        
        
        public void SetCommandMenus()
        {
            foreach (CommandMenuManager commandMenu in commandMenus)
            {
                if (commandMenu == activeCommandMenuManager)
                {
                    commandMenu.gameObject.SetActive(true);
                }
                else
                {
                    commandMenu.gameObject.SetActive(false);
                }
            }
        }
        
        
        public void ToggleSkillMenu()
        {
            ActiveCommandMenuManager = skillCommandMenuManager;
        }
        
        public void ToggleItemMenu()
        {
            
            ActiveCommandMenuManager = itemCommandMenuManager;
        }
        
        public void ToggleTargetMenu()
        {
            
            ActiveCommandMenuManager = targetCommandMenuManager;
        }
        
        public void ToggleMoveActionMenu()
        {
           
            ActiveCommandMenuManager = moveCommandMenuManager;
        }

        public void TogglePrevMenu()
        {
            
            ActiveCommandMenuManager = prevCommandMenuManager;
        }
        
        public void SetCurrentMenuAsPrev()
        {
            if (ActiveCommandMenuManager != null)
            {
                prevCommandMenuManager = ActiveCommandMenuManager;
            }
            
        }
        
        
    }
}