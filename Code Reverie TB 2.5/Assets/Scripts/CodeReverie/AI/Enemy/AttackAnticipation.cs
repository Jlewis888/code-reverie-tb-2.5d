// using BehaviorDesigner.Runtime.Tasks;
// using UnityEngine;
//
// namespace CodeReverie
// {
//     public class AttackAnticipation : EnemyAction
//     {
//
//         public string animationName;
//         public float anticipationWaitTime;
//         public bool randomWait;
//         public float anticipationWaitTimeMin;
//         public float anticipationWaitTimeMax;
//         
//         
//         
//         
//         private float anticipationTimer;
//
//         public override void OnStart()
//         {
//             //GetComponent<AnimationManager>().ChangeAnimationState(animationName);
//
//             if (randomWait)
//             {
//                 anticipationTimer = Random.Range(anticipationWaitTimeMin, anticipationWaitTimeMax);
//             }
//             else
//             {
//                 anticipationTimer = anticipationWaitTime;
//             }
//         }
//
//         public override TaskStatus OnUpdate()
//         {
//
//             anticipationTimer -= Time.deltaTime;
//             
//             if (anticipationTimer <= 0)
//             {
//                 return TaskStatus.Success;
//             }
//             
//             FaceTarget();
//             
//             return TaskStatus.Running;
//         }
//         
//     }
// }