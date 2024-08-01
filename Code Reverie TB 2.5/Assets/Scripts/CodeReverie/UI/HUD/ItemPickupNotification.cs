using System;
using Sirenix.OdinInspector;
using TMPro;

namespace CodeReverie
{
    public class ItemPickupNotification : SerializedMonoBehaviour
    {
        public string itemId;
        public TMP_Text itemCountText;
        public int itemCount;
        public TMP_Text itemName;


        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onItemPickup += ItemPickupTrigger;
        }

        private void OnDisable()
        {
            EventManager.Instance.playerEvents.onItemPickup -= ItemPickupTrigger;
        }

        private void Update()
        {
            if (itemCount == 0)
            {
                itemCount = 1;
                
            }

            itemCountText.text = $"+{itemCount}";

        }
        
        
        private void ItemPickupTrigger(string id, int itemPickupAmount)
        {

            if (id == itemId)
            {
                itemCount += itemPickupAmount; 
            }
           
        }


    }
}