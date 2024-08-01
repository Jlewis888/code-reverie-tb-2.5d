using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeReverie
{
    public class ActiveGearSlot : SerializedMonoBehaviour, IDropHandler, IPointerEnterHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject droppedItem = eventData.pointerDrag;


            if (droppedItem.TryGetComponent(out DraggableItem draggableItem) && droppedItem.TryGetComponent(out InventoryItemUI inventoryItemUI))
            {
               


                if (draggableItem.parentAfterDrag.TryGetComponent(out InventorySlotUI prevInventorySlotUI))
                {
                    draggableItem.parentAfterDrag = transform;
                }

            }
            
            // DraggableItem draggableItem = droppedItem.GetComponent<DraggableItem>();
            //
            // draggableItem.parentAfterDrag = transform;
            //draggableItem.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag)
            {
                Debug.Log("Entered the chat");
            }
            
        }
    }
}