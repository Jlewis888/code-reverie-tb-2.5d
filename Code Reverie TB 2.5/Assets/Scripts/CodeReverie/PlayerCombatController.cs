using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class PlayerCombatController : SerializedMonoBehaviour
    {
        public CharacterCombatState characterCombatState;
        public bool canAttack;
        public bool attackHold;
        public int currentComboCount;
        public int maxComboCount;
        public bool comboWindowOpen;

        public bool canCombo;
        public float cooldownTimer;
        public bool continueAttack;
        public SkillType currentSkillTypeAttack;
        public Skill currentSkill;
        public Vector3 battlePosition;
        
        private void OnEnable()
        {
            
            
            EventManager.Instance.combatEvents.onPlayerAttackStart += OnPlayerAttackStart;
            EventManager.Instance.combatEvents.onPlayerAttackEnd += OnBasicAttackEnd;
            EventManager.Instance.playerEvents.onDodgeEnd += OnBasicAttackEnd;
            
            EventManager.Instance.combatEvents.onPlayerComboWindowOpen += OnPlayerComboWindowOpen;
            EventManager.Instance.playerEvents.onCharacterSwap += Reset;
            //EventManager.Instance.combatEvents.onPlayerCombo += NextCombo;
            
        }

        private void OnDisable()
        {
            
            EventManager.Instance.combatEvents.onPlayerAttackStart -= OnPlayerAttackStart;
            
            EventManager.Instance.combatEvents.onPlayerAttackEnd -= OnBasicAttackEnd;
            
            EventManager.Instance.playerEvents.onDodgeEnd -= OnBasicAttackEnd;
            
            EventManager.Instance.combatEvents.onPlayerComboWindowOpen -= OnPlayerComboWindowOpen;
            EventManager.Instance.playerEvents.onCharacterSwap -= Reset;
            //EventManager.Instance.combatEvents.onPlayerCombo -= NextCombo;
        }


      
        
        
        public void OnPlayerComboWindowOpen()
        {
          
            comboWindowOpen = true;
        }


        public void OnPlayerAttackStart()
        {
            characterCombatState = CharacterCombatState.Attacking;
        }

        public void OnBasicAttackEnd()
        {
            
            characterCombatState = CharacterCombatState.Idle;
        }
        
        private void Reset()
        {
            characterCombatState = CharacterCombatState.Idle;
        }
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }
        
    }
    
    
   
}