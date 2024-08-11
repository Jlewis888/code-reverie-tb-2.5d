using System;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

namespace CodeReverie
{
    public class SkillPauseMenu : PauseMenu
    {
        public SkillsMenuNavigationState skillsMenuNavigationState;
        public MenuNavigation skillSlotPauseMenuNavigation;
        
        
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

        private void Awake()
        {
            equipSkillPauseMenuNavigation = new MenuNavigation();
            skillSlotPauseMenuNavigation = new MenuNavigation();
        }


        private void OnEnable()
        {
            equipSkillsPanel.SetActive(false);
            EventManager.Instance.generalEvents.ToggleCharacterSidePanelUI(false);
            //ClearNavigationButtons();
            ClearPartSlotUI();
            SetPartySlotUI();
            SetNavigationButtons();

            partySlotIndex = 0;
            
            skillSlotPauseMenuNavigation.SetFirstItem();
            
            SelectedPartySlotNavigationUI = partySlotNavigationUIList[partySlotIndex];
        }

        private void OnDisable()
        {
            ClearPartSlotUI();
            SelectedPartySlotNavigationUI = null;
            //ClearNavigationButtons();
        }

        private void Update()
        {
            switch (skillsMenuNavigationState)
            {
                case SkillsMenuNavigationState.SelectSkillSlot:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }

                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState
                            .Menu);
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
                    
                    skillSlotPauseMenuNavigation.NavigationInputUpdate();

                    break;
                case SkillsMenuNavigationState.EquipSkills:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }

                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        equipSkillsPanel.SetActive(false);
                        skillsMenuNavigationState = SkillsMenuNavigationState.SelectSkillSlot;
                        SetNavigationButtons();
                    }
                    
                    equipSkillPauseMenuNavigation.NavigationInputUpdate();
                    
                    break;
            }
        }

        private void Confirm()
        {
            switch (skillsMenuNavigationState)
            {
                case SkillsMenuNavigationState.SelectSkillSlot:
                    equipSkillsPanel.SetActive(true);
                    skillsMenuNavigationState = SkillsMenuNavigationState.EquipSkills;
                    SetLearnedSkills(skillSlotPauseMenuNavigation.SelectedNavigationButton.GetComponent<SkillSlotPauseMenuNavigationButton>().skillType);
                    break;
                case SkillsMenuNavigationState.EquipSkills:
                    equipSkillsPanel.SetActive(false);
                    skillsMenuNavigationState = SkillsMenuNavigationState.SelectSkillSlot;
                    SetNavigationButtons();
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

        public void SetNavigationButtons()
        {
            skillSlotPauseMenuNavigation.pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
            
            foreach (SkillSlotPauseMenuNavigationButton skillSlotPauseMenuNavigationButton in skillSlotPauseMenuNavigationButtons)
            {
                skillSlotPauseMenuNavigation.pauseMenuNavigationButtons.Add(skillSlotPauseMenuNavigationButton);
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
                    }
                }
            }
        }

        public void SetLearnedSkills(SkillType skillType)
        {
            Character character = selectedPartySlotNavigationUI.character;

            equipSkillPauseMenuNavigation.pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();

            foreach (Transform child in equipSkillsButtonNavigationHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            
            foreach (Skill skill in character.characterSkills.learnedSkills)
            {
                if (skill.info.skillType == skillType)
                {
                    EquipSkillPauseMenuNavigationButton equipSkillPauseMenuNavigationButton =
                        Instantiate(equipSkillPauseMenuNavigationButtonPF, equipSkillsButtonNavigationHolder.transform);
                    
                    equipSkillPauseMenuNavigationButton.skill = skill;
                    equipSkillPauseMenuNavigationButton.nameText.text = skill.info.skillName;
                    
                    equipSkillPauseMenuNavigation.pauseMenuNavigationButtons.Add(equipSkillPauseMenuNavigationButton);
                }
            }
        }
        
    }
}