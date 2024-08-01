using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class CraftingPauseMenu : PauseMenu
    {
        public CraftingItemPauseMenuNavigationButton craftingItemPauseMenuNavigationPF;
        
        public MenuNavigation pauseMenuNavigation;

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            pauseMenuNavigation.callBack = OnSelectedNavigationButtonChange;
        }
        
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange += OnPauseMenuSubNavigationStateChange;
            
            SetCraftableItemRecipes();
            //ClearNavigationButtons();
            
            pauseMenuNavigation.SetFirstItem();
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange -= OnPauseMenuSubNavigationStateChange;
            ClearNavigationButtons();
        }

        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
            {
                Confirm();
            }

            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {
                EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.Menu);
            }
            
            pauseMenuNavigation.NavigationInputUpdate();
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

        public void SetCraftableItemRecipes()
        {


            pauseMenuNavigation.pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
            
            foreach (ItemInfo itemInfo in ItemManager.Instance.allItemInfoMap.Values)
            {

                if (!itemInfo.craftable)
                {
                    continue;
                }
                
                CraftingItemPauseMenuNavigationButton craftingItemPauseMenuNavigation = Instantiate(craftingItemPauseMenuNavigationPF,
                    pauseMenuNavigationHolder.transform);

                craftingItemPauseMenuNavigation.itemInfo = itemInfo;
                craftingItemPauseMenuNavigation.nameText.text = itemInfo.itemName;
                pauseMenuNavigation.Add(craftingItemPauseMenuNavigation);
                
                //selectCraftingItemButtonUI.button.onClick.AddListener(()=>SelectItem(itemInfo));
            }
        }
        
        public void OnPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState pauseMenuNavigationState)
        {
            //navigationDelayTimer = navigationDelay;
            pauseMenuSubNavigationState = pauseMenuNavigationState;
        }

        public override void OnSelectedNavigationButtonChange()
        {
            
        }
    }
}