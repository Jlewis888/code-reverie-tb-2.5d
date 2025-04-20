// using BehaviorDesigner.Runtime.Tasks;
// using UnityEngine;
//
// namespace CodeReverie
// {
//     [TaskName("Check If Player Is In Attack Range")]
//     public class CheckIfPlayerInAttackRange : EnemyConditional
//     {
//         public override TaskStatus OnUpdate()
//         {
//             
//             Collider2D[] targetsDetected = Physics2D.OverlapCircleAll(transform.position, 3f);
//
//             foreach (Collider2D targets in targetsDetected)
//             {
//                 
//                 if (targets.TryGetComponent(out ComponentTagManager componentTagManager))
//                 {
//
//                     if (componentTagManager.HasTag(ComponentTag.Player))
//                     {
//                         return TaskStatus.Success;
//                     }
//                     
//                    
//                 }
//             }
//             
//             
//             
//             return TaskStatus.Failure;
//         }
//     }
// }