using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class ArchetypeSkills
    {
        
        public Dictionary<SkillType, SkillSlot> equippedSkills = new Dictionary<SkillType, SkillSlot>();
        public List<Skill> equippedPassivesSkills = new List<Skill>();
        
        
        
        
        
        public ArchetypeSkills()
        {
            
            equippedSkills = new Dictionary<SkillType, SkillSlot>();
            
            equippedSkills.Add(SkillType.Basic, null);
            equippedSkills.Add(SkillType.Dodge, null);
            equippedSkills.Add(SkillType.Action, null);
            equippedSkills.Add(SkillType.AlchemicBurst, null);
            
            
            // equippedSkills.Add(SkillType.Basic, new SkillSlot());
            // equippedSkills.Add(SkillType.Dash, new SkillSlot());
            // equippedSkills.Add(SkillType.Action, new SkillSlot());
            // equippedSkills.Add(SkillType.AlchemicBurst, new SkillSlot());
            
        }
        
        
        public void UseEquippedSkill(SkillType skillType)
        {
            if (equippedSkills.ContainsKey(skillType))
            {
                if (equippedSkills[skillType] != null && equippedSkills[skillType].skill != null)
                {
                    if (equippedSkills[skillType].skillSlotCooldownTimer <= 0)
                    {
                        equippedSkills[skillType].skill.UseSkill();
                        equippedSkills[skillType].SetCooldownTimer();
                    }
                }
            }
        }

        public void EquipPassiveSkill(SkillDataContainer skillDataContainer)
        {
            if (equippedPassivesSkills == null)
            {
                equippedPassivesSkills = new List<Skill>();
            }

            Skill skill = SkillsManager.Instance.CreateSkill(skillDataContainer);

            if (skill != null)
            {
                equippedPassivesSkills.Add(skill);
            }
            

            
        }
        
        
        public void RunSkillCooldownTimer(SkillType skillType)
        {

            if (equippedSkills.ContainsKey(skillType))
            {
                if (equippedSkills[skillType] != null && equippedSkills[skillType].skill != null)
                {
                    if (equippedSkills[skillType].skillSlotCooldownTimer > 0)
                    {
                        equippedSkills[skillType].skillSlotCooldownTimer -= Time.deltaTime; 
                    }
                }
            }
        }

        public void SubscribeEquippedSkills()
        {
            foreach (var skillSlot in equippedSkills.Values)
            {
                if (skillSlot != null)
                {
                    // if (skillSlot.skill != null)
                    // {
                    //     skillSlot.skill.SubscribeSkillListeners();
                    // }
                    
                    skillSlot.SubscribeListeners();
                    
                }
                
                
               
            }
        }
        
        public void UnsubscribeEquippedSkills()
        {
            foreach (var skillSlot in equippedSkills.Values)
            {
                if (skillSlot != null)
                {
                    skillSlot.UnsubscribeListeners();
                    
                    // if (skillSlot.skill != null)
                    // {
                    //     skillSlot.skill.UnsubscribeSkillListeners();
                    // }
                }
            }
        }
        
    }
}