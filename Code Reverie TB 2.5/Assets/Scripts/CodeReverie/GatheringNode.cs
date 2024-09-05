using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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
        [SerializeField] private string _interactableMessage;
        //public Slider slider;


        private void Awake()
        {
            CanInteract = true;
            interactableType = InteractableType.Gather;
            // slider.maxValue = gatheringTime;
            // slider.gameObject.SetActive(false);
            removeOnInteractComplete = true;
            
            if (_interactableMessage.IsNullOrWhitespace())
            {
                _interactableMessage = $"Collect {itemInfo.itemName}";
            }
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
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.notificationCenter.NotificationTrigger($"{item.info.itemName} acquired");
        }
        
        public int Priority { get; }
        public InteractableType interactableType { get; set; }

        public string interactableMessage
        {
            get => _interactableMessage;
            set => _interactableMessage = value;
        }

        public bool removeOnInteractComplete { get; set; }

        public void Interact(Action onComplete)
        {
            CanInteract = false;
            AddItemToInventory();
            onComplete();
        }

        public void InteractOnPress(Action onComplete) { }

        public void InteractOnHold(Action onComplete)
        {
            if (CanInteract)
            {
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.gameObject.SetActive(true);
                //slider.gameObject.SetActive(true);
                gatheringTimer += Time.deltaTime;
                //slider.value = gatheringTimer;
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.maxValue = gatheringTime;
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.value = gatheringTimer;

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


            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.value = 0;
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.gameObject.SetActive(false);
        }
    }
}