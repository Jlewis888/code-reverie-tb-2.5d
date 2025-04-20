using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Character Battle State", story: "Set Character Battle State to [characterBattleState]", category: "Action", id: "e3b39c0320c85046a1e2c442227f23a8")]
public partial class SetCharacterBattleState : Action
{

    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<CharacterBattleState> characterBattleState;
    
    protected override Status OnStart()
    {
        
        if (Self == null)
        {
            return Status.Failure;
        }
        
        
        Self.Value.GetComponent<CharacterBattleManager>().battleState = characterBattleState;
        
        
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

