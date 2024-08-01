using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ArchetypeSkillNodeButton : SerializedMonoBehaviour, IPointerClickHandler
    {
        private Button button;
        public ArchetypeSkillNodeDataContainer skillNodeDataContainer;
        public ArchetypeSkillNode archetypeSkillNode;
        public Image skillImage;
        public Image skillImageDraggable;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(UseButton);
        }

        public void UseButton()
        {

            // if(archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Trigger || archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Passive)
            // {
            //     
            // }
            AssignSkill();
        }


        public void Init()
        {
            if (archetypeSkillNode != null)
            {

                if (archetypeSkillNode.skillNodeDataContainer.skillDataContainer != null)
                {
                    skillImage.sprite = archetypeSkillNode.skillNodeDataContainer.skillDataContainer.icon;
                    skillImage.color = Color.white;
                    skillImage.transform.localScale = Vector3.one;
                    //skillImageDraggable.sprite = archetypeSkillNode.skillNodeDataContainer.skillDataContainer.icon;

                }
                
                
                // if (archetypeSkillNode.skillNodeDataContainer.alchemicBurstSkill)
                // {
                //     skillImageDraggable.gameObject.SetActive(false);
                // }
                
                
            }
        }


        public bool CheckIfCanAssignPoints()
        {

            //Debug.Log(PlayerManager.Instance.currentParty[0].SkillPoints);
            if (PlayerManager.Instance.currentParty[0].SkillPoints > 0)
            {
                if (archetypeSkillNode.skillNodeDataContainer.minTreePoints <=
                    CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.AssignedPoints)
                {
                    
                    // if(archetypeSkillNode.linkedNode)
                    //
                    // if (archetypeSkillNode.assignedPoints < archetypeSkillNode.skillNodeDataContainer.maxAssignedPoints)
                    // {
                    //     return true;
                    // }

                    return archetypeSkillNode.CanAssignPoints();

                }
            } 
            
            return false;
        }

        public void AssignAlchemicBurst()
        {
            CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.EquipAlchemicBurst(archetypeSkillNode.skillNodeDataContainer.skillDataContainer);
            EventManager.Instance.playerEvents.OnSkillSlotUpdate();
        }

        public void AssignSkill()
        {
            
            
            if (archetypeSkillNode.SkillNodeState == ArchetypeSkillNodeState.Assigned)
            {
                Debug.Log("Akready Assigned");
                UnassignSkill();
                return;
            } 
            
            if (archetypeSkillNode.skillNodeDataContainer.archetypeSkillNodeType == ArchetypeSkillNodeType.Root)
            {
                Debug.Log("Assigning Root Skill");
                archetypeSkillNode.SkillNodeState = ArchetypeSkillNodeState.Assigned;
                CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype
                    .skillNodesMap[skillNodeDataContainer.id].SkillNodeState = ArchetypeSkillNodeState.Assigned;
            }
            else
            {
                foreach (ArchetypeSkillNodeDataContainer archetypeSkillNodeDataContainer in archetypeSkillNode.skillNodeDataContainer.linkedSkillNodes)
                {
                    
                    if (CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype
                        .skillNodesMap[archetypeSkillNodeDataContainer.id].SkillNodeState == ArchetypeSkillNodeState.Assigned)
                    {
                        Debug.Log("Assigning Skill");
                        archetypeSkillNode.SkillNodeState = ArchetypeSkillNodeState.Assigned;
                        CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype
                            .skillNodesMap[skillNodeDataContainer.id].SkillNodeState = ArchetypeSkillNodeState.Assigned;
                        break;
                    }
                }
            }

            //Debug.Log(archetypeSkillNode.IsConnectedToAnAssignRootNode(archetypeSkillNode));
            
            if(archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Passive)
            {
                CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.characterSkills.EquipPassiveSkill(archetypeSkillNode.skillNodeDataContainer.skillDataContainer, 0);
            }
            
        }
        
        public void UnassignSkill()
        {


            if (archetypeSkillNode.AreAllAssignedNeighborsConnectedToRoot())
            {
                archetypeSkillNode.SkillNodeState = ArchetypeSkillNodeState.Unassigned;
                CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype
                    .skillNodesMap[skillNodeDataContainer.id].SkillNodeState = ArchetypeSkillNodeState.Unassigned;
                Debug.Log("Can Unassign Skill");
            }
            else
            {
                Debug.Log("Can not Unassign Skill");
            }
            
            
            // if (archetypeSkillNode.assignedPoints > 0)
            // {
            //     archetypeSkillNode.assignedPoints -= 1;
            //     PlayerManager.Instance.currentParty[0].character.AddSkillPoint(1);
            //     
            //     
            //     if (archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Passive)
            //     {
            //         Skill skill = CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.characterSkills.equippedPassivesSkills.First(
            //             skill => skill.info.id == archetypeSkillNode.skillNodeDataContainer.skillDataContainer.id);
            //         
            //         
            //         CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.characterSkills.equippedPassivesSkills.Remove(skill);
            //     }
            //     
            // }
        }
        


        public bool CanDrag()
        {

            List<SkillType> draggableSkillTypes = new List<SkillType>();
            
            draggableSkillTypes.Add(SkillType.Basic);
            draggableSkillTypes.Add(SkillType.Dodge);
            draggableSkillTypes.Add(SkillType.AlchemicBurst);
            draggableSkillTypes.Add(SkillType.Action);


            if (draggableSkillTypes.Contains(archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType))
            {
                if (archetypeSkillNode.skillNodeDataContainer.minTreePoints <=
                    CanvasManager.Instance.characterMenuManager.ActiveArchetypeTree.archetype.AssignedPoints)
                {
                    return true;
                }
            }

            // if (archetypeSkillNode.assignedPoints == archetypeSkillNode.skillNodeDataContainer.maxAssignedPoints &&
            //     archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Action)
            // {
            //     return true;
            // }

            return false;
        }
        

        // public void OnBeginDrag(PointerEventData eventData)
        // {
        //     if (CanDrag())
        //     {
        //         
        //         if (eventData.button == PointerEventData.InputButton.Left)
        //         {
        //             skillImageDraggable.raycastTarget = false;
        //         }  
        //         else
        //         {
        //             eventData.pointerDrag = null;
        //         }
        //         
        //        
        //     }
        //     else
        //     {
        //         eventData.pointerDrag = null;
        //     }
        //     
        // }
        //
        // public void OnDrag(PointerEventData eventData)
        // {
        //     
        //     if (CanDrag())
        //     {
        //         skillImageDraggable.transform.position = Input.mousePosition;
        //     }
        //     
        //     
        // }
        //
        // public void OnEndDrag(PointerEventData eventData)
        // {
        //     if (CanDrag())
        //     {
        //         skillImageDraggable.raycastTarget = true;
        //         skillImageDraggable.GetComponent<RectTransform>().localPosition = Vector3.zero; 
        //     }
        //     
        //     
        // }
        //
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                // UnassignSkill();
            }
        }


    }
}