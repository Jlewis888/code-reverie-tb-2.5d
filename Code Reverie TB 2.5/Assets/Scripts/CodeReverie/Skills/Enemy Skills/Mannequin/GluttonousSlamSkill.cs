namespace CodeReverie
{
    public class GluttonousSlamSkill : Skill
    {
        public GluttonousSlamSkill(SkillDataContainer skillDetails) : base(skillDetails)
        {
        }
        
        public override void UseSkill()
        {
            base.UseSkill();
            
            //CombatManager.Instance.combatManagerState = CombatManagerState.OnSkillUseEnd;
            //source.EndTurn();
            //source.DequeueAction();
            
            //PlaySkillAnimation();
            
        }
        

        public override void SubscribeSkillListeners()
        {
            throw new System.NotImplementedException();
        }

        public override void UnsubscribeSkillListeners()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonDown()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonDownAttacking()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonHold()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonUp()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonUpAttacking()
        {
            throw new System.NotImplementedException();
        }
    }
}