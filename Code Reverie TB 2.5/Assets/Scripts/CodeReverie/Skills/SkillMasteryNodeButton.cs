using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SkillMasteryNodeButton : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button button;

        public string skillNodeId;
        public TMP_Text skillNodeName;
        public TMP_Text skillPointsText;
        

        // private void Awake()
        // {
        //     button = GetComponent<Button>();
        //     //button.onClick.AddListener(UseButton);
        // }
        //
        //
        // public void UseButton()
        // {
        //     
        //     if (CheckIfCanAssignPoints())
        //     {
        //         
        //         if (SkillsMenuManager.instance.selectedSkillTree.GetSkillNodeById(skillNodeId).MaxAssignedPointsCheck())
        //         {
        //             SkillsMenuManager.instance.selectedSkillTree.AssignPoints(1);
        //             //GetComponentInParent<SkillTree>().totalSkillPointsInTree += 1;
        //         }  
        //     }
        //     else
        //     {
        //         Debug.Log("Can not assign points");
        //     }
        // }
        //
        // public bool CheckIfCanAssignPoints()
        // {
        //     if (!SkillsMenuManager.instance.selectedSkillTree.GetSkillNodeById(skillNodeId).skillMasteryNodeDetails
        //         .hasPrerequisites)
        //     {
        //         return true;
        //     }
        //
        //     if (SkillsMenuManager.instance.selectedSkillTree.GetSkillNodeById(skillNodeId).skillMasteryNodeDetails
        //             .skillPrerequisites != null)
        //     {
        //         return SkillsMenuManager.instance.selectedSkillTree.GetSkillNodeById(skillNodeId).PrerequisiteCheck();
        //     }
        //     
        //     return false;
        // }
        //
        public void OnPointerEnter(PointerEventData eventData)
        {
            // SkillsMenuManager.instance.selectedSkillTree.SetActiveSkillNodeById(skillNodeId);
            // SkillsMenuManager.instance.selectedSkillTree.selectedNodeButton = this;
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            // SkillsMenuManager.instance.selectedSkillTree.SetActiveSkillNodeNull();
            // SkillsMenuManager.instance.selectedSkillTree.selectedNodeButton = null;
        }
        
    }
}