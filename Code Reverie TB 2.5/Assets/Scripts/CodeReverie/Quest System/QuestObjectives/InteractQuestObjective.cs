using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    public class InteractQuestObjective : QuestStepObjective
    {
        public List<ItemInfo> itemInfoList = new List<ItemInfo>();

        public InteractQuestObjective()
        {
            itemInfoList = itemInfoList.Distinct().ToList();
            questObjectiveType = QuestObjectiveType.Interact;
        }
        
        
        protected override void SubscribeQuestTriggers()
        {
            base.SubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onObjectInteracted += ObjectInteracted;
        }

        protected override void UnsubscribeQuestTriggers()
        {
            base.UnsubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onObjectInteracted -= ObjectInteracted;
        }
        
        
        public void ObjectInteracted(string id)
        {
        
            if (!MetRequiredItemsCheck())
            {
                return;
            }
            
            if (id == questStepObjectiveListener.id)
            {
                
                CurrentCount++;
                
                if (CurrentCount >= requiredAmount)
                {
                    ObjectiveStatus = QuestObjectiveStatus.Complete;
                }
            }
        }

    }
}