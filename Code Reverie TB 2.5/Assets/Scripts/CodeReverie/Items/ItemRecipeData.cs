using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "New Item Recipe Data", menuName = "Scriptable Objects/Item/Item Recipe")]
    public class ItemRecipeData : SerializedScriptableObject
    {
        // public List<ItemInfo> requiredItems = new List<ItemInfo>();
        // public List<ItemIngredient> requiredItemsTest = new List<ItemIngredient>();
        public List<List<ItemIngredient>> requiredItems = new List<List<ItemIngredient>>();
        
        // [SerializeField]
        // public ItemIngredient test;
    }
}