using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CommandWheelPanelManager : SerializedMonoBehaviour
    {
        public List<CommandWheelPanel> commandWheelPanelList = new List<CommandWheelPanel>();
        public CommandWheelPanel activeCommandWheelPanel;
        public CommandWheelPanel previousCommandWheelPanel;

        public CommandWheelPanel actionCommandWheelPanel;
        public CommandWheelPanel skillsCommandWheelPanel;
        public CommandWheelPanel itemCommandWheelPanel;
        

        private void Awake()
        {
            commandWheelPanelList = GetComponentsInChildren<CommandWheelPanel>(true).ToList();
            ActiveCommandWheelPanel = null;
        }

        private void OnEnable()
        {
            //ToggleActionCommandWheelOff();
            EventManager.Instance.combatEvents.onPlayerSelectTarget += OnPlayerSelectTarget;
            EventManager.Instance.combatEvents.onPrevCommandWheelSelect += ReturnToPreviousCommandWheel;
            EventManager.Instance.combatEvents.onPlayerTurn += ToggleActionCommandWheelOn;
            EventManager.Instance.combatEvents.onPlayerTurnEnd += ToggleCommandWheelOff;
            EventManager.Instance.combatEvents.onActionCommandWheelSelect += ToggleActionCommandWheelOn;
            EventManager.Instance.combatEvents.onSkillCommandWheelSelect += ToggleSkillCommandWheelOn;
            EventManager.Instance.combatEvents.onItemCommandWheelSelect += ToggleItemCommandWheelOn;
        }

        

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onPlayerSelectTarget -= OnPlayerSelectTarget;
            EventManager.Instance.combatEvents.onPrevCommandWheelSelect -= ReturnToPreviousCommandWheel;
            EventManager.Instance.combatEvents.onPlayerTurn -= ToggleActionCommandWheelOn;
            EventManager.Instance.combatEvents.onPlayerTurnEnd -= ToggleCommandWheelOff;
            EventManager.Instance.combatEvents.onActionCommandWheelSelect -= ToggleActionCommandWheelOn;
            EventManager.Instance.combatEvents.onSkillCommandWheelSelect -= ToggleSkillCommandWheelOn;
            EventManager.Instance.combatEvents.onItemCommandWheelSelect -= ToggleItemCommandWheelOn;
        }

        public CommandWheelPanel ActiveCommandWheelPanel
        {
            get {return activeCommandWheelPanel; }
            set
            {
                SetCurrentPanelAsPrev();
                // if (value == null)
                // {
                //     activeCommandWheelPanel = value;
                //     SetCommandWheelPanels();
                //     return;
                // }
                
                if (value != activeCommandWheelPanel || value == null)
                {
                    activeCommandWheelPanel = value;
                    SetCommandWheelPanels();
                }
            }
        }
        
        public void ToggleActionCommandWheelOn()
        {
            ToggleActionCommandWheelOn(null);
        }
        
        public void ToggleActionCommandWheelOn(CharacterBattleManager characterBattleManager = null)
        {
            ActiveCommandWheelPanel = actionCommandWheelPanel;
        }
        
        public void ToggleSkillCommandWheelOn()
        {
            ActiveCommandWheelPanel = skillsCommandWheelPanel;
        }
        
        public void ToggleItemCommandWheelOn()
        {
            ActiveCommandWheelPanel = itemCommandWheelPanel;
        }
        
        public void ToggleCommandWheelOff()
        {
            ActiveCommandWheelPanel = null;
        }
        
        
        public void ToggleActionCommandWheelOff(CharacterBattleManager characterBattleManager = null)
        {
            ActiveCommandWheelPanel = null;
        }
        
        private void OnPlayerSelectTarget(CharacterBattleManager obj)
        {
            ActiveCommandWheelPanel = null;
        }

        public void SetCurrentPanelAsPrev()
        {
            if (ActiveCommandWheelPanel != null)
            {
                previousCommandWheelPanel = ActiveCommandWheelPanel;
            }
            
        }

        public void SetCommandWheelPanels()
        {
            foreach (CommandWheelPanel commandWheelPanel in commandWheelPanelList)
            {
                commandWheelPanel.gameObject.SetActive(false);
                if (commandWheelPanel == activeCommandWheelPanel)
                {
                    commandWheelPanel.gameObject.SetActive(true);
                }
            }
        }

        public void ReturnToPreviousCommandWheel()
        {
            ActiveCommandWheelPanel = previousCommandWheelPanel;
        }
    }
}