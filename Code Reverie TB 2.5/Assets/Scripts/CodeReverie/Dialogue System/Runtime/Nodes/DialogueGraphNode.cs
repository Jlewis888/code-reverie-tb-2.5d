using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeReverie
{
    [Serializable]
    public class DialogueGraphNode
    {
        public string GUID;
        public string id => GUID;
        
        public bool EntyPoint = false;
        //public string DialogueText;
        public string typeName;
        private Rect _position;
        public Rect position => _position;

        public DialogueGraphNode()
        {
            NewGUID();
        }

        void NewGUID()
        {
            GUID = Guid.NewGuid().ToString();
        }

        public void SetPosition(Rect position)
        {
            _position = position;
        }
        
        // public virtual string OnProcess(DialogueGraphAsset dialogueGraphAsset)
        // {
        //
        //     DialogueGraphNode nextNode = dialogueGraphAsset.GetNodeFromOutput(GUID, 0);
        //
        //     if (nextNode != null)
        //     {
        //         return nextNode.id;
        //     }
        //     
        //     return string.Empty;
        // }

        public virtual void Execute(DialogueGraphAsset dialogueGraphAsset)
        {
            
        }
        
        public virtual List<string> OnProcess(DialogueGraphAsset dialogueGraphAsset)
        {

            List<string> nodeIdList = new List<string>();

            List<DialogueGraphNode> nextDialogueGraphNodes = dialogueGraphAsset.GetNodesFromOutput(GUID, 0);

            nodeIdList = nextDialogueGraphNodes.Where(x => x != null).Select(x => x.id).ToList();

            return nodeIdList;
        }
        
        public virtual List<DialogueGraphNode> GetConnectedOutputNodes(DialogueGraphAsset dialogueGraphAsset)
        {

            List<string> nodeIdList = new List<string>();

            List<DialogueGraphNode> nextDialogueGraphNodes = dialogueGraphAsset.GetNodesFromOutput(GUID, 0);

            nodeIdList = nextDialogueGraphNodes.Where(x => x != null).Select(x => x.id).ToList();

            return nextDialogueGraphNodes;
        }
        
    }
}