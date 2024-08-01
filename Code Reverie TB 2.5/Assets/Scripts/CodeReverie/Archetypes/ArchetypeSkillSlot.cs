using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ArchetypeSkillSlot : SerializedMonoBehaviour, IDropHandler
    {
        public SkillSlot skillSlot;
        public int slotNum;
        public Image skillImage;
        public bool alchemicBurstSlot;
        public SkillType allowedSkillType;

        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onCharacterMenuSwap += Init;
            EventManager.Instance.playerEvents.onSkillSlotUpdate += Init;
        }
        
        private void OnDisable()
        {
            EventManager.Instance.playerEvents.onCharacterMenuSwap -= Init;
            EventManager.Instance.playerEvents.onSkillSlotUpdate -= Init;
        }
        
        

        public void Init()
        {
            
            // skillSlot = CanvasManager.Instance.characterMenuManager.ActivePartySlot.characterSkillsManager.character.characterSkills.equippedSkills[allowedSkillType];
            //
            //
            // if (skillSlot != null && skillSlot.skill != null)
            // {
            //     skillImage.sprite = skillSlot.skill.GetSpriteIcon();
            // }
            // else
            // {
            //     skillImage.sprite = null;
            // }
            
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!alchemicBurstSlot)
            {
                
            }
            
            GameObject droppedItem = eventData.pointerDrag;

            if (droppedItem.TryGetComponent(out ArchetypeSkillNodeButton prevParent))
            {
                if (prevParent.archetypeSkillNode.skillNodeDataContainer.skillDataContainer.skillType == allowedSkillType)
                {
                    //CanvasManager.Instance.characterMenuManager.ActivePartySlot.character.EquipSkill(prevParent.archetypeSkillNode.skillNodeDataContainer.skillDataContainer, allowedSkillType);
                    
                    Debug.Log("yo yo oy");

                    EventManager.Instance.playerEvents.OnSkillSlotUpdate();
                }
                else
                {
                    Debug.Log("Incorrect Skill Type");
                }
            }

        }
    }
}