using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class SkillsManager : ManagerSingleton<SkillsManager>
    {
        public SkillDataContainerList skills;
        public SkillModifierDataContainerList skillModifierDetailsList;
        
        //public List<SkillDataContainer> skillDetailsList;
        public Dictionary<string, SkillDataContainer> allSkills = new Dictionary<string, SkillDataContainer>();
        
        
        private void Awake()
        {
            base.Awake();
            Initialize();
        }
        
        public bool Initialized { get; set; }
        
        public void Initialize()
        {
            Init();
            
            Initialized = true;
        }
        
        public void Init()
        {
            if (skills != null)
            {
                allSkills = skills.GetAllSkills();
            }
            
            
            // allActionSkills = new Dictionary<string, ActionSkill>();
            // allActionSkills = GetAllActionSkills();
            

        }
        
       
        
        public SkillDataContainer GetSkillById(string id)
        {
            
      
            if (!allSkills.ContainsKey(id))
            {
                Debug.Log("Skill does not exist in skills list");
                return null;
            }
            
            
            return allSkills[id];
        }
       
        
        // public T GetSkillById<T>(string id) where T : class, new()
        // {
        //     if (!allActionSkills.ContainsKey(id))
        //     {
        //         Debug.Log("Skill does not exist in skills list");
        //         return new T();
        //     }
        //     
        //     
        //     return allSkills[id] as T;
        // }
        
        
        
        
        
        
        
        
        Dictionary<string, SkillModifier> GetAllSkillModifiers()
        {
            //SkillDataContainer[] skillDetailsList = Resources.LoadAll<SkillDataContainer>("Skills");

            return skillModifierDetailsList.GetAllSkillModifiers(transform);
        }
        
        // public SkillModifier GetSkillModifierById(string skillId, string skillModifierId)
        // {
        //     return GetActionSkillById(skillId).skillModifiers[skillModifierId];
        // }

        // public void ActivateSkillModifier(string skillId, string skillModifierId)
        // {
        //
        //     if (GetSkillModifierById(skillId, skillModifierId).isActive)
        //     {
        //         return;
        //     }
        //     
        //     GetSkillModifierById(skillId, skillModifierId).isActive = true;
        //     GetSkillModifierById(skillId, skillModifierId).ActivateSkillModifier();
        // }
        //
        // public void DeactivateSkillModifier(string skillId, string skillModifierId)
        // {
        //     
        //     if (!GetSkillModifierById(skillId, skillModifierId).isActive)
        //     {
        //         return;
        //     }
        //     
        //     GetSkillModifierById(skillId, skillModifierId).isActive = false;
        //     GetSkillModifierById(skillId, skillModifierId).DeactivateSkillModifier();
        //     
        // }
        

        public T CreateSkill<T>(string skillId) where T : Skill
        {
            
            Type itemType = Type.GetType($"CodeReverie.{GetSkillById(skillId).skillId}");
            T skill = null;
            if (itemType != null)
            {
                return (T)Activator.CreateInstance(itemType, new [] {GetSkillById(skillId)});
            }
            
            
            return null;
        }
        
        public Skill CreateSkill(SkillDataContainer skillDataContainer)
        {
      
            if (skillDataContainer != null)
            {
                Type itemType = Type.GetType($"CodeReverie.{GetSkillById(skillDataContainer.skillId).skillId}");
          
                if (itemType != null)
                {
                    return (Skill)Activator.CreateInstance(itemType, new [] {GetSkillById(skillDataContainer.skillId)});
                }
                else
                {
                    Debug.Log("SKill is null");
                }
            }


            return null;
        }
        
        
        public Skill CreateSkill(string id)
        {
            
            if (GetSkillById(id) != null)
            {
                Type itemType = Type.GetType($"CodeReverie.{id}");
          
                if (itemType != null)
                {
                    return (Skill)Activator.CreateInstance(itemType, new [] {GetSkillById(id)});
                }
            }


            return null;
        }
        
        

        // public ActionSkill CreateActionSkill(string skillId)
        // {
        //
        //     SkillDataContainer skillDetails = GetActionSkillById(skillId).info;
        //     
        //     Type itemType = Type.GetType($"ProjectAlchemy.{skillDetails.skillId}");
        //     ActionSkill skill = null;
        //     if (itemType != null)
        //     {
        //         return Activator.CreateInstance(itemType, new [] {skillDetails}) as ActionSkill;
        //     }
        //
        //     return null;
        // }

    }
}