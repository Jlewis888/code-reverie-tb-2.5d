using System;
using System.Collections.Generic;
using CodeReverie.Vendors;
using UnityEngine;

namespace CodeReverie
{
    public class VendorMenuManager : MenuManager
    {

        public VendorInventory vendorInventory;
        public VendorInventorySlotUI vendorInventorySlotUIPF;
        public GameObject vendorInventorySlotUIHolder;
        public List<VendorItemSlot> inventory = new List<VendorItemSlot>();
        
        private void OnEnable()
        {
           
            EventManager.Instance.playerEvents.OnPlayerLock(true);

            if (vendorInventory != null)
            {
                if (vendorInventory.inventoryData != null)
                {

                    inventory = new List<VendorItemSlot>();
                    
                    foreach (VendorItemData vendorItemData in vendorInventory.inventoryData)
                    {
                        VendorInventorySlotUI vendorInventorySlotUI = Instantiate(vendorInventorySlotUIPF,
                            vendorInventorySlotUIHolder.transform);

                        VendorItemSlot vendorItemSlot = new VendorItemSlot();
                        vendorItemSlot.item = new Item(vendorItemData.itemInfo);
                        vendorItemSlot.item.info = vendorItemData.itemInfo;
                        vendorItemSlot.price = vendorItemData.price;
                        vendorItemSlot.stockCount = vendorItemData.stockCount;
                        vendorItemSlot.currencyType = vendorItemData.currencyType;

                        vendorInventorySlotUI.vendorItemSlot = vendorItemSlot;

                    }
                }
            }
        }
        
        
        private void OnDisable()
        {
           
            foreach (Transform child in vendorInventorySlotUIHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
        }
    }
}