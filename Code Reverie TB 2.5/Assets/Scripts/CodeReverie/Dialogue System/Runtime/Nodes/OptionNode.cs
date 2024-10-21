using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [NodeInfo("Option", "", "Process/Option")]
    public class OptionNode : DialogueGraphNode
    {

        [ExposedProperty]
        public string optionText;
        
        public override List<string> OnProcess(DialogueGraphAsset dialogueGraphAsset)
        {
            Debug.Log("Moved to Option Node");
            Debug.Log(optionText);

            return base.OnProcess(dialogueGraphAsset);
        }
    }
}