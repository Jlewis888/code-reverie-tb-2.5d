using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class PlayerInventoryUIManager : SerializedMonoBehaviour
    {

        public static PlayerInventoryUIManager instance;
        public InventorySlotUI inventorySlotUIPF;
        public Dictionary<int, InventorySlotUI> inventorySlotUIList = new Dictionary<int, InventorySlotUI>();
        public InventoryItemUI inventoryItemUIPF;

        private void Awake()
        {

            instance = this;
            
            for (int i = 0; i < 30; i++)
            {
                InventorySlotUI inventoryItemSlot = Instantiate(inventorySlotUIPF, transform);
                inventoryItemSlot.slotIndex = i;
                inventoryItemSlot.gameObject.SetActive(true);
                inventorySlotUIList.Add(i, inventoryItemSlot);
            }
        }
    }
}