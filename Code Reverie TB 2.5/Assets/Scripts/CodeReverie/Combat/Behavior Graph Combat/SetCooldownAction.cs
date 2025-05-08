using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Cooldown", story: "Set Cooldown timer to [Cooldown]", category: "Action", id: "3b032976eda5c3b225b6c49e80b3e042")]
public partial class SetCooldownAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Cooldown;

    protected override Status OnStart()
    {

        Self.Value.GetComponent<CharacterBattleManager>().cooldownTimer = Cooldown.Value;
        
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

