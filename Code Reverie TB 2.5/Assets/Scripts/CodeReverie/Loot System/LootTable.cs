using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class LootTable
    {
        public LootTableDataContainer lootTableDetails;

        public LootTable(LootTableDataContainer lootTableDetails)
        {
            this.lootTableDetails = lootTableDetails;
        }

        public List<ItemInfo> RunLootTable()
        {
            List<ItemInfo> itemsToDrop = new List<ItemInfo>();

            foreach (KeyValuePair<ItemInfo, float> lootTable in lootTableDetails.lootTable)
            {
                float randomNum = Random.Range(1, 101);

                if (randomNum <= lootTable.Value)
                {
                    itemsToDrop.Add(lootTable.Key);
                }
                
            }
            
            
            return itemsToDrop;
        }
        
    }
}