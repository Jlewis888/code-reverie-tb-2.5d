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
        //public CommandMenu commandMenu;
        public GameObject characterActionSliderHolder;
        public CharacterActionSlider characterActionSliderPF;
        
        private void Awake()
        {
            //currentEnemyHealthPanel.gameObject.SetActive(false);
            //partyHudPanelManager.SetListeners();
        }


        private void OnEnable()
        {
            
            CanvasManager.Instance.hudManager.commandMenu.ToggleCommandMenuHolderOff();
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
            BattleManager.Instance.PauseAllAnimations();
            CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.characterBattleManager = characterBattleManager;
            CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.characterBattleManager = characterBattleManager;
            CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>().character.GetCharacterPortrait();
            CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.characterName.text = characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName;
            CameraManager.Instance.SetSelectedPlayerWeight(characterBattleManager, 10f);
            CanvasManager.Instance.hudManager.commandMenu.SetCharacterSkillDetails();

        }
        
        
        public void OnActionSelected()
        {
            EventManager.Instance.combatEvents.OnCombatPause(false);
            CanvasManager.Instance.hudManager.commandMenu.combatCommandMenu.characterBattleManager = null;
        }
        
    }
}