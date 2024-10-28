using System;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Dialogue Branch", story: "Repeating Branch [Repeat]", category: "Flow", id: "9117d8f759f4c092178509956a9b5ced")]
public partial class DialogueBranchSequence : Composite
{
    [SerializeReference] public BlackboardVariable<bool> Repeat;
    /// <summary>
    /// If true, the graph will restart when all nodes completes.
    /// </summary>
    [CreateProperty] int m_CurrentChild;

    protected override Status OnStart()
    {
        
        
        m_CurrentChild = 0;

        if (Children.Count == 0)
        {
            return Status.Success;
        }
        

        foreach (Node child in Children)
        {
            StartNode(child);
        }
        
        CanvasManager.Instance.dialogueManager.DisplayChoices();
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {


        if (Repeat)
        {
            foreach (Node child in Children)
            {
                if (child.CurrentStatus == Status.Success)
                {
                    StartNode(this);
                    return Status.Running;
                }
            }
            
        }
        
        return Status.Waiting;

    }

    protected override void OnEnd()
    {
    }
}

