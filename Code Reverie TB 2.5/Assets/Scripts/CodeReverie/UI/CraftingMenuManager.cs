using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CraftingMenuManager : MenuManager
    {
        public QuestMenuItemUI questMenuItemUIPF;
        public GameObject questMenuItemHolder;
        //public List<SelectCraftingItemButtonUI> selectCraftingItemButtonUIs;

        public Button craftItemButton;
        //public SelectCraftingItemButtonUI selectCraftingItemButtonUIPF;
        public GameObject selectCraftingItemButtonHolder;
        public ItemInfo selectedItemInfo;
        public bool canCraft;

        private void Awake()
        {
            
            // mainQuestFilterButton.onClick.AddListener(FilterMainQuest);
            // sideQuestFilterButton.onClick.AddListener(FilterSideQuest);
            // completedQuestFilterButton.onClick.AddListener(FilterCompletedQuest);
            
            craftItemButton.onClick.AddListener(CraftItem);
            
        }

        private void OnEnable()
        {
            Clear();
            // foreach (Quest quest in QuestManager.Instance.quests.Values)
            // {
            //     if (quest.Status == QuestStatus.Active || quest.Status == QuestStatus.Complete)
            //     {
            //         QuestMenuItemUI questMenuItemUI = Instantiate(questMenuItemUIPF, questMenuItemHolder.transform);
            //
            //         questMenuItemUI.quest = quest;
            //         questMenuItemUI.SetQuestData();
            //         questMenuItemUis.Add(questMenuItemUI);
            //     }
            // }

            foreach (ItemInfo itemInfo in ItemManager.Instance.allItemInfoMap.Values)
            {

                if (!itemInfo.craftable)
                {
                    continue;
                }
                
                // SelectCraftingItemButtonUI selectCraftingItemButtonUI = Instantiate(selectCraftingItemButtonUIPF,
                //     selectCraftingItemButtonHolder.transform);
                //
                // selectCraftingItemButtonUI.itemInfo = itemInfo;
                // selectCraftingItemButtonUI.itemNameText.text = itemInfo.itemName;
                // selectCraftingItemButtonUIs.Add(selectCraftingItemButtonUI);
                //
                // selectCraftingItemButtonUI.button.onClick.AddListener(()=>SelectItem(itemInfo));
            }
            
            
            //FilterMainQuest();
        }

        private void OnDisable()
        {
            Clear();
        }

        public void Clear()
        {
            foreach (Transform child in selectCraftingItemButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }

            selectedItemInfo = null;

            //selectCraftingItemButtonUIs = new List<SelectCraftingItemButtonUI>();
        }

        public void RefreshCrafting()
        {

            if (selectedItemInfo == null)
            {
                return;
            } 
            
            canCraft = true;


            if (selectedItemInfo.requiredItems.Count < 1)
            {
                canCraft = false;
                return;
            }
            

            foreach (KeyValuePair<ItemInfo, int> item in selectedItemInfo.requiredItems)
            {
                (bool, int) itemCheck = PlayerManager.Instance.inventory.ItemInInventory(item.Key);
                
                if (!itemCheck.Item1)
                {
                    canCraft = false;
                    
                    
                    return;
                }
                
                if (item.Value < PlayerManager.Instance.inventory.items[itemCheck.Item2].amount)
                {
                    canCraft = false;
                    return;
                }
                
            }
        }

        public void SelectItem(ItemInfo itemInfo)
        {
            selectedItemInfo = itemInfo;

            RefreshCrafting();
        }

        public void CraftItem()
        {
            if (selectedItemInfo != null && canCraft)
            {
                Debug.Log("Craft Item");
                
                foreach (KeyValuePair<ItemInfo, int> item in selectedItemInfo.requiredItems)
                {
                    PlayerManager.Instance.inventory.RemoveItem(item.Key, item.Value);
                }
                
                PlayerManager.Instance.inventory.AddItem(new Item(selectedItemInfo));
                EventManager.Instance.playerEvents.OnItemCrafted(selectedItemInfo.id);
                EventManager.Instance.playerEvents.OnItemPickup(selectedItemInfo.id, 1);
                
                RefreshCrafting();
                
            }
            
        }

        // public void FilterMainQuest()
        // {
        //     foreach (QuestMenuItemUI questMenuItemUi in questMenuItemUis)
        //     {
        //         if (questMenuItemUi.quest.info.questType == QuestType.Main)
        //         {
        //             questMenuItemUi.gameObject.SetActive(true);
        //         }
        //         else
        //         {
        //             questMenuItemUi.gameObject.SetActive(false);
        //         }
        //     }
        // }
        //
        // public void FilterSideQuest()
        // {
        //     foreach (QuestMenuItemUI questMenuItemUi in questMenuItemUis)
        //     {
        //         if (questMenuItemUi.quest.info.questType == QuestType.Side)
        //         {
        //             questMenuItemUi.gameObject.SetActive(true);
        //         }
        //         else
        //         {
        //             questMenuItemUi.gameObject.SetActive(false);
        //         }
        //     }
        // }
        //
        // public void FilterCompletedQuest()
        // {
        //     foreach (QuestMenuItemUI questMenuItemUi in questMenuItemUis)
        //     {
        //         if (questMenuItemUi.quest.Status == QuestStatus.Complete)
        //         {
        //             questMenuItemUi.gameObject.SetActive(true);
        //         }
        //         else
        //         {
        //             questMenuItemUi.gameObject.SetActive(false);
        //         }
        //     }
        // }
    }
}