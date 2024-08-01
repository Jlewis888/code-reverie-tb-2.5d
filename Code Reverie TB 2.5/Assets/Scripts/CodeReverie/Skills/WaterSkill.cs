using Rewired;
using UnityEngine;

namespace CodeReverie
{
    public class WaterSkill : Skill
    {

        public WaterSkill(SkillDataContainer skillDetails): base(skillDetails)
        {
            
        }

        // public override void UseSkill()
        // {
        //     
        //     GameObject gameObject = Resources.Load<GameObject>("Projectile");
        //
        //     GameObject.Instantiate(gameObject, source.directionPoint.transform.position,
        //         source.directionPoint.transform.rotation);
        //
        // }


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