using UnityEngine;

namespace CodeReverie
{
    public class SpeakToPersonQuestObjectiveData : QuestStepObjective
    {
        public CharacterDataContainer characterDataContainer;
        
        public SpeakToPersonQuestObjectiveData()
        {
            questObjectiveType = QuestObjectiveType.Meet;
        }
        
        public void SpeakToPersonQuestTrigger(string id)
        {
            
            if (!MetRequiredItemsCheck())
            {
                return;
            }
            
           
            if (questId == id)
            {
               
                CurrentCount++;
                // SpeakToPersonQuestObjectiveData speakToPersonQuestObjective = info as SpeakToPersonQuestObjectiveData;
                //
                //
                // if (speakToPersonQuestObjective.characterDataContainer != null)
                // {
                //     CurrentCount++;
                // }
                
                if (CurrentCount >= requiredAmount)
                {
                    ObjectiveStatus = QuestObjectiveStatus.Complete;
                }
                
            }
        }

        protected override void SubscribeQuestTriggers()
        {
            base.SubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onDialogueChoiceSelection += SpeakToPersonQuestTrigger;
        }

        protected override void UnsubscribeQuestTriggers()
        {
            base.UnsubscribeQuestTriggers();
            EventManager.Instance.playerEvents.onDialogueChoiceSelection -= SpeakToPersonQuestTrigger;
        }
    }
}