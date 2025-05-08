using CodeReverie;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Selected Skill", story: "Set Selected Skill [Skill]", category: "Action", id: "0a6a260873fce8657ca8b42d57e792ef")]
public partial class SetSelectedSkillAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<SkillDataContainer> Skill;

    protected override Status OnStart()
    {
        Skill skill = SkillsManager.Instance.CreateSkill(Skill.Value);

        Self.Value.GetComponent<CharacterBattleManager>().selectedSkill = skill;
        
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

