using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "VendorInventory", menuName = "Scriptable Objects/Vendor/VendorInventory", order = 1)]
    public class VendorInventory : SerializedScriptableObject
    {
        public List<VendorItemData> inventoryData = new List<VendorItemData>();
        //public List<VendorItemSlot> inventory = new List<VendorItemSlot>();
        
        
        
        private void OnValidate()
        {
#if UNITY_EDITOR

            // foreach (KeyValuePair<ItemInfo, VendorItemSlot> vendorItemSlot in inventory)
            // {
            //     if (vendorItemSlot.Value == null)
            //     {
            //         vendorItemSlot.Key = new VendorItemSlot();
            //
            //         //vendorItemSlot.Value = new VendorItemSlot();
            //     }
            // }
#endif
        }
        
        
    }
}