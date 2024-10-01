using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CommandWheelOptionExtensionItem : CommandWheelOptionExtension
    {
        public Item item;

        public void Init()
        {
            commandWheelOption.toolTipData = new CombatToolTipData();
            commandWheelOption.toolTipData.actionTitle = item.info.itemName;
            commandWheelOption.toolTipData.actionDescription = item.info.itemDescription;
            commandWheelOption.toolTipData.actionCost = 1;
            commandWheelOption.toolTipData.actionCastTime = SkillCastTime.Instant;
            
            
            commandWheelOption.actionIcon.sprite = item.info.uiIcon;
            commandWheelOption.Disabled = false;
            
        }
        
    }
}