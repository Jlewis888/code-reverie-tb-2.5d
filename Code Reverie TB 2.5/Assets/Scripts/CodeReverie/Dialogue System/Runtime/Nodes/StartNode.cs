using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [NodeInfo("Start", "", "Process/Start", false, true)]
    public class StartNode : DialogueGraphNode
    {
        [ExposedProperty] 
        public CharacterDataContainer speaker;
        
        [ExposedProperty]
        public string dialogueText;


        public override void Execute(DialogueGraphAsset dialogueGraphAsset)
        {
            CanvasManager.Instance.dialogueManager.Dialogue(speaker, dialogueText);
        }


        public override List<string> OnProcess(DialogueGraphAsset dialogueGraphAsset)
        {
            Debug.Log(dialogueText);

            return base.OnProcess(dialogueGraphAsset);
        }
    }
}