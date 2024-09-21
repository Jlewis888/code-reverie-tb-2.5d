using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SkillCommandMenuNavigationButton : CommandMenuNavigationButton
    {
        public Skill baseSkill;
        public Skill skill;
        public int skillIndex;
        public List<Skill> skills = new List<Skill>();
        public TMP_Text skillCostText;
        public List<GameObject> resonancePointsCostObjects = new List<GameObject>();


        public void Init()
        {

            skills = new List<Skill>();
            
            if (baseSkill != null)
            {
                skills.Add(baseSkill);

                if (baseSkill.resonanceSkills != null)
                {
                    skills.AddRange(baseSkill.resonanceSkills);
                }
            }


            skillIndex = 0;
            
            SetCurrentSkill();
        }


        public void SetNextSkill()
        {

            skillIndex++;

            if (skillIndex >= skills.Count)
            {
                skillIndex = skills.Count - 1;
            }

            SetCurrentSkill();
            
        }
        
        
        public void SetPrevSkill()
        {

            skillIndex--;

            if (skillIndex <= 0)
            {
                skillIndex = 0;
            }

            SetCurrentSkill();
            
        }


        public void SetCurrentSkill()
        {
            skill = skills[skillIndex];
            nameText.text = skills[skillIndex].info.skillName;

            skillCostText.text = $"SP {skill.info.skillPointsCost}";

            for (int i = 0; i < resonancePointsCostObjects.Count; i++)
            {
                resonancePointsCostObjects[i].SetActive(false);

                if (i < skill.info.resonancePointsCost)
                {
                    resonancePointsCostObjects[i].SetActive(true);
                }
                
            }
            
        }
        
        
    }
}