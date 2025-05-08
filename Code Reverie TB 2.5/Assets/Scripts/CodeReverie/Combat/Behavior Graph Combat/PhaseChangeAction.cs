using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Phase Change", story: "Change [Phase] to [NewPhase]", category: "Action", id: "7a47f964b4855dc02782aa9678c21c17")]
public partial class PhaseChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Phase;
    [SerializeReference] public BlackboardVariable<int> NewPhase;

    protected override Status OnStart()
    {
        Phase.Value = NewPhase.Value;
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

