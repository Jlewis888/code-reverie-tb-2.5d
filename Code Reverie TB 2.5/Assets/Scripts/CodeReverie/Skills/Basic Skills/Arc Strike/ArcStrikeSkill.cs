using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class ArcStrikeSkill : Skill
    {
        //TriggerSkill
        //ComboWindowOpen
        //NextCombo (multiple)
        //OnAttackEnd

        public List<string> animationsList = new List<string>();
        public int currentComboIndex = 1;
        public int MaxCombCount = 3;
        public bool comboWindowOpen;
        public bool continueAttack;
        

        public ArcStrikeSkill(SkillDataContainer skillDetails) : base(skillDetails)
        {
            animationsList.Add("arcalia_attack_1");
            animationsList.Add("arcalia_attack_2");
            animationsList.Add("arcalia_attack_3");
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
            Debug.Log("im here");
            //PlayerController.Instance.activeCharacterController.GetComponent<AnimationManager>().ChangeAnimationState(animationsList[0]);
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
        
        public override void UseNextCombo(int index)
        {
            string animation;
            
            //Debug.Log($"Current Index is: {index}");

            animation = animationsList[index];
            
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
                
            }
        }
        
        
        public void OnPlayerComboWindowOpen()
        {
            comboWindowOpen = true;
        }


        public void OnPlayerAttackEnd()
        {
            Debug.Log("yo yo yo");
            comboWindowOpen = false;
            continueAttack = false;
            currentComboIndex = 1;
        }
    }
}