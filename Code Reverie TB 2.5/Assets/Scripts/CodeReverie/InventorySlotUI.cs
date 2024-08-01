using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeReverie
{
    public class InventorySlotUI : SerializedMonoBehaviour, IDropHandler
    {
        public int slotIndex;
        public GameObject highlight;
        public GameObject inventoryItemUIHolder;
        public TMP_Text itemAmountText;
        public GameObject itemAmountHolder;
        public InventoryItemUI inventoryItemUI;


        private void OnEnable()
        {
            SetInventorySlot();
        }

        private void OnDisable()
        {
            Clear();
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject droppedItem = eventData.pointerDrag;


            if (droppedItem.TryGetComponent(out DraggableItem draggableItem) && droppedItem.TryGetComponent(out InventoryItemUI inventoryItem))
            {
                InventorySlotUI prevInventorySlotUI = draggableItem.parentAfterDrag.GetComponentInParent<InventorySlotUI>();

                if (prevInventorySlotUI != null)
                {
                    draggableItem.parentAfterDrag = transform;
                    
                    //inventoryItemUI = inventoryItem;
                    
                    //PlayerManager.Instance.inventory.SwapPositions(prevInventorySlotUI.slotIndex, slotIndex);
                    SetInventorySlot();
                    
                    prevInventorySlotUI.SetInventorySlot();
                    Destroy(draggableItem.gameObject);
                }
                
                // if (draggableItem.parentAfterDrag.TryGetComponent(out InventorySlotUI prevInventorySlotUI))
                // {
                //     
                //     draggableItem.parentAfterDrag = transform;
                //     
                //     //inventoryItemUI = inventoryItem;
                //
                //     
                //     PlayerManager.Instance.inventory.SwapPositions(prevInventorySlotUI.slotIndex, slotIndex);
                //     
                //   
                //     SetInventorySlot();
                //     
                //     prevInventorySlotUI.SetInventorySlot();
                //     Destroy(draggableItem.gameObject);
                //
                // }
                
                if (draggableItem.parentAfterDrag.TryGetComponent(out GearSlotUI gearSlotUI))
                {
                    
                    draggableItem.parentAfterDrag = transform;
                    
                    inventoryItemUI = inventoryItem;
                
                    
                    //PlayerManager.Instance.inventory.AddItem(slotIndex, inventoryItemUI.item);
                    
                  
                    SetInventorySlot();
                   
                    
                    CanvasManager.Instance.inventory.ActivePartySlot.character.characterGear.UnequipItem(inventoryItemUI.item);
                    EventManager.Instance.playerEvents.OnGearUpdate(PlayerManager.Instance.currentParty[0]);
                    Destroy(draggableItem.gameObject);
                    //gearSlotUI.SetGearSlot();
                }
                
                
            }
        }

        public void SetInventorySlot()
        {
            Clear();
            
        }
        


        public void Clear()
        {
            foreach (Transform child in inventoryItemUIHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            inventoryItemUI = null;
            itemAmountHolder.SetActive(false);
            itemAmountText.text = "";

        }
        
        
        
    }
}