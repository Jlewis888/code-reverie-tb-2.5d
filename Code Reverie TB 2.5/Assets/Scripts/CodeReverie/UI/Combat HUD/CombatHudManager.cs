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
        //public CommandMenu commandMenu;
        public GameObject characterActionSliderHolder;
        public CharacterActionSlider characterActionSliderPF;
        public List<CharacterActionSlider> characterActionSliders;
        public CommandWheelPanelManager commandWheelPanelManager;
        public TargetInfoPanel targetInfoPanel;
        
        private void Awake()
        {
            //currentEnemyHealthPanel.gameObject.SetActive(false);
            //partyHudPanelManager.SetListeners();
        }


        private void OnEnable()
        {
            targetInfoPanel.gameObject.SetActive(false);
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
                CharacterActionSlider characterActionSlider = Instantiate(characterActionSliderPF, characterActionSliderHolder.transform);
                characterActionSlider.characterBattleManager = characterBattleManager;
                characterActionSlider.name =
                    $"{characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName} Slider";
                characterActionSlider.SetSliderIconPosition();
                characterActionSlider.Init();
                characterActionSliders.Add(characterActionSlider);
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
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterBattleManager = characterBattleManager;
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterBattleManager = characterBattleManager;
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>().character.GetCharacterPortrait();
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterName.text = characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName;
            CameraManager.Instance.SetSelectedPlayerWeight(characterBattleManager, 10f, 1.3f);
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.SetCharacterSkillDetails();

        }
        
        
        public void OnActionSelected()
        {
            targetInfoPanel.gameObject.SetActive(false);
            EventManager.Instance.combatEvents.OnCombatPause(false);
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.characterBattleManager = null;
            
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