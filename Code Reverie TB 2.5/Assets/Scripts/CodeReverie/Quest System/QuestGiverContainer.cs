namespace CodeReverie
{

    public enum QuestGiverContainerType
    {
        Start,
        Complete
    }
    
    public class QuestGiverContainer
    {
        public QuestDataContainer questDataContainer;
        public QuestStepDataContainer questStepDataContainer;
        public bool startQuest;
    }
}