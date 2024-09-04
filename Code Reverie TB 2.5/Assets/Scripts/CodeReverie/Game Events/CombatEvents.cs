using System;
using UnityEngine;

namespace CodeReverie
{
    public class CombatEvents
    {
        
        public Action onCombatEnter;
        
        public void OnCombatEnter()
        {
            onCombatEnter?.Invoke();
        }
        
        public Action<bool> onCombatPause;
        
        public void OnCombatPause(bool isPaused)
        {
            onCombatPause?.Invoke(isPaused);
        }
        
        public Action<CharacterBattleManager> onPlayerTurn;
        
        public void OnPlayerTurn(CharacterBattleManager characterBattleManager)
        {
            onPlayerTurn?.Invoke(characterBattleManager);
        }
        
        
        public Action onActionSelected;
        
        public void OnActionSelected()
        {
            onActionSelected?.Invoke();
        }
        
        

        public Action onPlayerAttackStart;

        public void OnPlayerAttackStart()
        {
            onPlayerAttackStart?.Invoke();
        }
        
        public Action<CharacterBattleManager> onAttackEnd;

        public void OnAttackEnd(CharacterBattleManager characterBattleManager)
        {
            onAttackEnd?.Invoke(characterBattleManager);
        }
        
        
        public Action onPlayerAttackEnd;

        public void OnPlayerAttackEnd()
        {
            onPlayerAttackEnd?.Invoke();
        }
        
        public Action onPlayerVictory;
        
        public void OnPlayerVictory()
        {
            onPlayerVictory?.Invoke();
        }
        
        
        public Action onPlayerDefeat;
        
        public void OnPlayerDefeat()
        {
            onPlayerDefeat?.Invoke();
        }
        
        
        public Action<CharacterBattleManager> onPlayerSelectTarget;

        public void OnPlayerSelectTarget(CharacterBattleManager characterBattleManager)
        {
            onPlayerSelectTarget?.Invoke(characterBattleManager);
        }
        
        
        public Action onCombatAnimationEnd;

        public void OnCombatAnimationEnd()
        {
            onCombatAnimationEnd?.Invoke();
        }
        
        
        
        public Action onPlayerBasicSkillTrigger;

        public void OnPlayerBasicSkillTrigger()
        {
            onPlayerBasicSkillTrigger?.Invoke();
        }
        
        
        public Action onPlayerCoreSkillTrigger;

        public void OnPlayerCoreSkillTrigger()
        {
            onPlayerCoreSkillTrigger?.Invoke();
        }
        
        
        public Action onPlayerComboWindowOpen;

        public void OnPlayerComboWindowOpen()
        {
            onPlayerComboWindowOpen?.Invoke();
        }
        
        
        public Action onPlayerComboWindowClosed;

        public void OnPlayerComboWindowClosed()
        {
            onPlayerComboWindowClosed?.Invoke();
        }
        
        public Action onPlayerCombo;

        public void OnPlayerCombo()
        {
            onPlayerCombo?.Invoke();
        }
        
        public Action<string> onDeath;

        public void OnDeath(string id)
        {
            onDeath?.Invoke(id);
        }

        public Action<CharacterBattleManager> onCharacterDeath;

        public void OnCharacterDeath(CharacterBattleManager characterBattleManager)
        {
            onCharacterDeath?.Invoke(characterBattleManager);
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.notificationCenter.NotificationTrigger($"{characterBattleManager.GetComponent<CharacterUnitController>().character.info.characterName} defeated");
        }

        public Action<CharacterUnitController> onEnemyDeath;

        public void OnEnemyDeath(CharacterUnitController characterUnit)
        {
            onEnemyDeath?.Invoke(characterUnit);
        }
        
        
        
        public Action<CharacterUnitController> onPlayerDeath;

        public void OnPlayerDeath(CharacterUnitController characterUnit)
        {
            onPlayerDeath?.Invoke(characterUnit);
        }
        

        public Action onActionSkillUse;
        public void OnActionSkillUse()
        {
            onActionSkillUse?.Invoke();
        }
        
        public Action onActionRangeSkillUse;
        public void OnActionRangeSkillUse()
        {
            onActionRangeSkillUse?.Invoke();
        }

        public Action<DamageProfile> onDamage;

        public void OnDamage(DamageProfile damageProfile)
        {
            onDamage?.Invoke(damageProfile);
        }


        public Action onMeleeAttackSkillUse;

        public void OnMeleeAttackSkillUse()
        {
            onMeleeAttackSkillUse?.Invoke();
        }

        public Action<DamageProfile> onPlayerDamageTaken;

        public void OnPlayerDamageTaken(DamageProfile damageProfile)
        {
            onPlayerDamageTaken?.Invoke(damageProfile);
        }
        
        public Action<DamageProfile> onEnemyDamageTaken;

        public void OnEnemyDamageTaken(DamageProfile damageProfile)
        {
            onEnemyDamageTaken?.Invoke(damageProfile);
        }
        
        public Action<CharacterBattleManager> onSkillComplete;

        public void OnSkillComplete(CharacterBattleManager characterBattleManager)
        {
            onSkillComplete?.Invoke(characterBattleManager);
        }

    }
}