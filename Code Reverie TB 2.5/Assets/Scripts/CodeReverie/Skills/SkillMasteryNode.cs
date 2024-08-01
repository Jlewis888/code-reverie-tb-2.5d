using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SkillMasteryNode
    {
        public SkillMasteryNodeDetails skillMasteryNodeDetails;
        public int skillPointsAssigned;
        public int skillPointsAssignedFromOtherSources;

        // public SkillMasteryNode(SkillMasteryNodeDetails skillMasteryNodeDetails)
        // {
        //     this.skillMasteryNodeDetails = skillMasteryNodeDetails;
        //     skillPointsAssigned = 0;
        // }
        //
        // public void AssignPoints(int points)
        // {
        //     
        //     skillPointsAssigned += points;
        //
        //     if (skillPointsAssigned < 0)
        //     {
        //         skillPointsAssigned = 0;
        //     }
        //
        //     // if (skillPointsAssigned > 0 && skillPointsAssigned <= skillMasteryNodeDetails.skillModifierDetailsList.Count)
        //     // {
        //     //     GameEventsManager.Instance.skillEvents.SkillModifierActivation(skillMasteryNodeDetails.skillModifierDetailsList[skillPointsAssigned-1].id);
        //     // }
        //     
        //     UpdateSkillModifiers();
        // }
        //
        // public bool MaxAssignedPointsCheck()
        // {
        //     if (skillPointsAssigned <= skillMasteryNodeDetails.maxAssignedPoints)
        //     {
        //         return true;
        //     }
        //
        //     return false;
        // }
        //
        // public bool PrerequisiteCheck()
        // {
        //     foreach (KeyValuePair<SkillMasteryNodeDetails, int> skillPrerequisite in skillMasteryNodeDetails.skillPrerequisites)
        //     {
        //         if (SkillsMenuManager.instance.selectedSkillTree.GetSkillNodeById(skillPrerequisite.Key.id).skillPointsAssigned >= skillPrerequisite.Value)
        //         {
        //             return true;
        //         }
        //     }
        //
        //     return false;
        // }
        //
        // public bool CanAssignPointsCheck(int points)
        // {
        //     
        //     //if(skillPointsAssigned )
        //     return false;
        // }
        //
        // public void UpdateSkillModifiers()
        // {
        //     int count = skillPointsAssigned + skillPointsAssignedFromOtherSources;
        //
        //     foreach (SkillModifierDataContainer skillModifierDetails in skillMasteryNodeDetails.skillModifierDetailsList)
        //     {
        //         if (count > 0)
        //         {
        //             GameEventsManager.Instance.skillEvents.SkillModifierActivation(skillModifierDetails.skillDetailsId, skillModifierDetails.id);
        //         }
        //         else
        //         {
        //             GameEventsManager.Instance.skillEvents.SkillModifierDeactivation(skillModifierDetails.skillDetailsId, skillModifierDetails.id);
        //         }
        //         count -= 1;
        //     }
        // }
        
        
    }
}