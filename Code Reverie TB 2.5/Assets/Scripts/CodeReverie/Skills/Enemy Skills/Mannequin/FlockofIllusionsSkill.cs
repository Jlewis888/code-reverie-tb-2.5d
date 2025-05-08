namespace CodeReverie
{
    public class FlockofIllusionsSkill : Skill
    {
        public FlockofIllusionsSkill(SkillDataContainer skillDetails) : base(skillDetails)
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

        public override void OnSkillUseEnd()
        {
            CharacterDataContainer characterDataContainer = CharacterManager.Instance.GetCharacterByCharacterId("Mannequin Doll model 2 full");

            int count = 3;

            for (int i = 0; i < count; i++)
            {
                CombatManager.Instance.InstantiateNewEnemy(characterDataContainer, source.transform.position);
            }


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