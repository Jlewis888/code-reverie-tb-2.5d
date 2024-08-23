using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "New Base Item Info", menuName = "Scriptable Objects/Item/Base Item Details")]
    public class ItemInfo : SerializedScriptableObject
    {
        public string id;
        
        public string itemName;
        public string itemDescription;
        public ItemType itemType;
        public ItemSubType itemSubType;
        public ItemRarity itemRarity;
        public bool stackable;
        public int price;
        public Sprite lootIcon;
        public Sprite uiIcon;
        public WeaponInfo weaponInfo;
        public GearSetType gearSetType;
        public Stat primaryStat;
        public List<Stat> baseItemStats = new List<Stat>();
        public bool isInteractivePickup;
        public bool combatItem;
        public bool consumeOnUse;
        public bool craftable;
        public bool canUseInMenu;
        public bool canUseInBattle;
        public List<ItemRecipeData> itemRecipes = new List<ItemRecipeData>();
        public Dictionary<ItemInfo, int> requiredItems = new Dictionary<ItemInfo, int>();
        public TargetType targetType;
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            id = name;
            UnityEditor.EditorUtility.SetDirty(this);
            if (requiredItems == null)
            {
                requiredItems = new Dictionary<ItemInfo, int>();
            }

            if (requiredItems.Count > 0)
            {
                craftable = true;
            }
            else
            {
                craftable = false;
            }

            switch (itemType)
            {
                case ItemType.Weapon:
                case ItemType.Accessory:
                case ItemType.Artifact:
                case ItemType.Consumable:
                    isInteractivePickup = true;
                    break;
                case ItemType.Currency:
                    isInteractivePickup = false;
                    combatItem = false;
                    break;
            }
            
            
#endif
        
        }
    }
}