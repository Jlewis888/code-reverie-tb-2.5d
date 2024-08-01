using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ArchetypeSkillNodeButtonBAK : SerializedMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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

            // if (archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Action)
            // {
            //     if (archetypeSkillNode.skillNodeDataContainer.alchemicBurstSkill)
            //     {
            //         AssignAlchemicBurst();
            //     }
            // }
            // else 
                
                
            if(archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Trigger || archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Passive)
            {
                AssignSkillPoint();
            }
            
        }


        public void Init()
        {
            if (archetypeSkillNode != null)
            {
                skillImage.sprite = archetypeSkillNode.skillNodeDataContainer.skillDataContainer.icon;
                skillImageDraggable.sprite = archetypeSkillNode.skillNodeDataContainer.skillDataContainer.icon;

                // if (archetypeSkillNode.skillNodeDataContainer.alchemicBurstSkill)
                // {
                //     skillImageDraggable.gameObject.SetActive(false);
                // }
                
            }
        }


        public bool CheckIfCanAssignPoints()
        {

            //Debug.Log(PlayerManager.Instance.currentParty[0].character.SkillPoints);
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

        public void AssignSkillPoint()
        {
            
            if (CheckIfCanAssignPoints())
            {
                archetypeSkillNode.assignedPoints += 1;
                //PlayerManager.Instance.currentParty[0].availableSkillPoints -= 1;
                PlayerManager.Instance.currentParty[0].RemoveSkillPoint(1);

                if (archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Passive)
                {
                    
                    //CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.characterSkills.equippedPassivesSkills.Add(SkillsManager.Instance.CreateSkill(archetypeSkillNode.skillNodeDataContainer.skillDataContainer));
                    CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.characterSkills.EquipPassiveSkill(archetypeSkillNode.skillNodeDataContainer.skillDataContainer, 0);
                }
                
            }
        }
        
        public void UnassignSkillPoint()
        {
            if (archetypeSkillNode.assignedPoints > 0)
            {
                archetypeSkillNode.assignedPoints -= 1;
                PlayerManager.Instance.currentParty[0].AddSkillPoint(1);
                
                
                // if (archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == SkillType.Passive)
                // {
                //     Skill skill = CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.characterSkills.equippedPassivesSkills.First(
                //         skill => skill.info.id == archetypeSkillNode.skillNodeDataContainer.skillDataContainer.id);
                //     
                //     
                //     CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.characterSkills.equippedPassivesSkills.Remove(skill);
                // }
                
            }
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
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (CanDrag())
            {
                
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    skillImageDraggable.raycastTarget = false;
                }  
                else
                {
                    eventData.pointerDrag = null;
                }
                
               
            }
            else
            {
                eventData.pointerDrag = null;
            }
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            
            if (CanDrag())
            {
                skillImageDraggable.transform.position = Input.mousePosition;
            }
            
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (CanDrag())
            {
                skillImageDraggable.raycastTarget = true;
                skillImageDraggable.GetComponent<RectTransform>().localPosition = Vector3.zero; 
            }
            
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                UnassignSkillPoint();
            }
        }
    }
}