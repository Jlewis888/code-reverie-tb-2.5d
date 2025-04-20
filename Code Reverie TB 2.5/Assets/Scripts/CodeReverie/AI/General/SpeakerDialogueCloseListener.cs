// using BehaviorDesigner.Runtime.Tasks;
// using UnityEngine;
//
// namespace CodeReverie
// {
//     public class SpeakerDialogueCloseListener : GeneralConditional
//     {
//
//         public bool canContinue;
//         
//         public override void OnAwake()
//         {
//             EventManager.Instance.playerEvents.onDialogueEnd += OnDialogueEnd;
//         }
//         
//         public override TaskStatus OnUpdate()
//         {
//
//             if (canContinue)
//             {
//                 canContinue = false;
//                 return TaskStatus.Success;
//             }
//
//             return TaskStatus.Running;
//             
//         }
//
//         public void OnDialogueEnd(DialogueSpeaker speaker)
//         {
//
//             if (gameObject == null)
//             {
//                 EventManager.Instance.playerEvents.onDialogueEnd -= OnDialogueEnd;
//                 return;
//             }
//             
//             if (GetComponent<DialogueSpeaker>())
//             {
//                 if (speaker == GetComponent<DialogueSpeaker>())
//                 {
//                     canContinue = true;
//                 }
//             }
//         }
//         
//     }
// }