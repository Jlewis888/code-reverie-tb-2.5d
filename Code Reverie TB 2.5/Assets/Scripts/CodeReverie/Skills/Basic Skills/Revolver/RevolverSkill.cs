using System;
using Rewired;
using UnityEngine;

namespace CodeReverie
{
    public class RevolverSkill : Skill
    {
        public RevolverSkill(SkillDataContainer skillDataContainer) : base(skillDataContainer){}


        public override void UseSkill()
        {
            //PlayerController.Instance.activeCharacterController.GetComponent<AnimationManager>().ChangeAnimationState(info.initialAnimation);
        }

        public override void SubscribeSkillListeners()
        {
            //GameManager.Instance.playerInput.AddInputEventDelegate(OnButtonDown, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Attack");
        }
        
        public override void UnsubscribeSkillListeners()
        {
            //GameManager.Instance.playerInput.RemoveInputEventDelegate(OnButtonDown);
        }

        public override void OnButtonDown()
        {
            string skillName = String.IsNullOrEmpty(info.skillName) ? info.skillId : info.skillName;
            
            Debug.Log($"Use {skillName} On button down");
        }

        public override void OnButtonDownAttacking()
        {
            throw new NotImplementedException();
        }

        public override void OnButtonHold()
        {
            //Debug.Log("Fire Weapon");
            //PlayerController.Instance.activeCharacterController.GetComponent<AnimationManager>().ChangeAnimationState("cecil_revolver_fire");
            //PlayerController.Instance.activeCharacterController.GetComponent<AnimationManager>().currentAnimation = "";
        }
        

        public override void OnButtonUp()
        {
           
        }

        public override void OnButtonUpAttacking()
        {
            
            EventManager.Instance.combatEvents.onCombatAnimationEnd += OnPlayerAttackEnd;


            // PlayerController.Instance.playerCombatController.characterCombatState = CharacterCombatState.Idle;
            // PlayerController.Instance.playerMovementController.characterMovementState = CharacterMovementState.Idle;
        }

        public void OnPlayerAttackEnd()
        {
            Debug.Log("Stop Firing Weapon");
            EventManager.Instance.combatEvents.onCombatAnimationEnd -= OnPlayerAttackEnd;
            EventManager.Instance.combatEvents.OnPlayerAttackEnd();
        }
    }
}