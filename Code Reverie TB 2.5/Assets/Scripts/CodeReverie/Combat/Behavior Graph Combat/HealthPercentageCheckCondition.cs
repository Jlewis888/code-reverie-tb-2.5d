using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Health Percentage Check", story: "[Self] Health is [Operator] [Threshold]", category: "Conditions", id: "4e1ebf44109640d1271bca760daf6b21")]
public partial class HealthPercentageCheckCondition : Condition
{
    
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
    [SerializeReference] public BlackboardVariable<float> Threshold;

    public override bool IsTrue()
    {
        // if (Self.Value.GetComponent<Health>().CurrentHealth / Self.Value.GetComponent<Health>().MaxHealth > Threshold)
        // {
        //     return false;
        // }

        float healthPercentage = Self.Value.GetComponent<Health>().CurrentHealth /
                                 Self.Value.GetComponent<Health>().MaxHealth;
        
        // Debug.Log($"Health Percentage: {healthPercentage}");
        // Debug.Log($"Threshold: {Threshold}");
        
        return ConditionUtils.Evaluate(healthPercentage, Operator, Threshold);
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
