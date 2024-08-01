using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CharacterMecanimEvents : SerializedMonoBehaviour
    {
        public void CanAttackFalse()
        {
          
        }

        public void OnBasicAttackEnd()
        {
            EventManager.Instance.combatEvents.OnAttackEnd(GetComponent<CharacterBattleManager>());
        }
        
        
        public void OnCombatAnimationEnd()
        {
            EventManager.Instance.combatEvents.OnCombatAnimationEnd();
        }

        public void TriggerSkill()
        {
            //Debug.Log("this hits here");
            //EventManager.Instance.combatEvents.OnPlayerBasicSkillTrigger();
        }
        
        public void TriggerCoreSkill()
        {
            EventManager.Instance.combatEvents.OnPlayerCoreSkillTrigger();
        }
        
        

        public void ComboWindowOpen()
        {
           
            EventManager.Instance.combatEvents.OnPlayerComboWindowOpen();
        }

        public void NextCombo()
        {
            EventManager.Instance.combatEvents.OnPlayerCombo();
            // if (PlayerController.Instance.playerCombatController.continueAttack)
            // {
            //     EventManager.Instance.combatEvents.OnPlayerCombo();
            // } 
        }

        public void ClearAnimation()
        {
            
        }


        public void PlaySoundOneShot(string clip)
        {
            SoundManager.Instance.PlayOneShotSound(clip);
        }

        public void TestTest()
        {
            Debug.Log("This is an ANimation test");
        }
        
    }
}