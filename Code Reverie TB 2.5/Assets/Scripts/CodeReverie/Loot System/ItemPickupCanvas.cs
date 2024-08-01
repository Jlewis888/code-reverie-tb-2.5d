using System;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace CodeReverie
{
    public class ItemPickupCanvas : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Pickup pickup;
        private TooltipData tooltipData;

        private void Awake()
        {
            pickup = GetComponentInParent<Pickup>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipData tooltipData = new TooltipData();

            tooltipData.name = pickup.item.info.itemName;
            tooltipData.toolTipType = "item";
            //tooltipData.description = pickup.item.info.itemDescription;
            tooltipData.description = BuildToolTipDescription();
            tooltipData.headerColor = ItemManager.Instance.itemRarityMap[pickup.item.info.itemRarity].headerColor;
            EventManager.Instance.generalEvents.OpenToolTip(tooltipData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EventManager.Instance.generalEvents.CloseToolTip();
        }

        public string BuildToolTipDescription()
        {
            string description = "";

            description += "Base Stats \n";
            foreach (Stat stat in pickup.item.info.baseItemStats)
            {
                description += $"{stat.statAttribute} +{stat.statValue} \n";
            }


            return description;
        }
        
    }
}