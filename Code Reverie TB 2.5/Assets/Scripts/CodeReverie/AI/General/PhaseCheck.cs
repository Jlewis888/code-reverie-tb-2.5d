// using BehaviorDesigner.Runtime;
// using BehaviorDesigner.Runtime.Tasks;
//
// namespace CodeReverie
// {
//     public class PhaseCheck : EnemyConditional
//     {
//         public SharedInt currentPhase;
//         public int phase;
//         
//         
//         public override TaskStatus OnUpdate()
//         {
//             return currentPhase.Value == phase ? TaskStatus.Success : TaskStatus.Failure;
//         }
//     }
// }