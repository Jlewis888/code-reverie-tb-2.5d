using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeReverie
{
    public class InventoryPauseMenu : PauseMenu
    {
        public InventoryMenuNavigationState inventoryMenuNavigationState;
        public InventoryPauseMenuNavigationButton inventoryPauseMenuNavigationButtonPF;
        public GameObject inventoryPauseMenuNavigationButtonHolder;
        public List<InventoryPauseMenuNavigationButton> inventoryPauseMenuNavigationButtonList;
        public List<InventoryFilterButton> inventoryFilterButtons;
        public int itemFilterIndex;
        

        public MenuNavigation inventoryMenuNavigation;

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            pauseMenuNavigation.pauseMenuNavigationButtons = pauseMenuNavigationHolder.GetComponentsInChildren<PauseMenuNavigationButton>().ToList();
            pauseMenuNavigation.SetFirstItem();
            
            inventoryMenuNavigation = new MenuNavigation();
            inventoryMenuNavigation.callBack = OnSelectedNavigationButtonChange;
        }
        
        private void OnEnable()
        {
            pauseMenuNavigation.SetFirstItem();
            EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange += OnPauseMenuSubNavigationStateChange;
            inventoryPauseMenuNavigationButtonList = new List<InventoryPauseMenuNavigationButton>();
            
            ClearInventoryNavigationButtons();
            SetInventoryItems();
            itemFilterIndex = 0;
            SetFilters();
            
            inventoryMenuNavigation.SetFirstItem();
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange -= OnPauseMenuSubNavigationStateChange;
            inventoryPauseMenuNavigationButtonList = new List<InventoryPauseMenuNavigationButton>();
            ClearInventoryNavigationButtons();
        }

        private void Update()
        {

            switch (inventoryMenuNavigationState)
            {
                case InventoryMenuNavigationState.Menu:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }
                    
                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                    
                        EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.Menu);
                    
                    }
                    
                    
                    pauseMenuNavigation.NavigationInputUpdate();
                    break;
                case InventoryMenuNavigationState.Inventory:
                    if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                    {
                        Confirm();
                    }

                    if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                    {
                        inventoryMenuNavigationState = InventoryMenuNavigationState.Menu;
                    }

                    if (GameManager.Instance.playerInput.GetButtonDown("Navigate Menu Horizontal Button"))
                    {
                        itemFilterIndex++;

                        if (itemFilterIndex >= inventoryFilterButtons.Count + 1)
                        {
                            itemFilterIndex = 0;
                        }

                        SetFilters();
                    }
                    else if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Menu Horizontal Button"))
                    {
                        itemFilterIndex--;

                        if (itemFilterIndex < 0)
                        {
                            itemFilterIndex = inventoryFilterButtons.Count - 1;
                        }

                        SetFilters();
                    }

                    inventoryMenuNavigation.NavigationInputUpdate();
                    break;
            }
        }

        private void SetFilters()
        {
            if (inventoryFilterButtons[itemFilterIndex].applyAllFilter)
            {
                FilterAll();
            }
            else
            {
                FilterCategory(inventoryFilterButtons[itemFilterIndex].itemType);
            }
        }

        public void FilterAll()
        {
            inventoryMenuNavigation.ResetNavigationList();
            foreach (PauseMenuNavigationButton pauseMenuNavigationItem in inventoryPauseMenuNavigationButtonList)
            {
                pauseMenuNavigationItem.gameObject.SetActive(true);
                inventoryMenuNavigation.Add(pauseMenuNavigationItem
                    .GetComponent<InventoryPauseMenuNavigationButton>());
            }
        }

        public void FilterCategory(ItemType itemTypeFilter)
        {
            inventoryMenuNavigation.ResetNavigationList();
            foreach (PauseMenuNavigationButton pauseMenuNavigationItem in inventoryPauseMenuNavigationButtonList)
            {
                // if (pauseMenuNavigationItem.GetComponent<InventoryPauseMenuNavigationButton>().item.info.itemType ==
                //     itemTypeFilter) 

                ItemSubType itemSubType = pauseMenuNavigationItem.GetComponent<InventoryPauseMenuNavigationButton>()
                    .item.info.itemSubType;
                
                if (ItemManager.Instance.itemTypeMap[itemTypeFilter].Contains(itemSubType))
                {
                    pauseMenuNavigationItem.gameObject.SetActive(true);
                    inventoryMenuNavigation.Add(pauseMenuNavigationItem
                        .GetComponent<InventoryPauseMenuNavigationButton>());
                }
                else
                {
                    pauseMenuNavigationItem.gameObject.SetActive(false);
                }
            }
        }
        
        public void FilterSubCategory(ItemSubType itemTypeFilter)
        {
            inventoryMenuNavigation.ResetNavigationList();
            foreach (PauseMenuNavigationButton pauseMenuNavigationItem in inventoryPauseMenuNavigationButtonList)
            {
                if (pauseMenuNavigationItem.GetComponent<InventoryPauseMenuNavigationButton>().item.info.itemSubType == itemTypeFilter) 
                {
                    pauseMenuNavigationItem.gameObject.SetActive(true);
                    inventoryMenuNavigation.Add(pauseMenuNavigationItem
                        .GetComponent<InventoryPauseMenuNavigationButton>());
                }
                else
                {
                    pauseMenuNavigationItem.gameObject.SetActive(false);
                }
            }
        }


        private void Confirm()
        {
            
            switch (inventoryMenuNavigationState)
            {
                case InventoryMenuNavigationState.Menu:
                    if (pauseMenuNavigation.SelectedNavigationButton.GetComponent<ItemFilterMenuNavigationButton>()
                        .applyAllFilter)
                    {
                        FilterAll();
                        inventoryMenuNavigationState = InventoryMenuNavigationState.Inventory;
                    }
                    else
                    {
                        FilterCategory(pauseMenuNavigation.SelectedNavigationButton.GetComponent<ItemFilterMenuNavigationButton>().itemType);
                        inventoryMenuNavigationState = InventoryMenuNavigationState.Inventory;
                    }
                    
                    break;
                case InventoryMenuNavigationState.Inventory:
                    if (inventoryMenuNavigation.SelectedNavigationButton
                        .GetComponent<InventoryPauseMenuNavigationButton>().item.info.canUseInMenu)
                    {
                        EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState
                            .CurrentPartyMenuManager);

                        EventManager.Instance.inventoryEvents.OnItemMenuSelect(inventoryMenuNavigation.SelectedNavigationButton
                            .GetComponent<InventoryPauseMenuNavigationButton>().item);
                    }
                    break;
            }
        }


        public void SetInventoryItems()
        {
            foreach (Item item in PlayerManager.Instance.inventory.items)
            {
                InventoryPauseMenuNavigationButton inventoryPauseMenuNavigationButton =
                    Instantiate(inventoryPauseMenuNavigationButtonPF, inventoryPauseMenuNavigationButtonHolder.transform);

                inventoryPauseMenuNavigationButton.item = item;
                inventoryPauseMenuNavigationButton.nameText.text = item.info.itemName;
                inventoryPauseMenuNavigationButton.inventoryCountText.text = item.amount.ToString();

                inventoryPauseMenuNavigationButtonList.Add(inventoryPauseMenuNavigationButton);
            }
        }

        public void OnPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState pauseMenuNavigationState)
        {
            //navigationDelayTimer = navigationDelay;
            pauseMenuSubNavigationState = pauseMenuNavigationState;
        }
        
        public void ClearInventoryNavigationButtons()
        {
            //pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
            
            foreach (Transform child in inventoryPauseMenuNavigationButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        
    }
}