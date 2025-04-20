// using BehaviorDesigner.Runtime.Tasks;
// using UnityEngine;
//
// namespace CodeReverie
// {
//     public class EnemyRoamSetMoveAction : EnemyAction
//     {
//         public override TaskStatus OnUpdate()
//         {
//             
//             //Debug.Log(enemyAI.GetRoamingPosition());
//
//             enemyPathfinding.MoveTo(enemyAI.GetRoamingPosition());
//
//
//             
//
//
//             return TaskStatus.Success;
//         }
//
//
//         public override void OnLateUpdate()
//         {
//             Debug.Log("Test");
//         }
//     }
// }