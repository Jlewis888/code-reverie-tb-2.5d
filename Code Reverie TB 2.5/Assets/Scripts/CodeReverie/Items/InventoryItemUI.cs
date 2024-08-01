using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class InventoryItemUI : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Item item;
        public Image itemImage;

        private void Awake()
        {
            itemImage = GetComponent<Image>();
        }
        
        public string BuildToolTipDescription()
        {
            string description = "";

            description += "Base Stats \n";
            foreach (Stat stat in item.info.baseItemStats)
            {
                description += $"{stat.statAttribute} +{stat.statValue} \n";
            }


            return description;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipData tooltipData = new TooltipData();
            tooltipData.name = item.info.itemName;
            tooltipData.toolTipType = "item";
            //tooltipData.description = pickup.item.info.itemDescription;
            tooltipData.description = BuildToolTipDescription();
            tooltipData.headerColor = ItemManager.Instance.itemRarityMap[item.info.itemRarity].headerColor;
            EventManager.Instance.generalEvents.OpenToolTip(tooltipData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EventManager.Instance.generalEvents.CloseToolTip();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //onLeftClick.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (item.info.itemType == ItemType.Consumable)
                {
                    item.UseItem();
                    
                    if (GetComponentInParent<GearSlotUI>())
                    {
                    } 
                    else if (GetComponentInParent<InventorySlotUI>())
                    {
                        if (item.amount <= 0)
                        {
                            PlayerManager.Instance.inventory.RemoveItems(GetComponentInParent<InventorySlotUI>().slotIndex);
                        }
                        
                        GetComponentInParent<InventorySlotUI>().SetInventorySlot();
                        
                    }
                    
                    
                    
                }
                //onRightClick.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                //onMiddleClick.Invoke();
            }
        }
    }
}