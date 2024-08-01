using System;
using System.Reflection;
using Rewired;
using UnityEngine;

namespace CodeReverie
{
    public class SkillSlot
    {
        public float skillSlotCooldownTimer;
        public Skill skill;

        public void SetCooldownTimer()
        {
            //skillSlotCooldownTimer = skill.info.cooldown;
        }
        
        public void ResetCooldownTimer()
        {
            skillSlotCooldownTimer = 0;
        }

        public void SubscribeListeners()
        {
            // if (skill != null)
            // {
            //
            //     string actionId = "";
            //
            //     switch (skill.info.skillType)
            //     {
            //         case SkillType.Basic:
            //             actionId = "Attack";
            //             break;
            //     }
            //     
            //     
            //     foreach (var skillEventListener in skill.info.skillEventListeners)
            //     {
            //         // MethodInfo methodInfo = GetType().GetMethod(skillEventListener.methodName);
            //         //
            //         // Action<InputActionEventData> data;
            //         //
            //         // methodInfo.Invoke(this, null);
            //
            //         GameManager.Instance.playerInput.AddInputEventDelegate(SubscribeSkillListeners, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, actionId);
            //         switch (skillEventListener.skillInputType)
            //         {
            //             case SkillInputType.OnButtonDown:
            //                 GameManager.Instance.playerInput.AddInputEventDelegate(OnButtonDown, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, actionId);
            //                 break;
            //             case SkillInputType.OnButtonDownAttacking:
            //                 GameManager.Instance.playerInput.AddInputEventDelegate(OnButtonDownAttacking, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, actionId);
            //                 break;
            //             case SkillInputType.OnButtonHold:
            //                 GameManager.Instance.playerInput.AddInputEventDelegate(OnButtonHold, UpdateLoopType.Update, InputActionEventType.ButtonPressed, actionId);
            //                 break;
            //             case SkillInputType.OnButtonHoldAttacking:
            //                 GameManager.Instance.playerInput.AddInputEventDelegate(OnButtonHoldAttacking, UpdateLoopType.Update, InputActionEventType.ButtonPressed, actionId);
            //                 break;
            //             case SkillInputType.OnButtonUp:
            //                 GameManager.Instance.playerInput.AddInputEventDelegate(OnButtonUp, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, actionId);
            //                 break;
            //             case SkillInputType.OnButtonUpAttacking:
            //                 GameManager.Instance.playerInput.AddInputEventDelegate(OnButtonUpAttacking, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, actionId);
            //                 break;
            //            
            //         }
            //     }
            // }
        }
        
        public void UnsubscribeListeners()
        {
            // GameManager.Instance.playerInput.RemoveInputEventDelegate(SubscribeSkillListeners);
            // GameManager.Instance.playerInput.RemoveInputEventDelegate(OnButtonDown);
            // GameManager.Instance.playerInput.RemoveInputEventDelegate(OnButtonDownAttacking);
            // GameManager.Instance.playerInput.RemoveInputEventDelegate(OnButtonHold);
            // GameManager.Instance.playerInput.RemoveInputEventDelegate(OnButtonHoldAttacking);
            // GameManager.Instance.playerInput.RemoveInputEventDelegate(OnButtonUp);
            // GameManager.Instance.playerInput.RemoveInputEventDelegate(OnButtonUpAttacking);
            //
            // skill.UnsubscribeSkillListeners();
        }

        public bool CanUseSkill()
        {
            // if (skillSlotCooldownTimer <= 0 && PlayerController.Instance.playerMovementController.characterMovementState != CharacterMovementState.Dodging)
            // {
            //     return true;
            // }

            return false;
        }

        public void SubscribeSkillListeners(InputActionEventData data)
        {
            
            //string skillName = String.IsNullOrEmpty(skill.info.skillName) ? skill.info.skillId : skill.info.skillName;
            
            //Debug.Log($"Subscribe {skillName}");
            skill.SubscribeSkillListeners();
        }
        
        
        public void OnButtonDown(InputActionEventData data)
        {

            // if (CanUseSkill() && PlayerController.Instance.playerCombatController.characterCombatState == CharacterCombatState.Idle)
            // {
            //    
            //     EventManager.Instance.combatEvents.OnPlayerAttackStart();
            //     skill.OnButtonDown();
            //     SetCooldownTimer();
            // }
        }

        public void OnButtonDownAttacking(InputActionEventData data)
        {
            // if (PlayerController.Instance.playerCombatController.characterCombatState == CharacterCombatState.Attacking)
            // {
            //     skill.OnButtonDownAttacking();
            // }
        }

        public void OnButtonHold(InputActionEventData data)
        {
            // if (CanUseSkill())
            // {
            //    
            //     EventManager.Instance.combatEvents.OnPlayerAttackStart();
            //     skill.OnButtonHold();
            //     SetCooldownTimer();
            // }
        }
        
        public void OnButtonHoldAttacking(InputActionEventData data)
        {
            // if (PlayerController.Instance.playerCombatController.characterCombatState == CharacterCombatState.Attacking)
            // {
            //     skill.OnButtonHoldAttacking();
            // }
        }

        public void OnButtonUp(InputActionEventData data)
        {
            if (CanUseSkill())
            {
               
                EventManager.Instance.combatEvents.OnPlayerAttackStart();
                skill.OnButtonUp();
                SetCooldownTimer();
            }
            
        }
        
        public void OnButtonUpAttacking(InputActionEventData data)
        {
            // if (PlayerController.Instance.playerCombatController.characterCombatState == CharacterCombatState.Attacking)
            // {
            //     skill.OnButtonUpAttacking();
            // }
        }
        
    }
}