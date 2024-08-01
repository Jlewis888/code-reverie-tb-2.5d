using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    public class GatherQuestObjective : QuestStepObjective
    {
        public List<ItemInfo> itemInfoList = new List<ItemInfo>();


        public GatherQuestObjective()
        {
            itemInfoList = itemInfoList.Distinct().ToList();
            questObjectiveType = QuestObjectiveType.Gather;
        }
        

        protected override void SubscribeQuestTriggers()
        {
          
            base.SubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onItemPickup += ItemPickupTrigger;
        }

        protected override void UnsubscribeQuestTriggers()
        {
            base.UnsubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onItemPickup -= ItemPickupTrigger;
        }
        
        
        public void ItemPickupTrigger(string id, int count)
        {
            
            
            if (!MetRequiredItemsCheck())
            {
                return;
            }
            
            if (itemInfoList.Any(x => x.id == id))
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