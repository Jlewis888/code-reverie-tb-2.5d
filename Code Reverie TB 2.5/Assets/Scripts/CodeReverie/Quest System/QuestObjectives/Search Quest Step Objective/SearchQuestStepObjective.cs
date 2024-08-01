using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;

namespace CodeReverie
{
    public class SearchQuestStepObjective : QuestStepObjective
    {
        
        public List<CharacterDataContainer> characterQuestTriggerList = new List<CharacterDataContainer>();
        public SearchQuestType searchQuestType;
        
        public SearchQuestStepObjective()
        {
            ObjectiveStatus = QuestObjectiveStatus.Inactive;
            questObjectiveType = QuestObjectiveType.Search;
        }
        
        // public SearchQuestStepObjective(QuestStepObjectiveData questObjectiveData)
        // {
        //     this.info = questObjectiveData;
        //     ObjectiveStatus = QuestObjectiveStatus.Inactive;
        // }
        
        
        public void SearchQuestTrigger(string id)
        {
            
            Debug.Log("this hits The Search");
            
            if (!MetRequiredItemsCheck())
            {
                return;
            }
            
            if (questObjectiveStatus == QuestObjectiveStatus.Active)
            {
                
                if (questStepObjectiveListener.id == id)
                {
                    CurrentCount++;
                }
            
                if (CurrentCount >= requiredAmount)
                {
                    ObjectiveStatus = QuestObjectiveStatus.Complete;
                } 
            }
            
        }


       


        protected override void SubscribeQuestTriggers()
        {
            base.SubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onSearch += SearchQuestTrigger;
        }
        
        protected override void UnsubscribeQuestTriggers()
        {
            base.UnsubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onSearch -= SearchQuestTrigger;
            
        }

       
        
    }
}