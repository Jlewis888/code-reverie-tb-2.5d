using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace CodeReverie
{
    public abstract class QuestObjectiveBak
    {

        protected bool isComplete;
        public string questObjectiveDescription;
        public List<string> objectiveIds;
        public QuestObjectiveType questObjectiveType;
        public QuestStatus questStatus;
        public int currentCount = 0;
        public int requiredAmount;

    }
}