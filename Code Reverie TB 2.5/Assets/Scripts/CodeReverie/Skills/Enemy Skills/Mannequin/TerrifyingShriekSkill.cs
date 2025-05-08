namespace CodeReverie
{
    public class TerrifyingShriekSkill : Skill
    {
        public TerrifyingShriekSkill(SkillDataContainer skillDetails) : base(skillDetails)
        {
        }


        public override void OnSkillUseEnd()
        {
            FearStatusEffect fearStatusEffect = new FearStatusEffect(1);

            fearStatusEffect.source = source;
            fearStatusEffect.target = source.target;
            
            fearStatusEffect.TriggerStatusEffect();
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