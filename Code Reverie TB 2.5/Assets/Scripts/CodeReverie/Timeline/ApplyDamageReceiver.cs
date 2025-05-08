using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace CodeReverie
{
    public class ApplyDamageReceiver : SerializedMonoBehaviour, INotificationReceiver
    {
        public GameObject particleObjectPF;
        private GameObject particleObject;
        
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            ApplyDamageMarker applyDamageMarker = notification as ApplyDamageMarker;

            if (applyDamageMarker == null)
            {
                return;
            }
            
            CharacterBattleManager characterBattleManager = CombatManager.Instance.selectedSkillPlayerCharacter;
            
            
            // List<DamageTypes> damageTypes = new List<DamageTypes>();
            // damageTypes.Add(DamageTypes.Fire);

            foreach (CharacterBattleManager target in characterBattleManager.targetList)
            {
                if (particleObjectPF != null)
                {
                    particleObject = Instantiate(particleObjectPF, target.transform);
                }
                DamageProfile damage = new DamageProfile(characterBattleManager, target, characterBattleManager.selectedSkill);
                Destroy(particleObject, 10f);
            }
            
            // // var exp = Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);
            // // Destroy(exp, DestroyExplosion);
            // EventManager.Instance.combatEvents.OnSkillComplete(characterBattleManager);
            // CombatManager.Instance.combatManagerState = CombatManagerState.OnSkillUseEnd;
            // //characterBattleManager.EndTurn();
            //
            // //Destroy(gameObject);
        }
    }
}