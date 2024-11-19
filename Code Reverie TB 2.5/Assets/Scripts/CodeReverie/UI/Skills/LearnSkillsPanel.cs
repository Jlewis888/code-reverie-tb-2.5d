using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class LearnSkillsPanel : SerializedMonoBehaviour
    {
        public GameObject skillsContentHolder;
        public List<ArchetypeSkillContainer> archetypeSkillContainers;
        public LearnSkillPauseMenuNavigationButton learnSkillPauseMenuNavigationButtonPF;
        public List<PauseMenuNavigationButton> pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();

        private void Awake()
        {
            // Clear();
        }


        public void SetSkills()
        {
            Clear();
            
            foreach (ArchetypeSkillContainer archetypeSkillContainer in archetypeSkillContainers)
            {
                
                LearnSkillPauseMenuNavigationButton learnSkillPauseMenuNavigationButton = Instantiate(learnSkillPauseMenuNavigationButtonPF, skillsContentHolder.transform);

                learnSkillPauseMenuNavigationButton.archetypeSkillContainer = archetypeSkillContainer;
                learnSkillPauseMenuNavigationButton.nameText.text = archetypeSkillContainer.skill.info.skillName;
                pauseMenuNavigationButtons.Add(learnSkillPauseMenuNavigationButton);
            }
        }

        public void Clear()
        {
            
            pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
            
            foreach (Transform child in skillsContentHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            
        }
    }
}