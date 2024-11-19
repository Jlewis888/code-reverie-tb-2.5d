using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace CodeReverie
{
    public class Archetype
    {
        public ArchetypeDataContainer info;
        public List<Skill> learnedSkills = new List<Skill>();
        public Dictionary<int, int> skillsLevelMap = new Dictionary<int, int>();
        public List<ArchetypeSkillContainer> skillsLevel1 = new List<ArchetypeSkillContainer>();
        public List<ArchetypeSkillContainer> skillsLevel2 = new List<ArchetypeSkillContainer>();
        public List<ArchetypeSkillContainer> skillsLevel3 = new List<ArchetypeSkillContainer>();
        public List<ArchetypeSkillContainer> skillsLevel4 = new List<ArchetypeSkillContainer>();

        public int pointsUsed;
        
        
        public Archetype(ArchetypeDataContainer info)
        {
            this.info = info;

            //archetypeSkills = new ArchetypeSkills();
            learnedSkills = new List<Skill>();

            SetSkillsLevelMapToDefault();

            foreach (SkillDataContainer skillDataContainer in info.skillsLevel1)
            {
                ArchetypeSkillContainer archetypeSkillContainer = new ArchetypeSkillContainer();
                
                archetypeSkillContainer.skill = SkillsManager.Instance.CreateSkill(skillDataContainer);
                archetypeSkillContainer.skillLevel = 1;
                skillsLevel1.Add(archetypeSkillContainer);
            }
            
            foreach (SkillDataContainer skillDataContainer in info.skillsLevel2)
            {
                ArchetypeSkillContainer archetypeSkillContainer = new ArchetypeSkillContainer();
                
                archetypeSkillContainer.skill = SkillsManager.Instance.CreateSkill(skillDataContainer);
                archetypeSkillContainer.skillLevel = 2;
                skillsLevel2.Add(archetypeSkillContainer);
            }
            
            foreach (SkillDataContainer skillDataContainer in info.skillsLevel3)
            {
                ArchetypeSkillContainer archetypeSkillContainer = new ArchetypeSkillContainer();
                
                archetypeSkillContainer.skill = SkillsManager.Instance.CreateSkill(skillDataContainer);
                archetypeSkillContainer.skillLevel = 3;
                skillsLevel3.Add(archetypeSkillContainer);
            }
            
            foreach (SkillDataContainer skillDataContainer in info.skillsLevel4)
            {
                ArchetypeSkillContainer archetypeSkillContainer = new ArchetypeSkillContainer();
                
                archetypeSkillContainer.skill = SkillsManager.Instance.CreateSkill(skillDataContainer);
                archetypeSkillContainer.skillLevel = 4;
                skillsLevel4.Add(archetypeSkillContainer);
            }
            
        }

        public void SetSkillsLevelMapToDefault()
        {
            skillsLevelMap = new Dictionary<int, int>();
            
            skillsLevelMap.Add(1, 0);
            skillsLevelMap.Add(2, 0);
            skillsLevelMap.Add(3, 0);
            skillsLevelMap.Add(4, 0);
        }

        public void SetLearnedArchetypeSkill()
        {

            learnedSkills = new List<Skill>();
            skillsLevelMap = new Dictionary<int, int>();

            SetSkillsLevelMapToDefault();
            
            foreach (ArchetypeSkillContainer archetypeSkillContainer in skillsLevel1)
            {
                if (archetypeSkillContainer.hasLearned)
                {
                    learnedSkills.Add(archetypeSkillContainer.skill);

                    if (skillsLevelMap.ContainsKey(1))
                    {
                        skillsLevelMap[1] += 1;
                    }
                    else
                    {
                        skillsLevelMap.Add(1, 1);
                    }
                }
            }
            
            foreach (ArchetypeSkillContainer archetypeSkillContainer in skillsLevel2)
            {
                if (archetypeSkillContainer.hasLearned)
                {
                    learnedSkills.Add(archetypeSkillContainer.skill);
                    
                    if (skillsLevelMap.ContainsKey(2))
                    {
                        skillsLevelMap[2] += 1;
                    }
                    else
                    {
                        skillsLevelMap.Add(2, 1);
                    }
                }
            }
            
            foreach (ArchetypeSkillContainer archetypeSkillContainer in skillsLevel3)
            {
                if (archetypeSkillContainer.hasLearned)
                {
                    learnedSkills.Add(archetypeSkillContainer.skill);
                    
                    if (skillsLevelMap.ContainsKey(3))
                    {
                        skillsLevelMap[3] += 1;
                    }
                    else
                    {
                        skillsLevelMap.Add(3, 1);
                    }
                }
            }
            
            foreach (ArchetypeSkillContainer archetypeSkillContainer in skillsLevel4)
            {
                if (archetypeSkillContainer.hasLearned)
                {
                    learnedSkills.Add(archetypeSkillContainer.skill);
                    
                    if (skillsLevelMap.ContainsKey(4))
                    {
                        skillsLevelMap[4] += 1;
                    }
                    else
                    {
                        skillsLevelMap.Add(4, 1);
                    }
                }
            }
        }
        
    }
}