using System;
using Sirenix.OdinInspector;

namespace CodeReverie
{
    public class InventoryEvents
    {
        // public event Action<EquippableItem, EquippableItem> onEquip;
        //
        // public void OnEquip(EquippableItem equippableItemToAdd, EquippableItem equippableItemToRemove)
        // {
        //     onEquip?.Invoke(equippableItemToAdd, equippableItemToRemove);
        // }
        //
        //
        // public event Action<EquippableItem> onUnequip;
        //
        // public void OnUnequip(EquippableItem equippableItem)
        // {
        //     onUnequip?.Invoke(equippableItem);
        // }

        public Action<Item> onItemPickup;

        public void OnItemPickup(Item item)
        {
            onItemPickup?.Invoke(item);
        }
        
        
        public Action<Item> onItemMenuSelect;

        public void OnItemMenuSelect(Item item)
        {
            onItemMenuSelect?.Invoke(item);
        }

    }
}