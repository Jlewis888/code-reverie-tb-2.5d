﻿using Rewired;

namespace CodeReverie
{
    
    
    
    
    public class ActionSkill : Skill
    {
        
        
        public ActionSkill(SkillDataContainer skillDataContainer) : base(skillDataContainer)
        {
            
        }

        public override void SubscribeSkillListeners()
        {
        }

        public override void UnsubscribeSkillListeners()
        {
        }

        public override void OnButtonDown()
        {
            //throw new System.NotImplementedException();
        }

        public override void OnButtonDownAttacking()
        {
            //throw new System.NotImplementedException();
        }

        public override void OnButtonHold()
        {
            //throw new System.NotImplementedException();
        }

        public override void OnButtonUp()
        {
            //throw new System.NotImplementedException();
        }

        public override void OnButtonUpAttacking()
        {
            //throw new System.NotImplementedException();
        }
    }
}