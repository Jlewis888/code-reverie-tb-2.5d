using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

namespace CodeReverie
{
    public class Notification : SerializedMonoBehaviour
    {
        public Image notificationImage;
        public TMP_Text notificationMessage;


        private void OnEnable()
        {
            //EventManager.Instance.playerEvents.onItemPickup += ItemPickupTrigger;
        }

        private void OnDisable()
        {
           // EventManager.Instance.playerEvents.onItemPickup -= ItemPickupTrigger;
        }

        private void Update()
        {
            // if (itemCount == 0)
            // {
            //     itemCount = 1;
            //     
            // }

            //itemCountText.text = $"+{itemCount}";

        }
        
        
        // private void ItemPickupTrigger(string id, int itemPickupAmount)
        // {
        //
        //     if (id == itemId)
        //     {
        //         itemCount += itemPickupAmount; 
        //     }
        //    
        // }


    }
}