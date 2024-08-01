using BehaviorDesigner.Runtime.Tasks;

namespace CodeReverie
{
    public class EnemyConditional : Conditional
    {
        protected Health health;

        public override void OnAwake()
        {
            health = GetComponent<Health>();
        }

    }
}