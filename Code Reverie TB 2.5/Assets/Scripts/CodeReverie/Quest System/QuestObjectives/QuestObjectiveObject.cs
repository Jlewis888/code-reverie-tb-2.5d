using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace CodeReverie
{
    public abstract class QuestObjectiveObject : SerializedMonoBehaviour
    {

        protected bool isComplete;
        private string questId;
        public string questObjectiveDescription;
        public List<string> objectiveIds;
        public QuestObjectiveType questObjectiveType;
        public QuestStatus questStatus;
        public int currentCount = 0;
        public int requiredAmount;


        public void SetQuestId(string id)
        {
            questId = id;
        }
        
        protected void CompleteQuestObjective()
        {
            if (!isComplete)
            {
                isComplete = true;
                
                EventManager.Instance.questEvents.AdvanceQuest(questId);
                
                Destroy(gameObject);
            }
        }

    }
}