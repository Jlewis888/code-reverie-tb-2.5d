using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CommandWheelOptionExtensionSkill : CommandWheelOptionExtension
    {
        public Skill skill;

        public void Init()
        {
            commandWheelOption.toolTipData = new CombatToolTipData();
            commandWheelOption.toolTipData.actionTitle = skill.info.skillName;
            commandWheelOption.toolTipData.actionDescription = skill.info.skillDescription;
            commandWheelOption.toolTipData.actionCost = skill.info.actionPointsCost;
            commandWheelOption.toolTipData.actionCastTime = skill.info.skillCastTime;

            commandWheelOption.actionIcon.sprite = skill.info.icon;
            commandWheelOption.Disabled = false;
        }
        
    }
}