using System;
using System.Collections.Generic;
using Rewired;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace CodeReverie
{
    public class FireBlastSkill : Skill
    {

        public int currentComboIndex = 1;
        public int MaxCombCount = 3;
        public bool comboWindowOpen;
        public bool continueAttack;
        
        // private FireBlastSkillObject skillObject;
        //
        public FireBlastSkill(SkillDataContainer skillDetails) : base(skillDetails)
        {
            base.UseSkill();
            
        }
        
        public override void UseSkill()
        {
            //source.animator.Play(comboList[0]);
            //Debug.Log(PlayerController.Instance);
            
            SkillObject skillObject = GameObject.Instantiate(info.skillGameObject, CombatManager.Instance.skillObjectSpawnPoint1.transform);
            skillObject.characterUnitSource = source;
            skillObject.Init();
            
        }

        public override void SubscribeSkillListeners()
        {
         
        }

        public override void UnsubscribeSkillListeners()
        {
            
            
        }

        public override void OnButtonDown()
        {
            
            //PlayerController.Instance.activeCharacterController.GetComponent<AnimationManager>().ChangeAnimationState(info.initialAnimation);
        }

        public override void OnButtonDownAttacking()
        {
            if (comboWindowOpen)
            {
                continueAttack = true;
            }
        }

        public override void OnButtonHold()
        {
            throw new NotImplementedException();
        }

        public override void OnButtonUp()
        {
            throw new NotImplementedException();
        }

        public override void OnButtonUpAttacking()
        {
            throw new NotImplementedException();
        }

        public override void UseNextCombo(int index)
        {
            string animation;

            animation = info.animationComboList[index];
            
            //PlayerController.Instance.activeCharacterController.GetComponent<AnimationManager>().ChangeAnimationState(animation);
        }

        public void NextCombo()
        {
            
        }
        
        
        public void OnPlayerComboWindowOpen()
        {
            comboWindowOpen = true;
        }


       
        
        
    }
}