using System;
using UnityEngine;

namespace CodeReverie
{
    public class SkillSlotPauseMenuNavigationButton : PauseMenuNavigationButton
    {
        public SkillType skillType;
        public int skillSlotIndex;
        public Skill skill;

        

        private void OnEnable()
        {
            
        }
        
        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onPauseMenuCharacterSwap -= Init;
        }

        public void SetListeners()
        {
            EventManager.Instance.generalEvents.onPauseMenuCharacterSwap += Init;
        }


        public void Init(Character character)
        {
            if (skillType == SkillType.Action)
            {
                if (character.characterSkills.equippedActionSkills[skillSlotIndex] == null)
                {
                    skill = null;
                    nameText.text = "None";
                }
                else
                {
                    skill = character.characterSkills.equippedActionSkills[skillSlotIndex];
                    nameText.text = skill.info.skillName;
                }
            }
            
            if (skillType == SkillType.Passive)
            {
                if (character.characterSkills.equippedPassivesSkills[skillSlotIndex] == null)
                {
                    skill = null;
                    nameText.text = "None";
                }
                else
                {
                    skill = character.characterSkills.equippedPassivesSkills[skillSlotIndex];
                    nameText.text = skill.info.skillName;
                }
            }
        }
        
    }
}