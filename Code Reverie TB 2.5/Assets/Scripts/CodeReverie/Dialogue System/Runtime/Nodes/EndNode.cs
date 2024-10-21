using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [NodeInfo(
        name: "End", 
        category: "Process/End",
        description: "This is a test of sorts", 
        hasFlowOutput:false)]
    public class EndNode : DialogueGraphNode
    {
        
        public override void Execute(DialogueGraphAsset dialogueGraphAsset)
        {
            EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.screenSpaceCanvasManager.hudManager);
            //EventManager.Instance.playerEvents.OnDialogueEnd(speaker);
            
            CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController.transform);
            CameraManager.Instance.ToggleMainCamera();
        }


        public override List<string> OnProcess(DialogueGraphAsset dialogueGraphAsset)
        {
            return base.OnProcess(dialogueGraphAsset);
        }
    }
}