// using System.Collections;
// using System.Collections.Generic;
// using BehaviorDesigner.Runtime;
// using BehaviorDesigner.Runtime.Tasks;
// using UnityEngine;
//
// namespace CodeReverie
// {
//     public class SetChargeDirection : EnemyAction
//     {
//         public SharedVector3 sharedTargetPosition;
//         public float chargeWaitTime;
//         public float anticipationWaitTime;
//         public bool randomWait;
//         public float anticipationWaitTimeMin;
//         public float anticipationWaitTimeMax;
//
//         private float anticipationTimer;
//         public float chargeWaitTimer;
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
//            
//             
//             chargeWaitTimer = chargeWaitTime;
//             enemyAI.attackIndicatorsManager.attackIndicatorSquare.gameObject.SetActive(true);
//             //enemyAI.attackIndicatorsManager.attackIndicatorSquare.target = PlayerController.Instance.gameObject;
//         }
//
//         public override TaskStatus OnUpdate()
//         {
//
//             anticipationTimer -= Time.deltaTime;
//             chargeWaitTimer -= Time.deltaTime;
//
//             // if (chargeWaitTimer > 0)
//             // {
//             //     
//             // }
//             //
//             
//             if (anticipationTimer <= 0)
//             {
//                 enemyAI.attackIndicatorsManager.attackIndicatorSquare.target = null;
//                 
//                 return TaskStatus.Success;
//             }
//             
//             FaceTarget();
//             sharedTargetPosition.Value = PlayerManager.Instance.GetCurrentCharacterPosition();
//             
//             return TaskStatus.Running;
//         }
//     }
// }