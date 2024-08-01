using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeReverie
{
    public class InventorySlotUIContainer : SerializedMonoBehaviour, IDropHandler
    {
        public int slotIndex;
        public GameObject highlight;
        public InventorySlotUI inventorySlot;
        
        
        public void OnDrop(PointerEventData eventData)
        {
            GameObject droppedItem = eventData.pointerDrag;


            if (droppedItem.TryGetComponent(out DraggableItem draggableItem) && droppedItem.TryGetComponent(out InventorySlotUI inventoryItemUI))
            {
                draggableItem.parentAfterDrag = transform;

                // if (inventoryItem == null)
                // {
                //     inventoryItem = inventoryItemUI;
                // }
                
            }
        }
        
    }
}