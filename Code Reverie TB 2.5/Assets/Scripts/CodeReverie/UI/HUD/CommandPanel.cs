using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CommandPanel : SerializedMonoBehaviour
    {
        public CharacterBattleManager currentCharacterBattleManager;
        public GameObject currentCommandPanel;
        public GameObject commandsPanel;
        public GameObject actionsPanel;
        public GameObject itemsPanel;
        public GameObject confirmationPanel;
        public GameObject descriptionPanel;
        public Button actionButton;
        public Button itemsButton;

        public List<GameObject> navigationPanels; 


        private void Awake()
        {
            navigationPanels.Add(commandsPanel);
            navigationPanels.Add(actionsPanel);
            navigationPanels.Add(itemsPanel);
            navigationPanels.Add(confirmationPanel);
            actionButton.onClick.AddListener(ActionCommandButton);
            itemsButton.onClick.AddListener(ItemCommandButton);
            
            EventManager.Instance.combatEvents.onPlayerTurn += OnPlayerTurn;
            EventManager.Instance.combatEvents.onActionSelected += OnActionSelected;
        }

        private void OnEnable()
        {
            //SetNavigationPanel(commandsPanel);
            SetNavigationPanel(null);

            

        }

        private void OnDisable()
        {
            SetNavigationPanel(null);
        }


        public GameObject CurrentCommandPanel
        {
            get { return currentCommandPanel; }
            set
            {
                if (currentCommandPanel != value)
                {
                    currentCommandPanel = value;
                }
            }
        }

        public void SetNavigationPanel(GameObject selectedNavigationPanel)
        {
            foreach (GameObject navigationPanel in navigationPanels)
            {
                if (navigationPanel == selectedNavigationPanel)
                {
                    navigationPanel.SetActive(true);
                }
                else
                {
                    navigationPanel.SetActive(false);
                }
            }
        }

        public void ActionCommandButton()
        {
            SetNavigationPanel(actionsPanel);
        }
        
        public void ItemCommandButton()
        {
            SetNavigationPanel(itemsPanel);
        }

        public void OnPlayerTurn(CharacterBattleManager characterBattleManager)
        {
            // EventManager.Instance.combatEvents.OnCombatPause(true);
            // currentCharacterBattleManager = characterBattleManager;
            // SetNavigationPanel(commandsPanel);
            
        }

        public void OnActionSelected()
        {
            // EventManager.Instance.combatEvents.OnCombatPause(false);
            // SetNavigationPanel(null);
            // currentCharacterBattleManager = null;
        }
        
    }
}