using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class ItemManager : ManagerSingleton<ItemManager>
    {

        public BaseItemDetailsListContainer baseItemDetailsListContainer;
        public GearSetDataContainerList gearSets;
        public GemSetDataContainerList gemSets;
        
        public Dictionary<string, ItemInfo> allItemInfoMap;
        public Dictionary<GearSetType, GearSetDataContainer> gearSetDataContainersMap =
            new Dictionary<GearSetType, GearSetDataContainer>();

        public Dictionary<GemSetType, GemSetDataContainer> gemSetDataContainersMap =
            new Dictionary<GemSetType, GemSetDataContainer>();

        public Dictionary<ItemRarity, ItemRarityData> itemRarityMap = new Dictionary<ItemRarity, ItemRarityData>();
        public Dictionary<ItemType, List<ItemSubType>> itemTypeMap = new Dictionary<ItemType, List<ItemSubType>>();

        protected override void Awake()
        {
            base.Awake();
            SetAllItemsMap();
            gearSetDataContainersMap = gearSets.gearSetDataContainersMap;
            gemSetDataContainersMap = gemSets.GemSetDataContainersMap;

            itemTypeMap = new Dictionary<ItemType, List<ItemSubType>>();

            List<ItemSubType> consumableList = new List<ItemSubType>();
            consumableList.Add(ItemSubType.Consumable);
            itemTypeMap.Add(ItemType.Consumable, consumableList);
            
            List<ItemSubType> relicList = new List<ItemSubType>();
            relicList.Add(ItemSubType.GluttonRelic);
            relicList.Add(ItemSubType.WrathRelic);
            relicList.Add(ItemSubType.PrideRelic);
            relicList.Add(ItemSubType.EnvyRelic);
            relicList.Add(ItemSubType.GreedRelic);
            relicList.Add(ItemSubType.LustRelic);
            relicList.Add(ItemSubType.SlothRelic);
            itemTypeMap.Add(ItemType.Relic, relicList);
            
            
            List<ItemSubType> materialList = new List<ItemSubType>();
            materialList.Add(ItemSubType.Material);
            itemTypeMap.Add(ItemType.Material, materialList);

        }


        public ItemInfo GetItemDetails(string id)
        {

            if (id == null)
            {
                return null;
            }
            
            if (allItemInfoMap.ContainsKey(id))
            {
                return allItemInfoMap[id];
            }
            
            return null;
        } 
        

        public void SetAllItemsMap()
        {

            allItemInfoMap = new Dictionary<string, ItemInfo>();
            
            foreach (ItemInfo itemInfo in baseItemDetailsListContainer.items)
            {
                if (!allItemInfoMap.ContainsKey(itemInfo.id))
                {
                    allItemInfoMap.Add(itemInfo.id, itemInfo);
                }
                else
                {
                    Debug.Log($"Duplicate Key: {itemInfo.id}");
                }
            }
        }

        public Item CreateItem(ItemInfo info, bool createItemFromType = false)
        {
            if (info != null)
            {

                if (createItemFromType)
                {
                    Type itemType = Type.GetType($"CodeReverie.{GetItemDetails(info.id)}");
          
                    if (itemType != null)
                    {
                        return (Item)Activator.CreateInstance(itemType, new [] {GetItemDetails(info.id)});
                    }
                    else
                    {
                        Debug.Log("SKill is null");
                    } 
                }
                else
                {
                    return new Item(info);
                }
                
                
                
            }

            return null;
        }

        public Item CreateItem(string id)
        {
            if (GetItemDetails(id) != null)
            {
                Type itemType = Type.GetType($"CodeReverie.{GetItemDetails(id)}");
          
                if (itemType != null)
                {
                    return (Item)Activator.CreateInstance(itemType, new [] {GetItemDetails(id)});
                }
            }

            return null;

        }
        
        
        
        public bool CheckIfGearSetBonusKeyExist(GearSetType gearSetType, int setBonusNumber)
        {
            if (gearSetDataContainersMap[gearSetType].gearSetBonusMap.ContainsKey(setBonusNumber))
            {
                return true;
            }

            return false;
        }
        
        public bool CheckIfGemSetBonusKeyExist(GemSetType gemSetType, int setBonusNumber)
        {
            if (gemSetDataContainersMap[gemSetType].gemSetBonusMap.ContainsKey(setBonusNumber))
            {
                return true;
            }

            return false;
        }
        
        public GearSetBonus GetGearSetBonus(GearSetType gearSetType, int setBonusNumber)
        {
            return gearSetDataContainersMap[gearSetType].gearSetBonusMap[setBonusNumber];
        }
        
        public GemSetBonus GetGemSetBonus(GemSetType gemSetType, int setBonusNumber)
        {
            return gemSetDataContainersMap[gemSetType].gemSetBonusMap[setBonusNumber];
        }
    }
}