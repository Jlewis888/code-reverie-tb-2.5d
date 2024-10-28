using System;
using System.Collections.Generic;
using CodeReverie;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Dialogue Choice", story: "Player choice [Response]", category: "Action", id: "aaf5b4c895b11ab9fa71de8e5cec6b96")]
public partial class ChoiceAction : Action
{
    [SerializeReference] public BlackboardVariable<string> Response;
    [CreateProperty] int index;
    [CreateProperty] public bool selected;
    
    
    protected override Status OnStart()
    {
        
        if (CanvasManager.Instance.dialogueManager.gameObject.activeInHierarchy)
        {
            index = CanvasManager.Instance.dialogueManager.dialogueChoiceButtons.Count;
            CanvasManager.Instance.dialogueManager.AddChoice(Response);
        }
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {

        if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
        {
            (bool, int) choiceSelection = CanvasManager.Instance.dialogueManager.ConfirmChoice();
            
            if (choiceSelection.Item2 == index)
            {
                CanvasManager.Instance.dialogueManager.ChoiceSelected();
                return Status.Success;
            }
            
        }
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

