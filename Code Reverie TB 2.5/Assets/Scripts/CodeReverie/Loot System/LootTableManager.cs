using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class LootTableManager : ManagerSingleton<LootTableManager>
    {
        
        public LootTables lootTablesList;
        public Dictionary<string, LootTable> lootTables;
        public Pickup lootDropPF;


        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }
        
        
        

        public bool Initialized { get; set; }
        
        public void Initialize()
        {
            SetLootTables();
            
            Initialized = true;
        }
        
        private void OnEnable()
        {
            //EventManager.Instance.combatEvents.onEnemyDeath += DropLoot;
        }


        public LootTable GetLootTableById(string id)
        {
            if (!lootTables.ContainsKey(id))
            {
                Debug.Log("Need to add to lootables");
                return null; 
            }

            return lootTables[id];

        }

        public void SetLootTables()
        {
            lootTables = new Dictionary<string, LootTable>();
            
            foreach (LootTableDataContainer lootTable in lootTablesList.lootTableDetailsList)
            {
                if (lootTables.ContainsKey(lootTable.id))
                {
                    Debug.Log("Duplicate Key Exist");
                }
                
                lootTables.Add(lootTable.id, new LootTable(lootTable));
            }
        }

        public void DropLoot(CharacterController characterUnit)
        {
            
            

            if (characterUnit.character.info.lootTableDataContainer != null)
            {
               
                foreach (KeyValuePair<ItemInfo, float> lootTable in GetLootTableById(characterUnit.character.info.lootTableDataContainer.id).lootTableDetails.lootTable)
                {
                    float randomNum = Random.Range(1, 101);
            
                    if (randomNum <= lootTable.Value)
                    {
                        Pickup lootDrop = Instantiate(lootDropPF, characterUnit.transform.position,
                            Quaternion.identity);
            
                        lootDrop.spriteRenderer.sprite = lootTable.Key.lootIcon;
                        lootDrop.item = new Item(lootTable.Key);
                        lootDrop.item.info = lootTable.Key;
                        lootDrop.gameObject.SetActive(true);
            
                    }
                
                }
            }
            
            
            
            
        }
        
        
    }
}