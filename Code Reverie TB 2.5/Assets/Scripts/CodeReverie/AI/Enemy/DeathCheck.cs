using BehaviorDesigner.Runtime.Tasks;

namespace CodeReverie
{
    public class DeathCheck : EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            return health.CurrentHealth <= 0 ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}