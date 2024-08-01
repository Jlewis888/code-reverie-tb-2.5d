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
        
        
        
        
        public Dictionary<string, ItemInfo> allItemInfoMap;
        public Dictionary<GearSetType, GearSetDataContainer> gearSetDataContainersMap =
            new Dictionary<GearSetType, GearSetDataContainer>();

        public Dictionary<ItemRarity, ItemRarityData> itemRarityMap = new Dictionary<ItemRarity, ItemRarityData>();


        protected override void Awake()
        {
            base.Awake();
            SetAllItemsMap();
            gearSetDataContainersMap = gearSets.gearSetDataContainersMap;
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

        public Item CreateItem(ItemInfo info)
        {
            if (info != null)
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
        
        public GearSetBonus GetGearSetBonus(GearSetType gearSetType, int setBonusNumber)
        {
            return gearSetDataContainersMap[gearSetType].gearSetBonusMap[setBonusNumber];
        }
        
        
    }
}