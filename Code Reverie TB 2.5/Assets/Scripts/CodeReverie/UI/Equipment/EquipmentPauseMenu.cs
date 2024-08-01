using System;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

namespace CodeReverie
{
    public class EquipmentPauseMenu : PauseMenu
    {
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
        

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            equipmentItemMenuNavigation = new MenuNavigation();
        }


        private void OnEnable()
        {
            
            //ClearNavigationButtons();
            ClearPartSlotUI();
            SetPartySlotUI();
            SetNavigationButtons();
            
            partySlotIndex = 0;
            
            SelectedPartySlotNavigationUI = partySlotNavigationUIList[partySlotIndex];
            
            pauseMenuNavigation.SetFirstItem();
        }

        private void OnDisable()
        {
            ClearPartSlotUI();
            SelectedPartySlotNavigationUI = null;
            //ClearNavigationButtons();
        }

        private void Update()
        {
            
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

                if (partySlotIndex == PlayerManager.Instance.availableCharacters.Count + 1)
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
        }

        private void Confirm()
        {
            SetEquipmentNavigationButtons();
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

        public void SetEquipmentNavigationButtons()
        {
            foreach (Transform child in equipItemPauseMenuNavigationButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            
            foreach (Item item in PlayerManager.Instance.inventory.items)
            {

                if (item.info.itemType == pauseMenuNavigation.SelectedNavigationButton.GetComponent<GearSlotUI>().item.info.itemType)
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
        
    }
}