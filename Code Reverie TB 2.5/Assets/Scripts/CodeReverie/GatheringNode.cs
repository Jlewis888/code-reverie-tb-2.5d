using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class GatheringNode : SerializedMonoBehaviour, IInteractable
    {
        public ItemInfo itemInfo;
        public bool inRange;
        public float gatheringTime;
        public float gatheringTimer;
        public bool exhausted;
        //public Slider slider;


        private void Awake()
        {
            CanInteract = true;
            // slider.maxValue = gatheringTime;
            // slider.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButton("Interact") && inRange && !exhausted)
            {
                
            }
        }

        public void AddItemToInventory()
        {
            Item item = new Item(itemInfo);
            PlayerManager.Instance.inventory.AddItem(item);
            EventManager.Instance.playerEvents.OnItemPickup(itemInfo.id, 1);
            CanvasManager.Instance.hudManager.notificationCenter.NotificationTrigger($"{item.info.itemName} acquired");
        }
        
        public int Priority { get; }

        public void Interact()
        {
            
        }

        public void InteractOnPress(Action onComplete) { }

        public void InteractOnHold(Action onComplete)
        {
            if (CanInteract)
            {
                CanvasManager.Instance.hudManager.interactionSlider.gameObject.SetActive(true);
                //slider.gameObject.SetActive(true);
                gatheringTimer += Time.deltaTime;
                //slider.value = gatheringTimer;
                CanvasManager.Instance.hudManager.interactionSlider.maxValue = gatheringTime;
                CanvasManager.Instance.hudManager.interactionSlider.value = gatheringTimer;

                if (gatheringTimer >= gatheringTime)
                {
                    exhausted = true;
                    AddItemToInventory();
                    CanInteract = false;
                    ResetNode();
                    onComplete();
                }
            }
        }

        public void InteractOnPressUp(Action onComplete)
        {
            ResetNode();
        }
        

        public bool CanInteract { get; set; }


        public void ResetNode()
        {
            gatheringTimer = 0;
            //slider.value = 0;
            //slider.gameObject.SetActive(false);


            CanvasManager.Instance.hudManager.interactionSlider.value = 0;
            CanvasManager.Instance.hudManager.interactionSlider.gameObject.SetActive(false);
        }
    }
}