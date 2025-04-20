using System;
using System.Collections.Generic;
using System.Linq;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SelectNewAction", story: "Select Character Next Battle Action State", category: "Action", id: "bb692f9dfca2038e574a28ecbceadc36")]
public partial class SelectCharacterNextBattleActionState : Action
{

    [SerializeReference] public BlackboardVariable<GameObject> self;
    
    protected override Status OnStart()
    {
        
        List<CharacterBattleActionState> combatAbilityChoices = Enum.GetValues(typeof(CharacterBattleActionState))
            .Cast<CharacterBattleActionState>().ToList();
        
        combatAbilityChoices.Remove(CharacterBattleActionState.Idle);
        combatAbilityChoices.Remove(CharacterBattleActionState.Move);

        int randomNum = 0;
        self.Value.GetComponent<CharacterBattleManager>().characterBattleActionState = combatAbilityChoices[Random.Range(0, combatAbilityChoices.Count)];
        
        
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

