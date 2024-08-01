using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "New ItemRarityData", menuName = "Scriptable Objects/Item/ItemRarityData", order = 1)]
    public class ItemRarityData : SerializedScriptableObject
    {
        public ItemRarity itemRarity;
        public string description;
        public Color color;
        public Color headerColor;

#if UNITY_EDITOR
        // private void OnValidate()
        // {
        //     headerColor = color;
        //
        // }
        
#endif
    }
}