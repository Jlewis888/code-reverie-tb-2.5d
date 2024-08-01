using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "LootTables", menuName = "Scriptable Objects/Loot/LootTables", order = 1)]

    public class LootTables : SerializedScriptableObject
    {
        public List<LootTableDataContainer> lootTableDetailsList;

        private void OnValidate()
        {
            #if UNITY_EDITOR
                lootTableDetailsList = lootTableDetailsList.Distinct().ToList();
                UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
        
    }
}