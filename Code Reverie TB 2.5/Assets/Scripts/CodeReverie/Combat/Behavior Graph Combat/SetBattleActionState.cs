using System;
using CodeReverie;
using FullscreenEditor;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetBattleActionState", story: "Set Character Battle Action State to [characterBattleActionState]", category: "Action/Character Battle Action", id: "120731b93a275f54023c49f6fe66a2e8")]
public partial class SetBattleActionState : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<CharacterBattleActionState> characterBattleActionState;

    protected override Status OnStart()
    {
       
        if (Self == null)
        {
            return Status.Failure;
        }
        
        Self.Value.GetComponent<CharacterBattleManager>().characterBattleActionState = characterBattleActionState;
        
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

