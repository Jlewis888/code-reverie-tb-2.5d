using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "LootTableDataContainer", menuName = "Scriptable Objects/Loot/LootTableDataContainer", order = 1)]
    public class LootTableDataContainer : SerializedScriptableObject
    {
        public string id;
        public Dictionary<ItemInfo, float> lootTable = new Dictionary<ItemInfo, float>();
        public int minLevel;
        public int maxLevel;
        
        private void OnValidate()
        {
            #if UNITY_EDITOR
                id = name;

                if (minLevel > maxLevel)
                {
                    minLevel = maxLevel - 1;
                }
                
                UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
        
    }
}