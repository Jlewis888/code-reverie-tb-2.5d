using Rewired;
using UnityEngine;

namespace CodeReverie
{
    public class InfernalVortexSkill : Skill
    {
        public InfernalVortexSkill(SkillDataContainer skillDataContainer) : base(skillDataContainer) {}


        public override void UseSkill()
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            GameObject.Instantiate(info.skillGameObject, pos, Quaternion.identity);
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