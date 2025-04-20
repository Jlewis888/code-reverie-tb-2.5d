// using BehaviorDesigner.Runtime.Tasks;
// using UnityEngine;
//
// namespace CodeReverie
// {
//     public class EnemyAction : Action
//     {
//         protected EnemyAI enemyAI;
//         protected EnemyPathfinding enemyPathfinding;
//
//
//         public override void OnAwake()
//         {
//             enemyPathfinding = GetComponent<EnemyPathfinding>();
//             enemyAI = GetComponent<EnemyAI>();
//         }
//         
//         public void FaceTarget()
//         {
//             if (transform.position.x > PlayerManager.Instance.currentParty[0].characterController.transform.position.x)
//             {
//                 GetComponent<CharacterUnitController>().characterUnit.spriteRenderer.flipX = true;
//                 //transform.localScale =  new Vector3(-1,1,1);
//                 //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
//             }
//             else
//             {
//                 //transform.localScale = new Vector3(1,1,1);
//                 //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1f, transform.localScale.y, transform.localScale.z);
//                 GetComponent<CharacterUnitController>().characterUnit.spriteRenderer.flipX = false;
//             }
//         }
//         
//         
//     }
// }