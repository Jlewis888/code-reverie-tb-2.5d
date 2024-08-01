using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class ArchetypeNodeConnector : SerializedMonoBehaviour
    {
        // public ArchetypeSkillNodeButton skillNodeButton1;
        // public ArchetypeSkillNodeButton skillNodeButton2;

        public GameObject startConnector, endConnector;
        
        public ArchetypeSkillNode skillNode1;
        public ArchetypeSkillNode skillNode2;
        
        public UILineRenderer uiLineRenderer;
        public Color baseColor;
        public Color assignedColor;
        public string baseColorHex = "#8E9294";
        public string assignedColorHex = "#FFCB5A";
        public bool rootNodeConnector;
        
        private void Awake()
        {
            uiLineRenderer = GetComponent<UILineRenderer>();
        }


        private void Update()
        {
            // if (skillNodeButton1 != null && skillNodeButton2 != null)
            // {
            //     if (skillNodeButton1.archetypeSkillNode != null && skillNodeButton2.archetypeSkillNode != null)
            //     {
            //         if (skillNodeButton1.archetypeSkillNode.SkillNodeState == ArchetypeSkillNodeState.Assigned &&
            //             skillNodeButton2.archetypeSkillNode.SkillNodeState == ArchetypeSkillNodeState.Assigned)
            //         {
            //             uiLineRenderer.color = ColorUtility.TryParseHtmlString(assignedColorHex, out Color color) ? color: assignedColor;
            //         }
            //         else
            //         {
            //             uiLineRenderer.color = ColorUtility.TryParseHtmlString(baseColorHex, out Color color) ? color: baseColor;
            //         }
            //     }
            //     else
            //     {
            //         uiLineRenderer.color = ColorUtility.TryParseHtmlString(baseColorHex, out Color color) ? color: baseColor;
            //     }
            // }
            // else
            // {
            //     uiLineRenderer.color = ColorUtility.TryParseHtmlString(baseColorHex, out Color color) ? color: baseColor;
            // }


            if (skillNode1 != null && skillNode2 != null)
            {
                if (skillNode1.SkillNodeState == ArchetypeSkillNodeState.Assigned && skillNode2.SkillNodeState == ArchetypeSkillNodeState.Assigned)
                {
                    uiLineRenderer.color = ColorUtility.TryParseHtmlString(assignedColorHex, out Color color) ? color: assignedColor;
                }
                else
                {
                    uiLineRenderer.color = ColorUtility.TryParseHtmlString(baseColorHex, out Color color) ? color: baseColor;
                }
                
                
            }
            else
            {
                uiLineRenderer.color = ColorUtility.TryParseHtmlString(baseColorHex, out Color color) ? color: baseColor;
            }
        }

        public void SetConnectors()
        {
            // foreach (Transform child in transform)
            // {
            //     Destroy(child);
            // }
        }
        
    }
}