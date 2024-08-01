using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie.Vendors
{
    public class VendorInventorySlotUI : SerializedMonoBehaviour
    {
        private Button button;
        public VendorItemSlot vendorItemSlot;


        private void Awake()
        {
            button = GetComponent<Button>();
            
            
            button.onClick.AddListener(PurchaseItem);
        }



        public void PurchaseItem()
        {
            if (vendorItemSlot.stockCount >= 1)
            {
                if (CurrencyManager.Instance.CheckIfEnoughCurrency(vendorItemSlot.currencyType, vendorItemSlot.price))
                {
                    // if (PlayerManager.Instance.inventory.InventoryFull())
                    // {
                    //     Debug.Log("Inventory Full");
                    // }
                    // else
                    // {
                    //     CurrencyManager.Instance.UpdateCurrency(vendorItemSlot.currencyType, -vendorItemSlot.price); 
                    //     PlayerManager.Instance.inventory.AddItem(vendorItemSlot.item);
                    //     vendorItemSlot.stockCount -= 1;
                    // }
                
                    CurrencyManager.Instance.UpdateCurrency(vendorItemSlot.currencyType, -vendorItemSlot.price); 
                    PlayerManager.Instance.inventory.AddItem(vendorItemSlot.item);
                    vendorItemSlot.stockCount -= 1;
                
                }
                else
                {
                    Debug.Log("Not enough Funds");
                }
            }
            else
            {
                Debug.Log("Item Out of Stock");
            }
            
            
           
        }
        
        
    }
}