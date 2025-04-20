using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Change Animation State", story: "Change Animation State to [animation]", category: "Action", id: "076d555bdc5a8a6f58705fe7777b8d85")]
public partial class ChangeAnimationState : Action
{
    
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<string> animation;

    protected override Status OnStart()
    {
        if (Self == null)
        {
            return Status.Failure;
        }
        
        Self.Value.GetComponent<AnimationManager>().ChangeAnimationState(animation);
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

