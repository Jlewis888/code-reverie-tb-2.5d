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
            
            // comboListFront.Add("fullbody_front_fire_fist_melee_1");
            // comboListFront.Add("fullbody_front_fire_fist_melee_2");
            // comboListFront.Add("fullbody_front_fire_fist_melee_1");
            //
            // comboListBack.Add("fullbody_back_fire_fist_melee_1");
            // comboListBack.Add("fullbody_back_fire_fist_melee_2");
            // comboListBack.Add("fullbody_back_fire_fist_melee_1");
            //
            // comboListSide.Add("fullbody_side_fire_fist_melee_1");
            // comboListSide.Add("fullbody_side_fire_fist_melee_2");
            // comboListSide.Add("fullbody_side_fire_fist_melee_1");
            
        }
        
        public override void UseSkill()
        {
            //source.animator.Play(comboList[0]);
            //Debug.Log(PlayerController.Instance);
            
            
            
        }

        public override void SubscribeSkillListeners()
        {
            EventManager.Instance.combatEvents.onPlayerComboWindowOpen += OnPlayerComboWindowOpen;
            EventManager.Instance.combatEvents.onPlayerCombo += NextCombo;
            EventManager.Instance.combatEvents.onPlayerAttackEnd += UnsubscribeSkillListeners;
            EventManager.Instance.combatEvents.onPlayerAttackEnd += OnPlayerAttackEnd;
        }

        public override void UnsubscribeSkillListeners()
        {
            
            EventManager.Instance.combatEvents.onPlayerComboWindowOpen -= OnPlayerComboWindowOpen;
            EventManager.Instance.combatEvents.onPlayerCombo -= NextCombo;
            EventManager.Instance.combatEvents.onPlayerAttackEnd -= UnsubscribeSkillListeners;
            EventManager.Instance.combatEvents.onPlayerAttackEnd -= OnPlayerAttackEnd;
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
            if (continueAttack)
            {
                currentComboIndex++;
                comboWindowOpen = false;
                continueAttack = false;
                
                if (currentComboIndex <= MaxCombCount)
                {
                    UseNextCombo(currentComboIndex - 1);
                }
                else
                {
                    //Debug.Log("End Combo");
                }
                
            }
        }
        
        
        public void OnPlayerComboWindowOpen()
        {
            comboWindowOpen = true;
        }


        public void OnPlayerAttackEnd()
        {
            comboWindowOpen = false;
            continueAttack = false;
            currentComboIndex = 1;
        }
        
        
    }
}