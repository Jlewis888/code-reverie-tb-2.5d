using CodeReverie;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Dialogue", story: "[Speaker] says [Sentence]", category: "Action", id: "21833f7daba357a22b1d73050140eace")]
public partial class DialogueAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterDataContainer> Speaker;
    [SerializeReference] public BlackboardVariable<string> Sentence;
    protected override Status OnStart()
    {
        
        if (CanvasManager.Instance.dialogueManager.gameObject.activeInHierarchy)
        {
            CanvasManager.Instance.dialogueManager.Dialogue(Speaker, Sentence);
        }
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
    
        if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
        {
            CanvasManager.Instance.dialogueManager.CompleteDialogueText();
            CanvasManager.Instance.dialogueManager.Continue();
        }

        return CanvasManager.Instance.dialogueManager.continueDialogue ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
       
    }
}

