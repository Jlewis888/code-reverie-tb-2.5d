using Rewired;
using UnityEngine;

namespace CodeReverie
{
    public class FireballSkill : Skill
    {
        public FireballSkill(SkillDataContainer skillDataContainer) : base(skillDataContainer) {}


        public override void UseSkill()
        {
            base.UseSkill();
            //SkillObject skillObject = GameObject.Instantiate(info.skillGameObject, source.castObjectHolder.transform.position, Quaternion.identity);
            
            
            PlaySkillAnimation();
            
            // SkillObject skillObject = GameObject.Instantiate(info.skillGameObject, source.castObjectHolder.transform);
            // skillObject.characterUnitSource = source;
            // skillObject.Init();

            //PlayerController.Instance.activeCharacterController.GetComponent<AnimationManager>().ChangeAnimationState(info.initialAnimation);
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


        public void ContinueAttackCombo()
        {
            
        }
        
    }
}