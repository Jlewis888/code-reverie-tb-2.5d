using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeReverie
{
    public class TrashInventoryItemUI : SerializedMonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject droppedItem = eventData.pointerDrag;


            if (droppedItem.TryGetComponent(out DraggableItem draggableItem) &&
                droppedItem.TryGetComponent(out InventoryItemUI inventoryItem))
            {
                
                InventorySlotUI prevInventorySlotUI = draggableItem.parentAfterDrag.GetComponentInParent<InventorySlotUI>();

                if (prevInventorySlotUI != null)
                {
                    PlayerManager.Instance.inventory.RemoveItems(prevInventorySlotUI.slotIndex);
                    prevInventorySlotUI.SetInventorySlot();
                }
                
                
                // if (draggableItem.parentAfterDrag.TryGetComponent(out InventorySlotUI prevInventorySlotUI))
                // {
                //     PlayerManager.Instance.inventory.RemoveItems(prevInventorySlotUI.slotIndex);
                //     prevInventorySlotUI.SetInventorySlot();
                //     
                // }
                
                if (draggableItem.parentAfterDrag.TryGetComponent(out GearSlotUI prevGearSlotUI))
                {
                }
                
                
                Destroy(inventoryItem.gameObject);

            }
        }
    }
}