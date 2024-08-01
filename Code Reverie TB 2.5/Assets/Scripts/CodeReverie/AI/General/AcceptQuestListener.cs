using BehaviorDesigner.Runtime.Tasks;

namespace CodeReverie
{
    public class AcceptQuestListener : GeneralConditional
    {
        public QuestDataContainer questDataContainer;
        protected Quest quest;


        public override void OnStart()
        {
            quest = QuestManager.Instance.GetQuestById(questDataContainer.id);
        }


        public override TaskStatus OnUpdate()
        {
            return quest.Status == QuestStatus.Active ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}