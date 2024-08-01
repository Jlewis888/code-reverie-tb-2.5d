using BehaviorDesigner.Runtime.Tasks;

namespace CodeReverie
{
    public class HollowFollowCheck : HollowConditional
    {
        public override TaskStatus OnUpdate()
        {
            return hollowAIController.hollowControllerState == HollowControllerState.Follow
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}