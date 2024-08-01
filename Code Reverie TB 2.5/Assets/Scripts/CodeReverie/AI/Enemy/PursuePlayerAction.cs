using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CodeReverie
{
    public class PursuePlayerAction : EnemyAction
    {
        private Vector3 direction;
        
        public override TaskStatus OnUpdate()
        {
            
            
            enemyPathfinding.SetTargetPosition(PlayerManager.Instance.GetCurrentCharacterPosition());
            direction = (Vector3)enemyPathfinding.targetPosition - transform.position;
            if (Vector3.Distance(transform.position, enemyPathfinding.targetPosition) > 0.01f)
            {
                return TaskStatus.Running;
            }

            return TaskStatus.Success;
        }


        public override void OnFixedUpdate()
        {
            float deltaSpeed = enemyPathfinding.activeMoveSpeed * Time.fixedDeltaTime;
            GetComponent<Rigidbody2D>().MovePosition(transform.position + direction * deltaSpeed );
            //transform.position = Vector2.Lerp(transform.position, enemyPathfinding.targetPosition, enemyPathfinding.activeMoveSpeed * Time.deltaTime);
        }
    }
}