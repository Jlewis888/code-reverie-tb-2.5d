using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CodeReverie
{
    public class EnemyRoamAction : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            
           
            if (Vector3.Distance(transform.position, enemyPathfinding.targetPosition) > 0.01f)
            {
                return TaskStatus.Running;
            }


            return TaskStatus.Success;
        }
        
        public override void OnFixedUpdate()
        {
            transform.position = Vector2.Lerp(transform.position, enemyPathfinding.targetPosition, enemyPathfinding.activeMoveSpeed * Time.deltaTime);
            //enemyPathfinding.rb.MovePosition(enemyPathfinding.rb.position + enemyPathfinding.moveInput * (activeMoveSpeed * Time.fixedDeltaTime));
        }
    }
}