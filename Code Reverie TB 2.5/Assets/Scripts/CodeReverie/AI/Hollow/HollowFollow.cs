using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CodeReverie
{
    public class HollowFollow : HollowAction
    {
        public override TaskStatus OnUpdate()
        {
            // float moveSpeed = PlayerController.Instance.GetComponent<PlayerMovementController>().moveSpeed - 0.5f;
            //
            //
            //
            // GameObject followTarget = PlayerController.Instance.hollowFollowPoint.gameObject;
            //
            //
            // if (Vector3.Distance(transform.position, followTarget.transform.position) > 0.1)
            // {
            //     
            //     FaceFollowTarget();
            //     
            //     transform.position = Vector3.MoveTowards(transform.position,
            //         followTarget.transform.position,
            //         moveSpeed * Time.deltaTime);
            //     return TaskStatus.Running;
            // }

            return TaskStatus.Success;

        }
    }
}