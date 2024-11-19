using System;
using System.Collections.Generic;
using System.Linq;
using IEVO.UI.uGUIDirectedNavigation;
using Ink.Parsed;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SkillPauseMenu : PauseMenu
    {
        public SkillsMenuNavigationState skillsMenuNavigationState;
        public MenuNavigation skillSlotPauseMenuNavigation;
        public Image characterPortrait;
        public TMP_Text characterName;
        
        public PartySlotNavigationUI partySlotNavigationUIPF;
        public PartySlotNavigationUI selectedPartySlotNavigationUI;
        public List<PartySlotNavigationUI> partySlotNavigationUIList;
        public GameObject partySlotNavigationUIHolder;
        public int partySlotIndex;
        
        public List<SkillSlotPauseMenuNavigationButton> skillSlotPauseMenuNavigationButtons =
            new List<SkillSlotPauseMenuNavigationButton>();
        
        public GameObject equipSkillsPanel;
        public GameObject equipSkillsButtonNavigationHolder;
        public EquipSkillPauseMenuNavigationButton equipSkillPauseMenuNavigationButtonPF;
        public MenuNavigation equipSkillPauseMenuNavigation;
        
        public GameObject learnSkillsPanelContentHolder;
        public LearnSkillPauseMenuNavigationButton selectedLearnSkillPauseMenuNavigationButton;

        public LearnSkillsPanel skillLevel1;
        public LearnSkillsPanel skillLevel2;
        public LearnSkillsPanel skillLevel3;
        public LearnSkillsPanel skillLevel4;
        

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            equipSkillPauseMenuNavigation = new MenuNavigation();
            skillSlotPauseMenuNavigation = new MenuNavigation();
            
            //pauseMenuNavigation.pauseMenuNavigationButtons = pauseMenuNavigationHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            //pauseMenuNavigation.pauseMenuNavigationButtons = GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            
            pauseMenuNavigation.SetFirstItem();
            skillsMenuNavigationState = SkillsMenuNavigationState.LearnSkills;
        }


        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onLearnSkillSlotSelect += OnSkillSlotSelect;
            SelectedPartySlotNavigationUI = null;
            pauseMenuNavigation.SetFirstItem();
            
            equipSkillsPanel.SetActive(false);
            EventManager.Instance.generalEvents.ToggleCharacterSidePanelUI(false);
            //ClearNavigationButtons();
            ClearPartSlotUI();
            SetPartySlotUI();
            //SetNavigationButtons();
            //skillSlotPauseMenuNavigation.SetFirstItem();
            
            partySlotIndex = 0;
            SelectedPartySlotNavigationUI = partySlotNavigationUIList[partySlotIndex];
            
            
            SetArchetypeSkills();
            //pauseMenuNavigation.pauseMenuNavigationButtons = learnSkillsPanelContentHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            
            List<Selectable> pauseMenuNavigationButtonSelectables = pauseMenuNavigation.pauseMenuNavigationButtons.Select(go => go.GetComponent<Selectable>()) // Get the Selectable component
                .Where(selectable => selectable != null)    // Filter out nulls
                .ToList();
            
            Debug.Log(pauseMenuNavigation.pauseMenuNavigationButtons.Count);

            foreach (var pauseMenuNavigationButton in pauseMenuNavigation.pauseMenuNavigationButtons)
            {
                
                pauseMenuNavigationButton.GetComponent<DirectedNavigation>().ConfigRight.Type = DirectedNavigationType.Value.SelectableList;
                pauseMenuNavigationButton.GetComponent<DirectedNavigation>().ConfigLeft.Type = DirectedNavigationType.Value.SelectableList;
                pauseMenuNavigationButton.GetComponent<DirectedNavigation>().ConfigUp.Type = DirectedNavigationType.Value.SelectableList;
                pauseMenuNavigationButton.GetComponent<DirectedNavigation>().ConfigDown.Type = DirectedNavigationType.Value.SelectableList;
                
                pauseMenuNavigationButton.GetComponent<DirectedNavigation>().ConfigRight.SelectableList.SelectableList =
                    pauseMenuNavigationButtonSelectables.ToArray();
                pauseMenuNavigationButton.GetComponent<DirectedNavigation>().ConfigLeft.SelectableList.SelectableList =
                    pauseMenuNavigationButtonSelectables.ToArray();
                pauseMenuNavigationButton.GetComponent<DirectedNavigation>().ConfigUp.SelectableList.SelectableList =
                    pauseMenuNavigationButtonSelectables.ToArray();
                pauseMenuNavigationButton.GetComponent<DirectedNavigation>().ConfigDown.SelectableList.SelectableList =
                    pauseMenuNavigationButtonSelectables.ToArray();
            }
            
           

            if (pauseMenuNavigation.pauseMenuNavigationButtons[0].gameObject != null)
            {
                Debug.Log(pauseMenuNavigation.pauseMenuNavigationButtons[0].gameObject);
                EventSystem.current.SetSelectedGameObject(pauseMenuNavigation.pauseMenuNavigationButtons[0].gameObject);
            }
            
            
        }

        private void OnDisable()
        {
            ClearPartSlotUI();
            SelectedPartySlotNavigationUI = null;
            selectedLearnSkillPauseMenuNavigationButton = null;
            //ClearNavigationButtons();
        }

        private void Update()
        {
            switch (skillsMenuNavigationState)
            {
                case SkillsMenuNavigationState.LearnSkills:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }
                    
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        //skillsMenuNavigationState = SkillsMenuNavigationState.Main;
                        EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.Menu);
                    }
                    
                    if (GameManager.Instance.playerInput.GetButtonDown("Navigate Menu Horizontal Button"))
                    {
                        partySlotIndex++;

                        if (partySlotIndex >= partySlotNavigationUIList.Count)
                        {
                            partySlotIndex = 0;
                        }

                        SelectedPartySlotNavigationUI = partySlotNavigationUIList[partySlotIndex];
                    }
                    else if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Menu Horizontal Button"))
                    {
                        partySlotIndex--;

                        if (partySlotIndex < 0)
                        {
                            partySlotIndex = partySlotNavigationUIList.Count - 1;
                        }

                        SelectedPartySlotNavigationUI = partySlotNavigationUIList[partySlotIndex];
                    }
                    
                    break;
            }
        }

        private void Confirm()
        {
            switch (skillsMenuNavigationState)
            {
                // case SkillsMenuNavigationState.Main:
                //     skillsMenuNavigationState = SkillsMenuNavigationState.SelectSkillSlot;
                //     break;
                case SkillsMenuNavigationState.SelectSkillSlot:
                    equipSkillsPanel.SetActive(true);
                    skillsMenuNavigationState = SkillsMenuNavigationState.EquipSkills;
                    //SetLearnedSkills(skillSlotPauseMenuNavigation.SelectedNavigationButton.GetComponent<SkillSlotPauseMenuNavigationButton>().skillType);
                    break;
                // case SkillsMenuNavigationState.EquipSkills:
                //     
                //     
                //     
                //     SkillDataContainer skillDataContainer = equipSkillPauseMenuNavigation.SelectedNavigationButton
                //         .GetComponent<EquipSkillPauseMenuNavigationButton>().skill.info;
                //     int skillSlotIndex = skillSlotPauseMenuNavigation.SelectedNavigationButton
                //         .GetComponent<SkillSlotPauseMenuNavigationButton>().skillSlotIndex;
                //     
                //     
                //     EquipActionSkill(skillDataContainer, skillSlotIndex);
                //     equipSkillsPanel.SetActive(false);
                //     skillsMenuNavigationState = SkillsMenuNavigationState.SelectSkillSlot;
                //     //SetNavigationButtons();
                //     break;
                
                case SkillsMenuNavigationState.LearnSkills:
                    Debug.Log("Test");
                    LearnSkill();
                    break;
            }
        }


        public void ClearPartSlotUI()
        {
            partySlotNavigationUIList = new List<PartySlotNavigationUI>();

            foreach (Transform child in partySlotNavigationUIHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void SetPartySlotUI()
        {
            foreach (Character character in PlayerManager.Instance.availableCharacters)
            {
                PartySlotNavigationUI partySlotNavigationUI =
                    Instantiate(partySlotNavigationUIPF, partySlotNavigationUIHolder.transform);

                partySlotNavigationUI.character = character;
                partySlotNavigationUI.characterPortrait.sprite = character.GetCharacterPortrait();

                partySlotNavigationUIList.Add(partySlotNavigationUI);
            }
        }
        
        public PartySlotNavigationUI SelectedPartySlotNavigationUI
        {
            get { return selectedPartySlotNavigationUI; }

            set
            {
                if (selectedPartySlotNavigationUI != value)
                {
                    selectedPartySlotNavigationUI = value;

                    if (selectedPartySlotNavigationUI != null)
                    {
                        EventManager.Instance.generalEvents.OnPauseMenuCharacterSwap(selectedPartySlotNavigationUI
                            .character);
                        
                        characterName.text = value.character.info.characterName;
                        characterPortrait.sprite = value.character.GetCharacterPortrait();
                    }
                }
            }
        }
        

        // public void SetLearnedSkills(SkillType skillType)
        // {
        //     Character character = selectedPartySlotNavigationUI.character;
        //
        //     equipSkillPauseMenuNavigation.pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
        //
        //     foreach (Transform child in equipSkillsButtonNavigationHolder.transform)
        //     {
        //         Destroy(child.gameObject);
        //     }
        //     
        //     
        //     foreach (Skill skill in character.characterSkills.learnedSkills)
        //     {
        //         if (skill.info.skillType == skillType)
        //         {
        //             EquipSkillPauseMenuNavigationButton equipSkillPauseMenuNavigationButton =
        //                 Instantiate(equipSkillPauseMenuNavigationButtonPF, equipSkillsButtonNavigationHolder.transform);
        //             
        //             equipSkillPauseMenuNavigationButton.skill = skill;
        //             equipSkillPauseMenuNavigationButton.nameText.text = skill.info.skillName;
        //             
        //             equipSkillPauseMenuNavigation.pauseMenuNavigationButtons.Add(equipSkillPauseMenuNavigationButton);
        //         }
        //     }
        //     
        //     equipSkillPauseMenuNavigation.SetFirstItem();
        // }

        public void SetArchetypeSkills()
        {
            Character character = selectedPartySlotNavigationUI.character;

            

            if (character.equippedArchetype != null)
            {
                skillLevel1.archetypeSkillContainers = character.equippedArchetype.skillsLevel1;
                skillLevel2.archetypeSkillContainers = character.equippedArchetype.skillsLevel2;
                skillLevel3.archetypeSkillContainers = character.equippedArchetype.skillsLevel3;
                skillLevel4.archetypeSkillContainers = character.equippedArchetype.skillsLevel4; 
                
                skillLevel1.SetSkills();
                skillLevel2.SetSkills();
                skillLevel3.SetSkills();
                skillLevel4.SetSkills();
                
                
                pauseMenuNavigation.pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
            
                pauseMenuNavigation.pauseMenuNavigationButtons.AddRange(skillLevel1.pauseMenuNavigationButtons);
                pauseMenuNavigation.pauseMenuNavigationButtons.AddRange(skillLevel2.pauseMenuNavigationButtons);
                pauseMenuNavigation.pauseMenuNavigationButtons.AddRange(skillLevel3.pauseMenuNavigationButtons);
                pauseMenuNavigation.pauseMenuNavigationButtons.AddRange(skillLevel4.pauseMenuNavigationButtons);
              
            }
        }

        public void LearnSkill()
        {
            Character character = selectedPartySlotNavigationUI.character;


            if (selectedLearnSkillPauseMenuNavigationButton != null)
            {
                if (!selectedLearnSkillPauseMenuNavigationButton.archetypeSkillContainer.hasLearned)
                {
                    selectedLearnSkillPauseMenuNavigationButton.archetypeSkillContainer.hasLearned = true;
                }
            }

            character.equippedArchetype.SetLearnedArchetypeSkill();

        }
        
        public void OnSkillSlotSelect(LearnSkillPauseMenuNavigationButton skillSlotUI)
        {
            selectedLearnSkillPauseMenuNavigationButton = skillSlotUI;
        }
        
    }
}