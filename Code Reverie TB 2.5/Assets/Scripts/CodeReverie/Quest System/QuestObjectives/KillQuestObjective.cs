using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    public class KillQuestObjective : QuestStepObjective
    {
        public List<CharacterDataContainer> killQuestTriggerList = new List<CharacterDataContainer>();

        public KillQuestObjective()
        {
            killQuestTriggerList = killQuestTriggerList.Distinct().ToList();
            questObjectiveType = QuestObjectiveType.Kill;
        }
        
        
        protected override void SubscribeQuestTriggers()
        {
            base.SubscribeQuestTriggers();
            EventManager.Instance.combatEvents.onDeath += KillQuestTrigger;
        }

        protected override void UnsubscribeQuestTriggers()
        {
            base.UnsubscribeQuestTriggers();
            EventManager.Instance.combatEvents.onDeath -= KillQuestTrigger;
        }
        
        public void KillQuestTrigger(string triggerId)
        {
            
            if (!MetRequiredItemsCheck())
            {
                return;
            }
        
            
            if (killQuestTriggerList.Any(x => x.id == triggerId))
            {
                CurrentCount++;
            }
                
            if (CurrentCount >= requiredAmount)
            {
                ObjectiveStatus = QuestObjectiveStatus.Complete;
            }
        }
        
        
        
    }
}