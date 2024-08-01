using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class InventoryPauseMenu : PauseMenu
    {
        public InventoryPauseMenuNavigationButton inventoryPauseMenuNavigationButtonPF;
        public List<InventoryPauseMenuNavigationButton> pauseMenuNavigationButtonList;
        public List<InventoryFilterButton> inventoryFilterButtons;
        public int itemFilterIndex;

        public MenuNavigation pauseMenuNavigation;

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            pauseMenuNavigation.callBack = OnSelectedNavigationButtonChange;
        }
        
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange += OnPauseMenuSubNavigationStateChange;
            pauseMenuNavigationButtonList = new List<InventoryPauseMenuNavigationButton>();
            
            ClearNavigationButtons();
            SetInventoryItems();
            itemFilterIndex = 0;
            SetFilters();
            
            pauseMenuNavigation.SetFirstItem();
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange -= OnPauseMenuSubNavigationStateChange;
            pauseMenuNavigationButtonList = new List<InventoryPauseMenuNavigationButton>();
            ClearNavigationButtons();
        }

        private void Update()
        {
            if (pauseMenuSubNavigationState == PauseMenuSubNavigationState.None)
            {
                if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                {
                    Confirm();
                }

                if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                {
                    //if (navigationDelayTimer <= 0)
                   // {
                        EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState
                            .Menu);
                    //}
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

                pauseMenuNavigation.NavigationInputUpdate();
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
            pauseMenuNavigation.ResetNavigationList();
            foreach (PauseMenuNavigationButton pauseMenuNavigationItem in pauseMenuNavigationButtonList)
            {
                pauseMenuNavigationItem.gameObject.SetActive(true);
                pauseMenuNavigation.Add(pauseMenuNavigationItem
                    .GetComponent<InventoryPauseMenuNavigationButton>());
            }
        }

        public void FilterCategory(ItemType itemTypeFilter)
        {
            pauseMenuNavigation.ResetNavigationList();
            foreach (PauseMenuNavigationButton pauseMenuNavigationItem in pauseMenuNavigationButtonList)
            {
                if (pauseMenuNavigationItem.GetComponent<InventoryPauseMenuNavigationButton>().item.info.itemType ==
                    itemTypeFilter)
                {
                    pauseMenuNavigationItem.gameObject.SetActive(true);
                    pauseMenuNavigation.Add(pauseMenuNavigationItem
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
            if (pauseMenuNavigation.SelectedNavigationButton
                .GetComponent<InventoryPauseMenuNavigationButton>().item.info.canUseInMenu)
            {
                EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState
                    .CurrentPartyMenuManager);

                EventManager.Instance.inventoryEvents.OnItemMenuSelect(pauseMenuNavigation.SelectedNavigationButton
                    .GetComponent<InventoryPauseMenuNavigationButton>().item);
            }
        }


        public void SetInventoryItems()
        {
            foreach (Item item in PlayerManager.Instance.inventory.items)
            {
                InventoryPauseMenuNavigationButton inventoryPauseMenuNavigationButton =
                    Instantiate(inventoryPauseMenuNavigationButtonPF, pauseMenuNavigationHolder.transform);

                inventoryPauseMenuNavigationButton.item = item;
                inventoryPauseMenuNavigationButton.nameText.text = item.info.itemName;
                inventoryPauseMenuNavigationButton.inventoryCountText.text = item.amount.ToString();

                pauseMenuNavigationButtonList.Add(inventoryPauseMenuNavigationButton);
            }
        }

        public void OnPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState pauseMenuNavigationState)
        {
            //navigationDelayTimer = navigationDelay;
            pauseMenuSubNavigationState = pauseMenuNavigationState;
        }
    }
}