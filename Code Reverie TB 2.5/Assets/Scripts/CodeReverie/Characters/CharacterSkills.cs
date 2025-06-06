﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeReverie
{
    public class CharacterSkills
    {


        public Dictionary<GearSlotType, List<Skill>> equippedSkills = new Dictionary<GearSlotType, List<Skill>>();
        public Dictionary<GearSlotType, List<SkillSlot>> skillSlots = new Dictionary<GearSlotType, List<SkillSlot>>();
        public Dictionary<int, Skill> equippedActionSkills = new Dictionary<int, Skill>();
        //public Dictionary<int, SkillSlot> equippedActionSkills = new Dictionary<int, SkillSlot>();
        public Dictionary<int, Skill> equippedPassivesSkills = new Dictionary<int, Skill>();
        
        public List<Skill> learnedSkills = new List<Skill>();
        public List<Skill> learnedArchetypeSkills = new List<Skill>();
        
        
        
        
        
        public CharacterSkills()
        {
            // if (equippedSkills == null)
            // {
            //     equippedSkills = new Dictionary<int, SkillSlot>();
            // }

            if (equippedActionSkills == null)
            {
                equippedActionSkills = new Dictionary<int, Skill>();
            }
            
            if (equippedPassivesSkills == null)
            {
                equippedPassivesSkills = new Dictionary<int, Skill>();
            }

            for (int i = 0; i < 6; i++)
            {
                // if (!equippedSkills.ContainsKey(i))
                // {
                //     equippedSkills.Add(i, new SkillSlot());
                // }
                
                if (!equippedActionSkills.ContainsKey(i))
                {
                    equippedActionSkills.Add(i, null);
                }
                
                if (!equippedPassivesSkills.ContainsKey(i))
                {
                    equippedPassivesSkills.Add(i, null);
                }
            }
            
            learnedSkills = new List<Skill>();
            
            skillSlots.Add(GearSlotType.Gluttony, new List<SkillSlot>());
            skillSlots.Add(GearSlotType.Wrath, new List<SkillSlot>());
            skillSlots.Add(GearSlotType.Pride, new List<SkillSlot>());
            skillSlots.Add(GearSlotType.Envy, new List<SkillSlot>());
            skillSlots.Add(GearSlotType.Greed, new List<SkillSlot>());
            skillSlots.Add(GearSlotType.Lust, new List<SkillSlot>());
            skillSlots.Add(GearSlotType.Sloth, new List<SkillSlot>());
        }
        
        public void EquipActionSkill(SkillDataContainer skillDataContainer, int index)
        {
            
            if (equippedActionSkills == null)
            {
                equippedActionSkills = new Dictionary<int, Skill>();
            }

            Skill skill = SkillsManager.Instance.CreateSkill(skillDataContainer);

            if (skill != null)
            {
                equippedActionSkills[index] = skill;
            }
        }
        
        public void EquipPassiveSkill(SkillDataContainer skillDataContainer, int index)
        {
            if (equippedPassivesSkills == null)
            {
                equippedPassivesSkills = new Dictionary<int, Skill>();
            }

            Skill skill = SkillsManager.Instance.CreateSkill(skillDataContainer);

            if (skill != null)
            {
                equippedPassivesSkills[index] = skill;
            }
        }
        
        public void LearnSkill(Skill skill)
        {
            //Skill skill = SkillsManager.Instance.CreateSkill(skillDataContainer);
            if (skill != null)
            {
                learnedSkills.Add(skill);
            }
        }

        public void LearnSkill(SkillDataContainer skillDataContainer)
        {
            Skill skill = SkillsManager.Instance.CreateSkill(skillDataContainer);
            if (skill != null)
            {
                learnedSkills.Add(skill);
            }
        }

        public void SetLearnedSkills()
        {
            
            learnedSkills = new List<Skill>();

            foreach (List<SkillSlot> skillSlotsList in skillSlots.Values)
            {
                foreach (SkillSlot skillSlot in skillSlotsList)
                {
                    if (skillSlot.skill != null)
                    {
                        learnedSkills.Add(skillSlot.skill);
                    }
                    
                }
                
            }

            learnedSkills = learnedSkills.GroupBy(x => x.info.id).Select(y => y.First()).ToList();

        }
        
    }
}