using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CombatHudManager : SerializedMonoBehaviour
    {
        
        public ActionBarManager actionBarManager;
        public CurrentEnemyHealthPanel currentEnemyHealthPanel;
        public PartyHudPanelManager partyHudPanelManager;
        public CommandMenu commandMenu;
        public GameObject characterActionSliderHolder;
        public CharacterActionSlider characterActionSliderPF;
        
        private void Awake()
        {
            //currentEnemyHealthPanel.gameObject.SetActive(false);
            //partyHudPanelManager.SetListeners();
        }


        private void OnEnable()
        {
            // CameraManager.Instance.ToggleMainCamera();
            // EventManager.Instance.playerEvents.OnPlayerLock(false);
            // actionBarManager.SetActionBar();
            // GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            // GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 0);
            //
            // EventManager.Instance.combatEvents.onEnemyDamageTaken += EnableEnemyHealthPanel;
            commandMenu.ToggleCommandMenuHolderOff();
            //commandMenu.targetCommandMenuManager.SetInitialEnemyNavigationButtons();
            EventManager.Instance.combatEvents.onPlayerTurn += OnPlayerTurn;
            EventManager.Instance.combatEvents.onActionSelected += OnActionSelected;
            
        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onEnemyDamageTaken -= EnableEnemyHealthPanel;
            EventManager.Instance.combatEvents.onPlayerTurn -= OnPlayerTurn;
        }


        private void Update()
        {
            
        }
        
        public void Init()
        {
            
            ClearSliders();
            
            foreach (CharacterBattleManager characterBattleManager in BattleManager.Instance.allUnits)
            {
                CharacterActionSlider characterActionSlider = Instantiate(characterActionSliderPF, characterActionSliderHolder.transform);
                characterActionSlider.characterBattleManager = characterBattleManager;
                characterActionSlider.SetSliderIconPosition();
                characterActionSlider.Init();
            }
            
            if (PlayerManager.Instance.currentParty != null)
            {
                if (PlayerManager.Instance.currentParty[0] != null)
                {
                   
                    // characterPortrait.sprite =
                    //     PlayerManager.Instance.currentParty[0].GetCharacterPortrait();
                }
            }
            
            //commandMenu.targetCommandMenuManager.SetInitialEnemyNavigationButtons();
            partyHudPanelManager.SetPartyUnitPanels();
            EventManager.Instance.playerEvents.OnActionBarSet();
        }

        public void ClearSliders()
        {
            foreach (Transform child in characterActionSliderHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        


        public void EnableEnemyHealthPanel(DamageProfile damageProfile)
        {
            currentEnemyHealthPanel.health = damageProfile.damageTarget;
            currentEnemyHealthPanel.gameObject.SetActive(true);
        }
        
        public void OnPlayerTurn(CharacterBattleManager characterBattleManager)
        {
            EventManager.Instance.combatEvents.OnCombatPause(true);
            commandMenu.characterBattleManager = characterBattleManager;
            commandMenu.characterBattleManager = characterBattleManager;
            commandMenu.characterPortrait.sprite = characterBattleManager.GetComponent<CharacterController>().character.GetCharacterPortrait();
            commandMenu.characterName.text = characterBattleManager.GetComponent<CharacterController>().character.info.characterName;
            //CameraManager.Instance.UpdateCamera(characterBattleManager.transform);
            CameraManager.Instance.SetSelectedPlayerWeight(characterBattleManager, 10f);
            commandMenu.SetCharacterSkillDetails();
            // currentCharacterBattleManager = characterBattleManager;
            // SetNavigationPanel(commandsPanel);

        }
        
        
        public void OnActionSelected()
        {
            EventManager.Instance.combatEvents.OnCombatPause(false);
            commandMenu.characterBattleManager = null;
            // SetNavigationPanel(null);
            // currentCharacterBattleManager = null;
        }
        
    }
}