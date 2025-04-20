// using BehaviorDesigner.Runtime.Tasks;
// using UnityEngine;
//
// namespace CodeReverie
// {
//     public class PlayAnimation : GeneralAction
//     {
//         
//         public string animationName;
//         
//         public override void OnStart()
//         {
//             GetComponent<AnimationManager>().ChangeAnimationState(animationName);
//         }
//         
//         
//         public override TaskStatus OnUpdate()
//         {
//             
//             
//             // if (Vector3.Distance(transform.position, enemyPathfinding.targetPosition) > 0.01f)
//             // {
//             //     return TaskStatus.Running;
//             // }
//
//             return TaskStatus.Success;
//         }
//
//     }
// }