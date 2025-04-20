using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Character Action Gauge State", story: "Set Character Action Gauge to [characterActionGaugeState]", category: "Action", id: "33aab67e650546d0a1811423875f7848")]
public partial class SetCharacterActionGaugeState : Action
{
    
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<CharacterActionGaugeState> characterActionGaugeState;

    protected override Status OnStart()
    {
        
        if (Self == null)
        {
            return Status.Failure;
        }
        
        
        Self.Value.GetComponent<CharacterBattleManager>().characterActionGaugeState = characterActionGaugeState;

        
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

