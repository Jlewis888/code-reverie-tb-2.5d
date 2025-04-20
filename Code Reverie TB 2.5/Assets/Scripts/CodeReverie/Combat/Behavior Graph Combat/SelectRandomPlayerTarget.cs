using System;
using CodeReverie;
using FullscreenEditor;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Select Random Player Target", story: "Select Random Player Target", category: "Action/Character Battle Action", id: "6f98b621d3850a7838a2edfc67f38bbf")]
public partial class SelectRandomPlayerTarget : Action
{

    [SerializeReference] public BlackboardVariable<GameObject> Self;
    //[SerializeReference] public BlackboardVariable<CharacterBattleManager> characterBattleManager;
    [SerializeReference] public BlackboardVariable<CharacterBattleActionState> characterBattleActionState;

    
    protected override Status OnStart()
    {

        
        CharacterBattleManager characterBattleManager = Self.Value.GetComponent<CharacterBattleManager>();
        
        int randomNum = Random.Range(0, CombatManager.Instance.playerUnits.Count);

        if (characterBattleManager == null)
        {
            return Status.Failure;
        }

        //ReferenceEquals(characterBattleManager?.Value, null);
        
        characterBattleManager.target = CombatManager.Instance.playerUnits[randomNum];
        characterBattleManager.SetSkillCast();
        characterBattleManager.SetAttackActionTargetPosition();
        
        
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

