using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace CodeReverie
{
    public class EndSkillTimelineReceiver : SerializedMonoBehaviour, INotificationReceiver
    {
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            EndSkillTimelineMarker marker = notification as EndSkillTimelineMarker;

            if (marker == null)
            {
                return;
            }
            
            CharacterBattleManager characterBattleManager = CombatManager.Instance.selectedSkillPlayerCharacter;
            
            EventManager.Instance.combatEvents.OnSkillComplete(characterBattleManager);
            CombatManager.Instance.combatManagerState = CombatManagerState.OnSkillUseEnd;
            
            Destroy(gameObject);
        }
    }
}