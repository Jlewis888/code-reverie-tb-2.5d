using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "End Turn Reset", story: "End Turn Reset", category: "Action", id: "0c586cd9ea3f3839b749a2200f96ace6")]
public partial class EndTurnResetAction : Action
{
    
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    

    protected override Status OnStart()
    {
        
        if (Self == null)
        {
            return Status.Failure;
        }
        
        Self.Value.GetComponent<CharacterBattleManager>().EndTurnReset();

        
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

