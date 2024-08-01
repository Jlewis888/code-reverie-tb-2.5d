using BehaviorDesigner.Runtime.Tasks;
using Rewired;
using UnityEngine;

namespace CodeReverie
{
    public class CheckIfPlayerInRange : EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            Collider2D[] targetsDetected = Physics2D.OverlapCircleAll(transform.position, 10);

            foreach (Collider2D targets in targetsDetected)
            {
                
                
                
                if (targets.TryGetComponent(out ComponentTagManager componentTagManager))
                {

                    if (componentTagManager.HasTag(ComponentTag.Player))
                    {
                        return TaskStatus.Success;
                    }
                    
                   
                }
            }
            
            

            return TaskStatus.Failure;
        }
    }
}