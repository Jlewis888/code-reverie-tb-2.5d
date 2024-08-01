using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "CraftItemObjectiveData", menuName = "Scriptable Objects/Quest/CraftItemQuestObjectiveData", order = 1)]
    public class CraftItemQuestObjective : QuestStepObjective
    {
        public List<ItemInfo> itemInfoList = new List<ItemInfo>();

        public CraftItemQuestObjective()
        {
            itemInfoList = itemInfoList.Distinct().ToList();
            questObjectiveType = QuestObjectiveType.Craft;
        }
        
        
        protected override void SubscribeQuestTriggers()
        {
            base.SubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onItemCrafted += ItemCraftTrigger;
        }

        protected override void UnsubscribeQuestTriggers()
        {
            base.UnsubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onItemCrafted -= ItemCraftTrigger;
        }
        
        
        public void ItemCraftTrigger(string id)
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