using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [NodeInfo(
        name: "Choice Node", 
        category: "Process/Choice",
        description: "This is a test of sorts"
        )
    ]
    public class ChoiceNode : DialogueGraphNode
    {
        
        [ExposedProperty] 
        public CharacterDataContainer speaker;
        
        [ExposedProperty]
        public string dialogueText;

        [ExposedProperty]
        public string text;
        
        public override List<string> OnProcess(DialogueGraphAsset dialogueGraphAsset)
        {
            Debug.Log("Moved to Option Node");
            Debug.Log(text);

            return base.OnProcess(dialogueGraphAsset);
        }

        public override void Execute(DialogueGraphAsset dialogueGraphAsset)
        {
            
            CanvasManager.Instance.dialogueManager.Dialogue(speaker, dialogueText);
            
        }
    }
}