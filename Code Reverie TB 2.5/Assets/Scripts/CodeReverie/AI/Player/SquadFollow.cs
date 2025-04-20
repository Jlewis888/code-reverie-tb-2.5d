// using BehaviorDesigner.Runtime.Tasks;
// using Unity.VisualScripting;
// using UnityEngine;
//
// namespace CodeReverie
// {
//     public class SquadFollow : SquadAction
//     {
//         // public override TaskStatus OnUpdate()
//         // {
//         //     float moveSpeed = PlayerManager.Instance.currentParty[0].characterUnit.GetComponent<PlayerMovementController>().moveSpeed - 0.5f;
//         //     //float moveSpeed = 5;
//         //
//         //
//         //     GameObject followTarget;
//         //     
//         //     if (playerCharacterUnit == PlayerManager.Instance.currentParty[1].characterUnit)
//         //     {
//         //         followTarget = PlayerManager.Instance.currentParty[0].characterUnit.gameObject;
//         //         
//         //     }
//         //     else
//         //     {
//         //         followTarget = PlayerManager.Instance.currentParty[1].characterUnit.gameObject;
//         //     }
//         //     
//         //     
//         //     if (Vector3.Distance(transform.position, followTarget.transform.position) > 2)
//         //     {
//         //         
//         //         FaceFollowTarget();
//         //         
//         //         transform.position = Vector3.MoveTowards(transform.position,
//         //             followTarget.transform.position,
//         //             moveSpeed * Time.deltaTime);
//         //         return TaskStatus.Running;
//         //     }
//         //
//         //     return TaskStatus.Success;
//         //
//         // }
//     }
// }