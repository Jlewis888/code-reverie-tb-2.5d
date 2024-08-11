using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class CraftingPauseMenu : PauseMenu
    {
        public CraftingItemPauseMenuNavigationButton craftingItemPauseMenuNavigationPF;
        
        public GameObject craftIngredientItemPanelHolder;
        public CraftIngredientItemPanelUI craftIngredientItemPanelPF;
        
        public MenuNavigation pauseMenuNavigation;

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            pauseMenuNavigation.callBack = OnSelectedNavigationButtonChange;
            ClearNavigationButtons();
        }
        
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.ToggleCharacterSidePanelUI(false);
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
            CraftItem();
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
        
        public void CraftItem()
        {
           
            ItemInfo selectedItemInfo = pauseMenuNavigation.SelectedNavigationButton
                .GetComponent<CraftingItemPauseMenuNavigationButton>().itemInfo;
            
            if (selectedItemInfo != null && CanCraft())
            {
                
                foreach (KeyValuePair<ItemInfo, int> item in selectedItemInfo.requiredItems)
                {
                    PlayerManager.Instance.inventory.RemoveItem(item.Key, item.Value);
                }
                
                PlayerManager.Instance.inventory.AddItem(new Item(selectedItemInfo));
                EventManager.Instance.playerEvents.OnItemCrafted(selectedItemInfo.id);
                EventManager.Instance.playerEvents.OnItemPickup(selectedItemInfo.id, 1);
                
                
            }
            
        }

        public bool CanCraft()
        {
            ItemInfo selectedItemInfo = pauseMenuNavigation.SelectedNavigationButton
                .GetComponent<CraftingItemPauseMenuNavigationButton>().itemInfo;
            
            if (selectedItemInfo == null)
            {
                return false;
            }


            if (selectedItemInfo.requiredItems.Count < 1)
            {
                return false;
            }

            foreach (KeyValuePair<ItemInfo, int> item in selectedItemInfo.requiredItems)
            {
                (bool, int) itemCheck = PlayerManager.Instance.inventory.ItemInInventory(item.Key);
                
                if (!itemCheck.Item1)
                {
                    
                    return false;
                }
                
                // Debug.Log(item.Value);
                // Debug.Log(PlayerManager.Instance.inventory.items[itemCheck.Item2].amount);
                
                if (item.Value > PlayerManager.Instance.inventory.items[itemCheck.Item2].amount)
                {
                    return false;
                }
                
            }
            
            

            return true;
        }
        
        
        
        public void OnPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState pauseMenuNavigationState)
        {
            //navigationDelayTimer = navigationDelay;
            pauseMenuSubNavigationState = pauseMenuNavigationState;
        }

        public override void OnSelectedNavigationButtonChange()
        {
            foreach (Transform child in craftIngredientItemPanelHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            
            foreach (KeyValuePair<ItemInfo, int> itemInfoRequiredItem in pauseMenuNavigation.SelectedNavigationButton.GetComponent<CraftingItemPauseMenuNavigationButton>().itemInfo.requiredItems)
            {
                CraftIngredientItemPanelUI craftIngredientItemPanelUI =
                    Instantiate(craftIngredientItemPanelPF, craftIngredientItemPanelHolder.transform);
                
                (bool, int) inventorySlotCheck = PlayerManager.Instance.inventory.ItemInInventory(itemInfoRequiredItem.Key);

                craftIngredientItemPanelUI.requiredAmount = itemInfoRequiredItem.Value;
                craftIngredientItemPanelUI.nameText.text = itemInfoRequiredItem.Key.itemName;
                
                if (inventorySlotCheck.Item1)
                {
                    craftIngredientItemPanelUI.item = PlayerManager.Instance.inventory.items[inventorySlotCheck.Item2];
                }
                
            }
        }
    }
}