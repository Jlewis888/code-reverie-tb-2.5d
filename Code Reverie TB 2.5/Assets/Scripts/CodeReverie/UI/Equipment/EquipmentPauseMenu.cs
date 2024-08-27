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
    public class EquipmentPauseMenu : PauseMenu
    {
        public EquipItemMenuNavigationState equipItemMenuNavigationState;
        public Image characterPortrait;
        public TMP_Text characterName;
        public PartySlotNavigationUI partySlotNavigationUIPF;
        public PartySlotNavigationUI selectedPartySlotNavigationUI;
        public List<PartySlotNavigationUI> partySlotNavigationUIList;
        public GameObject partySlotNavigationUIHolder;
        public int partySlotIndex;

        public List<GearSlotUI> gearSlotUiList = new List<GearSlotUI>();
        public MenuNavigation pauseMenuNavigation;
        public List<StatMenuPanel> statMenuPanels;
        
        public MenuNavigation equipmentItemMenuNavigation;
        public EquipItemPauseMenuNavigationButton equipItemPauseMenuNavigationButtonPF;
        public GameObject equipItemPauseMenuNavigationButtonPanel;
        public GameObject equipItemPauseMenuNavigationButtonHolder;
        public List<SkillSlotUI> skillSlotUIList = new List<SkillSlotUI>();
        public SkillSlotUI selectedSkillSlotUI;

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            equipmentItemMenuNavigation = new MenuNavigation();
            SetNavigationButtons();
        }


        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onSkillSlotSelect += OnSkillSlotSelect;
            EventManager.Instance.generalEvents.ToggleCharacterSidePanelUI(false);
            equipItemPauseMenuNavigationButtonPanel.SetActive(false);
            //ClearNavigationButtons();
            ClearPartSlotUI();
            SetPartySlotUI();
           
            ToggleSkillSlotsOff();
            
            partySlotIndex = 0;
            
            SelectedPartySlotNavigationUI = partySlotNavigationUIList[partySlotIndex];
            
            pauseMenuNavigation.SetFirstItem();
            
            
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onSkillSlotSelect -= OnSkillSlotSelect;
            selectedSkillSlotUI = null;
            ClearPartSlotUI();
            SelectedPartySlotNavigationUI = null;
            //ClearNavigationButtons();
        }

        private void Start()
        {
            //SetSkillSlots();
        }

        private void Update()
        {

            switch (equipItemMenuNavigationState)
            {
                case EquipItemMenuNavigationState.Menu:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }
            
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState
                            .Menu);
                    }
                    
                    if (GameManager.Instance.playerInput.GetButtonDown("Set Skills"))
                    {
                        ToggleSkillSlotsOn();
                        SetSkillSlots();
                        SetSkillSlotsNavigation();
                        equipItemMenuNavigationState = EquipItemMenuNavigationState.NavigateSkills;
                    }
                    
                    if (GameManager.Instance.playerInput.GetButtonDown("Navigate Menu Horizontal Button"))
                    {
                        partySlotIndex++;

                        if (partySlotIndex >= PlayerManager.Instance.availableCharacters.Count)
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
                            partySlotIndex = PlayerManager.Instance.availableCharacters.Count - 1;
                        }

                        SelectedPartySlotNavigationUI = partySlotNavigationUIList[partySlotIndex];
                    }
                    
                    pauseMenuNavigation.NavigationInputUpdate();
                    
                    break;
                
                case EquipItemMenuNavigationState.EquipItem:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        ConfirmItemSelection();
                    }
            
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        equipItemMenuNavigationState = EquipItemMenuNavigationState.Menu;
                        equipItemPauseMenuNavigationButtonPanel.SetActive(false);
                    }
                    
                    
                    
                    equipmentItemMenuNavigation.NavigationInputUpdate();
                    break;
                case EquipItemMenuNavigationState.NavigateSkills:
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        equipItemMenuNavigationState = EquipItemMenuNavigationState.Menu;
                        ToggleSkillSlotsOff();
                    }
                   
                    break;
            }
            
            
        }

        private void Confirm()
        {
            equipItemPauseMenuNavigationButtonPanel.SetActive(true);
            equipItemMenuNavigationState = EquipItemMenuNavigationState.EquipItem;
            SetEquipmentNavigationButtons();
        }

        void ConfirmSkillSlot()
        {
            equipItemPauseMenuNavigationButtonPanel.SetActive(true);
            equipItemMenuNavigationState = EquipItemMenuNavigationState.EquipSkill;
            SetSkillSlotNavigationButtons();
        }

        void ConfirmItemSelection()
        {
            
            Character character = SelectedPartySlotNavigationUI.character;
            character.characterGear.EquipRelic(equipmentItemMenuNavigation.SelectedNavigationButton.GetComponent<EquipItemPauseMenuNavigationButton>().item);
            SetStatMenuPanels();
            equipItemMenuNavigationState = EquipItemMenuNavigationState.Menu;
            equipItemPauseMenuNavigationButtonPanel.SetActive(false);
        }

        void ConfirmSkillSelection()
        {
            Character character = SelectedPartySlotNavigationUI.character;
            character.characterGear.EquipRelic(equipmentItemMenuNavigation.SelectedNavigationButton.GetComponent<EquipItemPauseMenuNavigationButton>().item);
            SetStatMenuPanels();
            equipItemMenuNavigationState = EquipItemMenuNavigationState.NavigateSkills;
            equipItemPauseMenuNavigationButtonPanel.SetActive(false);
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
            foreach (GearSlotUI gearSlotUI in gearSlotUiList)
            {
                pauseMenuNavigation.pauseMenuNavigationButtons.Add(gearSlotUI);
            }
        }

        public void SetSkillSlots()
        {
            int prevIndex = 0;
            int currentIndex = 0;
            List<SkillSlotUI> prevSkillSlotUIList = new List<SkillSlotUI>();
            
            foreach (PauseMenuNavigationButton pauseMenuNavigationButton in pauseMenuNavigation.pauseMenuNavigationButtons)
            {
                if (pauseMenuNavigationButton.GetComponent<GearSlotUI>().skillSlotUIList.Count > 0)
                {
                    pauseMenuNavigation.pauseMenuNavigationButtons[prevIndex].GetComponent<GearSlotUI>().SetVerticalDownNavigationSkillSlots(pauseMenuNavigationButton.GetComponent<GearSlotUI>().skillSlotUIList);
                    pauseMenuNavigationButton.GetComponent<GearSlotUI>().SetVerticalUpNavigationSkillSlots(prevSkillSlotUIList);
                        
                    prevSkillSlotUIList = pauseMenuNavigationButton.GetComponent<GearSlotUI>().skillSlotUIList;
                    prevIndex = currentIndex;
                }
                
                currentIndex += 1;
            }
        }

        public void SetSkillSlotsNavigation()
        {
            foreach (PauseMenuNavigationButton pauseMenuNavigationButton in pauseMenuNavigation.pauseMenuNavigationButtons)
            {
                if (pauseMenuNavigationButton.GetComponent<GearSlotUI>().skillSlotUIList.Count > 0)
                {
                    EventSystem.current.SetSelectedGameObject(pauseMenuNavigationButton.GetComponent<GearSlotUI>().skillSlotUIList[0].gameObject);
                    return;
                }
            }
        }

        public void ToggleSkillSlotsOn()
        {
            foreach (PauseMenuNavigationButton pauseMenuNavigationButton in pauseMenuNavigation.pauseMenuNavigationButtons)
            {
                pauseMenuNavigationButton.GetComponent<GearSlotUI>().ToggleSkillSlotOn();
            }
        }
        
        public void ToggleSkillSlotsOff()
        {
            foreach (PauseMenuNavigationButton pauseMenuNavigationButton in pauseMenuNavigation.pauseMenuNavigationButtons)
            {
                pauseMenuNavigationButton.GetComponent<GearSlotUI>().ToggleSkillSlotOff();
            }
        }

        public void SetEquipmentNavigationButtons()
        {
            
            foreach (Transform child in equipItemPauseMenuNavigationButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            
            foreach (Item item in PlayerManager.Instance.inventory.items)
            {

                if (item.info.itemType == ItemType.Relic && item.info.gearSlotType == pauseMenuNavigation.SelectedNavigationButton.GetComponent<GearSlotUI>().gearSlotType)
                {
                    EquipItemPauseMenuNavigationButton equipItemPauseMenuNavigationButton =
                        Instantiate(equipItemPauseMenuNavigationButtonPF, equipItemPauseMenuNavigationButtonHolder.transform);

                    equipItemPauseMenuNavigationButton.item = item;
                    equipItemPauseMenuNavigationButton.nameText.text = item.info.itemName;
                    equipItemPauseMenuNavigationButton.inventoryCountText.text = item.amount.ToString();

                    equipmentItemMenuNavigation.Add(equipItemPauseMenuNavigationButton);
                }
                
            }
        }
        
        public void SetSkillSlotNavigationButtons()
        {
            
            foreach (Transform child in equipItemPauseMenuNavigationButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            
            foreach (Item item in PlayerManager.Instance.inventory.items)
            {

                if (item.info.itemType == ItemType.Relic && item.info.itemSubType == selectedSkillSlotUI.itemSubType)
                {
                    EquipItemPauseMenuNavigationButton equipItemPauseMenuNavigationButton =
                        Instantiate(equipItemPauseMenuNavigationButtonPF, equipItemPauseMenuNavigationButtonHolder.transform);

                    equipItemPauseMenuNavigationButton.item = item;
                    equipItemPauseMenuNavigationButton.nameText.text = item.info.itemName;
                    equipItemPauseMenuNavigationButton.inventoryCountText.text = item.amount.ToString();

                    //equipmentItemMenuNavigation.Add(equipItemPauseMenuNavigationButton);
                }
                
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
                        EventManager.Instance.generalEvents.OnPauseMenuCharacterSwap(selectedPartySlotNavigationUI.character);
                        SetStatMenuPanels();
                        characterName.text = value.character.info.characterName;
                        characterPortrait.sprite = value.character.GetCharacterPortrait();
                    }
                }
            }
        }
        
        public void SetStatMenuPanels()
        {
            foreach (StatMenuPanel statMenuPanel in statMenuPanels)
            {
                statMenuPanel.statValueText.text = SelectedPartySlotNavigationUI.character.characterController
                    .GetComponent<CharacterStatsManager>().GetStat(statMenuPanel.statAttribute).ToString();
            }
        }

        public void OnSkillSlotSelect(SkillSlotUI skillSlotUI)
        {
            selectedSkillSlotUI = skillSlotUI;
        }
        
    }
}