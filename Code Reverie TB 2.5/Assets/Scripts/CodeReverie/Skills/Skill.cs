using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityString;
using Rewired;
using UnityEngine;

namespace CodeReverie
{
    //Action Skill types: Basic(Melee or Range), Dodge Skill, Standard (core) Skill, Alchemic Burst Skill (ultimate skill)
    
    public abstract class Skill
    {
        public SkillDataContainer info;
        public string skillID;
        public int level = 1;
        public CharacterBattleManager source;
        public MeleeSkillObject meleeSkillObject;
        public List<Skill> resonanceSkills = new List<Skill>();
        
        
        // public CharacterUnit source;
        //
        // public Dictionary<string, SkillModifier> skillModifiers = new Dictionary<string, SkillModifier>();
        //
        //
        // public Skill()
        // {
        //
        // }
        
        public Skill(SkillDataContainer skillDetails)
        {
            info = skillDetails;
            skillID = info.id;


            foreach (SkillDataContainer skillDataContainer in info.resonanceSkillsList)
            {
                resonanceSkills.Add(SkillsManager.Instance.CreateSkill(skillDataContainer));
            }
            
            
        }
        
        public virtual void UseSkill()
        {
            string skillName = String.IsNullOrEmpty(info.skillName) ? info.skillId : info.skillName;
            
            Debug.Log($"Use {skillName}");
        }

        public void SetSkillCamera()
        {
            
        }
        

        public abstract void SubscribeSkillListeners();
        public abstract void UnsubscribeSkillListeners();
        
        // public virtual void SubscribeSkillListeners()
        // {
        //     GameManager.Instance.playerInput.AddInputEventDelegate(OnButtonDown, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Attack");
        // }
        //
        // public virtual void UnsubscribeSkillListeners()
        // {
        //     GameManager.Instance.playerInput.RemoveInputEventDelegate(OnButtonDown);
        // }

        public abstract void OnButtonDown();
        public abstract void OnButtonDownAttacking();

        public abstract void OnButtonHold();
        public virtual void OnButtonHoldAttacking(){}

        public abstract void OnButtonUp();
        public abstract void OnButtonUpAttacking();


        public virtual void UseSkillEffect()
        {
            
        }

        public virtual void UseNextCombo(int index)
        {
            
        }
        
        public Sprite GetSpriteIcon()
        {
            if (info == null)
            {
                return null;
            }
            
            
            //Sprite sprite = Resources.Load<Sprite>(skillDetails.iconPath);
            
            
            Sprite sprite = info.icon;
            
            if (sprite != null)
            {
                return sprite;
            }
            
            
            return null;
        }
        
        
        
        //
        // public virtual void SetSkillModifier()
        // {
        //     // foreach (SkillModifier skillModifier in skillModifiers)
        //     // {
        //     //     //skillModifier.SetSkillModifier();
        //     // }
        //     
        //     
        //     
        // }
        //
        // public virtual void ResetSkillProperties()
        // {
        //     
        // }
        //
        //
        // public SkillModifier GetSkillModifierById(string id)
        // {
        //     return skillModifiers[id];
        // }
        //
        // public void ActivateSkillModifier(string id)
        // {
        //
        //     if (GetSkillModifierById(id).isActive)
        //     {
        //         return;
        //     }
        //     
        //     GetSkillModifierById(id).isActive = true;
        //     GetSkillModifierById(id).ActivateSkillModifier();
        // }
        //
        // public void DeactivateSkillModifier(string id)
        // {
        //     
        //     if (!GetSkillModifierById(id).isActive)
        //     {
        //         return;
        //     }
        //     
        //     GetSkillModifierById(id).isActive = false;
        //     GetSkillModifierById(id).DeactivateSkillModifier();
        //     
        // }


        public void SetSkillInfo()
        {
            if (info == null && !String.IsNullOrEmpty(skillID))
            {
                info = SkillsManager.Instance.GetSkillById(skillID);
            }
            
        }
        
    }
}