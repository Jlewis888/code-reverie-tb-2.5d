using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class Character
    {
        public CharacterDataContainer info;
        public CharacterController characterController;
        public CharacterState characterState;
        
        public CharacterSkills characterSkills;
        
        public Archetype equippedArchetype;
        public List<Archetype> availableArchetypes = new List<Archetype>();

        //public int availableSkillPoints;
        public int usedSkillPoints;


        public CharacterGear characterGear = new CharacterGear();

        public float currentHealth;

        public Dictionary<ArchetypeCategory, int> archetypeLevelMap = new Dictionary<ArchetypeCategory, int>();
        
        public int currentLevel = 1;
        public float currentExp = 0;
        public int skillPoints = 0;

        public Character(CharacterDataContainer info)
        {
            this.info = info;
            currentLevel = 1;

            characterSkills = new CharacterSkills();

            foreach (ArchetypeDataContainer archetypeDataContainer in info.archetypes)
            {
                //availableArchetypes.Add(new Archetype(archetypeDataContainer));
            }

            if (availableArchetypes.Count > 0)
            {
                equippedArchetype = availableArchetypes[0];
            } 
            
            characterGear = new CharacterGear();
            
            

            foreach (Archetype archetype in availableArchetypes)
            {

                ArchetypeSkillsToLearn(archetype, SkillType.Basic);
                ArchetypeSkillsToLearn(archetype, SkillType.Dodge);
                ArchetypeSkillsToLearn(archetype, SkillType.Action);
                ArchetypeSkillsToLearn(archetype, SkillType.AlchemicBurst);

                // characterSkills.learnedSkills.Add(archetype.skills.equippedSkills[SkillType.Dodge].skill);
                // characterSkills.learnedSkills.Add(archetype.skills.equippedSkills[SkillType.Action].skill);
                // characterSkills.learnedSkills.Add(archetype.skills.equippedSkills[SkillType.AlchemicBurst].skill);
            }
            
            Array stat = Enum.GetValues(typeof(ArchetypeCategory));
            
            for (int i = 0; i < stat.Length; i++)
            {
                if (!archetypeLevelMap.ContainsKey((ArchetypeCategory)stat.GetValue(i)))
                {
                    archetypeLevelMap.Add((ArchetypeCategory)stat.GetValue(i), 0);
                }
            }
            
            
        }

        public void Init()
        {
            if (characterController != null)
            {
                GameObject.Destroy(characterController.gameObject);
            }
        }


        public Sprite GetCharacterPortrait()
        {

            Sprite sprite = info.characterPortrait;


            if (sprite != null)
            {
                return sprite;
            }
            
            return null;
        }

        public Archetype EquippedArchetype
        {
            get { return equippedArchetype; }

            set
            {
                if (value != equippedArchetype)
                {
                    equippedArchetype = value;
                    //EventManager.Instance.playerEvents.OnSkillSlotUpdate();
                }
            }
        }

        public void ArchetypeSkillsToLearn(Archetype archetype, SkillType skillType)
        {
            if (archetype.skills.equippedSkills.ContainsKey(skillType))
            {
                if (archetype.skills.equippedSkills[skillType] != null)
                {
                    if (archetype.skills.equippedSkills[skillType].skill != null)
                    {
                        characterSkills.learnedSkills.Add(archetype.skills.equippedSkills[skillType].skill);
                    }
                }
            }
        }
        
        public int Level
        {
            get => currentLevel;
            set
            {
                if (currentLevel + value > currentLevel)
                {
                    EventManager.Instance.playerEvents.OnLevelUp();
                    currentLevel += value;
                    skillPoints += 1;
                }
            }
        }
        
        
        public float Experience
        {
            get => currentExp;

            set
            {
                if (Level >= 99)
                {
                    currentExp = 0;
                    return;
                }

                currentExp += value;

                if (Level < 99)
                {
                    while (ExperienceAboveNextLevelCheck())
                    {
                        currentExp -= PlayerManager.Instance.playerExperienceMap.experienceMap[Level];

                        Level = 1;

                        if (Level >= 99)
                        {
                            currentExp = 0;
                            break;
                        }
                    }
                }
            }
        }

        public bool ExperienceAboveNextLevelCheck()
        {
            if (currentExp >= PlayerManager.Instance.playerExperienceMap.experienceMap[Level])
            {
                return true;
            }

            return false;
        }


        // public bool SkillEquipped(string skillID)
        // {
        //
        //     foreach (SkillSlot skillSlot in characterSkills.equippedActionSkills.Values)
        //     {
        //         if (skillSlot.skill != null)
        //         {
        //             if (skillSlot.skill.info.id == skillID)
        //             {
        //                 return true;
        //             }
        //         }
        //     }
        //     
        //     return false;
        // }

        // public void EquipSkill(int index, SkillDataContainer skillDataContainer)
        // {
        //     foreach (SkillSlot skillSlot in characterSkills.equippedActionSkills.Values)
        //     {
        //         if (skillSlot.skill != null)
        //         {
        //             if (skillSlot.skill.info.id == skillDataContainer.id)
        //             {
        //                 skillSlot.skill = null;
        //             }
        //         }
        //         
        //     }
        //
        //
        //     characterSkills.equippedActionSkills[index].skill = SkillsManager.Instance.CreateSkill(skillDataContainer);
        //
        // }

        // public void EquipSkill(SkillDataContainer skillDataContainer, SkillType skillType)
        // {
        //     
        //     // if (characterSkills.equippedSkills.ContainsKey(skillType))
        //     // {
        //     //     characterSkills.equippedSkills[skillType].skill =
        //     //         SkillsManager.Instance.CreateSkill(skillDataContainer);
        //     // }
        //     // else
        //     // {
        //     //     characterSkills.equippedSkills.Add(skillType, new SkillSlot());
        //     //     characterSkills.equippedSkills[skillType].skill =
        //     //         SkillsManager.Instance.CreateSkill(skillDataContainer);
        //     //     
        //     // }
        // }
        
        // public void EquipSkill(SkillDataContainer skillDataContainer, int skillSlot)
        // {
        //     
        //     if (characterSkills.equippedSkills.ContainsKey(skillSlot))
        //     {
        //         characterSkills.equippedSkills[skillSlot].skill =
        //             SkillsManager.Instance.CreateSkill(skillDataContainer);
        //     }
        //     else
        //     {
        //         characterSkills.equippedSkills.Add(skillSlot, new SkillSlot());
        //         characterSkills.equippedSkills[skillSlot].skill =
        //             SkillsManager.Instance.CreateSkill(skillDataContainer);
        //         
        //     }
        // }


        public void EquipAlchemicBurst(SkillDataContainer skillDataContainer)
        {
            characterSkills.equippedAlchemicBurst.skill = SkillsManager.Instance.CreateSkill(skillDataContainer);
        }

        public int SkillPoints
        {
            get
            {
                return PlayerManager.Instance.skillPoints - usedSkillPoints;
            }

            // set
            // {
            //     usedSkillPoints += value;
            //
            //     if (usedSkillPoints < 0)
            //     {
            //         usedSkillPoints = 0;
            //     }
            //
            //     if (usedSkillPoints > PlayerManager.Instance.skillPoints)
            //     {
            //         usedSkillPoints = PlayerManager.Instance.skillPoints;
            //     }
            //     
            // }
            
            
        }


        public void RemoveSkillPoint(int skillPoints)
        {
            usedSkillPoints += skillPoints;
            
            if (usedSkillPoints < 0)
            {
                usedSkillPoints = 0;
            }
            else if (usedSkillPoints >= PlayerManager.Instance.skillPoints)
            {
                usedSkillPoints = PlayerManager.Instance.skillPoints;
            }
        }
        
        public void AddSkillPoint(int skillPoints)
        {
            usedSkillPoints -= skillPoints;
            
            if (usedSkillPoints < 0)
            {
                usedSkillPoints = 0;
            }
            else if (usedSkillPoints >= PlayerManager.Instance.skillPoints)
            {
                usedSkillPoints = PlayerManager.Instance.skillPoints;
            }
        }
        
        public void SpawnCharacter(Vector3 transform)
        {
            characterController = GameObject.Instantiate(info.characterUnitPF, transform, Quaternion.identity);
            characterController.gameObject.SetActive(false);
            characterController.character = this;
            characterController.GetComponent<Health>().SetHealth();
        }

    }
}