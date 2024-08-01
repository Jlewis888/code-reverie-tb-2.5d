using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CodeReverie
{
    public class MoveToLocation : GeneralAction
    {
        public GameObject targetLocation;
        public float moveSpeed = 3f;
        
        
        
        public override TaskStatus OnUpdate()
        {
            
            if (Vector3.Distance(transform.position, targetLocation.transform.position) > 0.1)
            {
                
                transform.position = Vector3.MoveTowards(transform.position,
                    targetLocation.transform.position,
                    moveSpeed * Time.deltaTime);
                return TaskStatus.Running;
            }

            return TaskStatus.Success;

        }
    }
}