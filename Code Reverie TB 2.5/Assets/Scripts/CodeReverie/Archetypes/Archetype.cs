using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace CodeReverie
{
    public class Archetype
    {
        public ArchetypeDataContainer info;
        public ArchetypeSkills skills;
       
        public Archetype(ArchetypeDataContainer info)
        {
            this.info = info;

            skills = new ArchetypeSkills();
            

            EquipBaseSkills();
        }


        public void EquipBaseSkill(SkillType skillType)
        {
            if (info.baseSkills.ContainsKey(skillType) && info.baseSkills[skillType] != null)
            {
                skills.equippedSkills[skillType] = new SkillSlot();
                skills.equippedSkills[skillType].skill = SkillsManager.Instance.CreateSkill(info.baseSkills[skillType]);
            }
        }


        public void EquipBaseSkills()
        {
            EquipBaseSkill(SkillType.Basic);
            EquipBaseSkill(SkillType.Dodge);
            EquipBaseSkill(SkillType.Action);
            EquipBaseSkill(SkillType.AlchemicBurst);
        }
        
       

        public void SetSkillData()
        {
            foreach (var skillSlot in skills.equippedSkills.Values)
            {
                if (skillSlot != null)
                {
                    if (skillSlot.skill != null)
                    {
                        skillSlot.skill.info = SkillsManager.Instance.GetSkillById(skillSlot.skill.skillID);
                    }
                }
            }
            
            foreach (var skillSlot in skills.equippedPassivesSkills)
            {
                if (skillSlot != null)
                {
                    skillSlot.info = SkillsManager.Instance.GetSkillById(skillSlot.skillID);
                }
            }
        }

        // public bool CanAssignLinkSkillNodesPoint(ArchetypeSkillNode archetypeSkillNode)
        // {
        //     int totalAssignedPoints = 0;
        //     bool canAssign = false;
        //     bool continueLoop = true;
        //     
        //     
        //     info.skillNodeLinks.ForEach(skillNodeLink =>
        //     {
        //         skillNodeLink.linkedArchetypeSkillNodes.ForEach(linkedSkillNodeContainer =>
        //         {
        //             if (archetypeSkillNode.skillNodeDataContainer.id == linkedSkillNodeContainer.skillNodeDataContainer.id)
        //             {
        //                 totalAssignedPoints = skillNodeLink.TotalAssignedPoints();
        //                 return;
        //             }
        //         });
        //
        //         if (!continueLoop)
        //         {
        //             canAssign = totalAssignedPoints < skillNodeLink.maxAssignedPoints;
        //             return;
        //         }
        //         
        //     });
        //     
        //     
        //     
        //
        //     return canAssign;
        // }

        // public bool SkillNodeLinkCheck(ArchetypeSkillNode archetypeSkillNode)
        // {
        //     bool linked = info.skillNodeLinks.Exists(x => x.linkedArchetypeSkillNodes.Contains(archetypeSkillNode));
        //     
        //
        //     return linked;
        // }
        
        
    }
}