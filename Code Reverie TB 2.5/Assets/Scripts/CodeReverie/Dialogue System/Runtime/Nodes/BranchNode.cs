using System.Collections.Generic;

namespace CodeReverie
{
    [NodeInfo(
        name: "Branch Node",
        category: "Process/Branch Bode",
        description: "",
        hasMultiOutput: true
        )
    ]
    public class BranchNode : DialogueGraphNode
    {
        public List<ChoiceNode> choiceNodes = new List<ChoiceNode>();


        public override void Execute(DialogueGraphAsset dialogueGraphAsset)
        {
            List<DialogueGraphNode> graphNodes = GetConnectedOutputNodes(dialogueGraphAsset);
            
            SetChoiceNodes(graphNodes);
            
            CanvasManager.Instance.dialogueManager.DisplayChoices(this);
            
        }
        
        public void SetChoiceNodes(List<DialogueGraphNode> dialogueGraphNodes)
        {
            foreach (DialogueGraphNode dialogueGraphNode in dialogueGraphNodes)
            {
                if (dialogueGraphNode is ChoiceNode)
                {
                    choiceNodes.Add(dialogueGraphNode as ChoiceNode);
                }
            }
        }
    }
}