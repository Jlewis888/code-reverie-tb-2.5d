using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CombatHudManager : SerializedMonoBehaviour
    {
        
        public ActionBarManager actionBarManager;
        public CurrentEnemyHealthPanel currentEnemyHealthPanel;
        public PartyHudPanelManager partyHudPanelManager;
        public CombatActionMenu commandMenu;
        public GameObject characterActionSliderHolder;
        public CharacterActionSlider characterActionSliderPF;
        public List<CharacterActionSlider> characterActionSliders;
        public CommandWheelPanelManager commandWheelPanelManager;
        public TargetInfoPanel targetInfoPanel;
        public SelectedPartyMemberPanel selectedPartyMemberPanel;

        public ActionGauge actionGauge;
        public GameObject actionGaugeHolder;
        public CharacterActionGauge characterActionGaugePF;
        public List<CharacterActionGauge> characterActionGauges;
        
        private void Awake()
        {
            //currentEnemyHealthPanel.gameObject.SetActive(false);
            //partyHudPanelManager.SetListeners();
        }


        private void OnEnable()
        {
            targetInfoPanel.gameObject.SetActive(false);
            commandMenu.gameObject.SetActive(true);
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCommandMenuHolderOff();
            EventManager.Instance.combatEvents.onPlayerTurn += OnPlayerTurn;
            EventManager.Instance.combatEvents.onActionSelected += OnActionSelected;
            EventManager.Instance.combatEvents.onPlayerSelectTarget += OnPlayerSelectTarget;

        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onEnemyDamageTaken -= EnableEnemyHealthPanel;
            EventManager.Instance.combatEvents.onPlayerTurn -= OnPlayerTurn;
            
            EventManager.Instance.combatEvents.onPlayerSelectTarget -= OnPlayerSelectTarget;
        }


        private void Update()
        {
            if (CombatManager.Instance != null)
            {
                if (!CombatManager.Instance.pause)
                {
                    characterActionSliders.Sort((x,y) => y.slider.value.CompareTo(x.slider.value));

                    for (int i = 0; i < characterActionSliders.Count; i++)
                    {
                        characterActionSliders[i].UpdateCanvasOrder(characterActionSliders.Count - i);
                    }
                }
            }
        }
        
        public void Init()
        {
            
            ClearSliders();
            
            foreach (CharacterBattleManager characterBattleManager in CombatManager.Instance.allUnits)
            {
                // CharacterActionSlider characterActionSlider = Instantiate(characterActionSliderPF, characterActionSliderHolder.transform);
                // characterActionSlider.characterBattleManager = characterBattleManager;
                // characterActionSlider.name =
                //     $"{characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName} Slider";
                // characterActionSlider.SetSliderIconPosition();
                // characterActionSlider.Init();
                // characterActionSliders.Add(characterActionSlider);
                
                CharacterActionGauge characterActionGauge = Instantiate(characterActionGaugePF, actionGaugeHolder.transform);
                characterActionGauge.characterBattleManager = characterBattleManager;
                characterActionGauge.name =
                    $"{characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName} Gauge";
                
                characterActionGauge.SetSliderIconPosition();
                characterActionGauge.Init();
                characterActionGauges.Add(characterActionGauge);
                
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
            CombatManager.Instance.PauseAllAnimations();
            // CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.ToggleCombatCommandMenuHolderOn();
            // CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterBattleManager = characterBattleManager;
            // CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterBattleManager = characterBattleManager;
            // CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>().character.GetCharacterPortrait();
            // CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterName.text = characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName;
            //
            
            // commandMenu.ToggleCombatCommandMenuHolderOn();
            // commandMenu.characterBattleManager = characterBattleManager;
            // commandMenu.characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>().character.GetCharacterPortrait();
            // commandMenu.characterName.text = characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName;
            //
            
            
            
            // Debug.Log("We here arent we?");
            // selectedPartyMemberPanel.gameObject.SetActive(true);
            // selectedPartyMemberPanel.characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>().character.GetCharacterPortrait();
            // selectedPartyMemberPanel.characterName.text = characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName;
            CameraManager.Instance.SetSelectedPlayerWeight(characterBattleManager, 10f, 1.3f);
            //CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.SetCharacterSkillDetails();
            commandMenu.SetCharacterSkillDetails();

        }
        
        
        public void OnActionSelected()
        {
            targetInfoPanel.gameObject.SetActive(false);
            EventManager.Instance.combatEvents.OnCombatPause(false);
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterBattleManager = null;
            selectedPartyMemberPanel.gameObject.SetActive(false);
        }

        public void OnPlayerSelectTarget(CharacterBattleManager characterBattleManager)
        {
            
            targetInfoPanel.gameObject.SetActive(true);
            
            targetInfoPanel.characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>()
                .character.GetCharacterPortrait();

            targetInfoPanel.characterName.text = characterBattleManager.GetComponent<CharacterUnitController>()
                .character.info.characterName;
            
            targetInfoPanel.targetCharacterPortrait.sprite = characterBattleManager.target
                .GetComponent<CharacterUnitController>().character.GetCharacterPortrait();

            switch (characterBattleManager.characterBattleActionState)
            {
                case CharacterBattleActionState.Attack:
                    targetInfoPanel.targetCharacterActionName.text = "Attack";
                    break;
                case CharacterBattleActionState.Defend:
                    targetInfoPanel.targetCharacterActionName.text = "Defend";
                    break;

                case CharacterBattleActionState.Skill:
                    targetInfoPanel.targetCharacterActionName.text = characterBattleManager.selectedSkill.info.skillName;
                    break;

                case CharacterBattleActionState.Item:
                    targetInfoPanel.targetCharacterActionName.text = characterBattleManager.selectedItem.info.itemName;
                    break;
                case CharacterBattleActionState.Break:
                    targetInfoPanel.targetCharacterActionName.text = "Break";
                    break;
            }

            

        }

    }
}