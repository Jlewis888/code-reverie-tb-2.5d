using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "Dialogue Graph", menuName = "Dialogue/Dialogue Graph")]
    public class DialogueGraphAsset : ScriptableObject
    {
        // [SerializeReference] 
        // public BlackboardAsset Blackboard;

        [SerializeReference] 
        private List<DialogueGraphNode> dialogueNodes = new List<DialogueGraphNode>();

        [SerializeReference] 
        private List<DialogueGraphConnection> connections;

        private Dictionary<string, DialogueGraphNode> nodeDictionary;
        
        
        public List<DialogueGraphNode> DialogueNodes => dialogueNodes;
        public List<DialogueGraphConnection> Connections => connections;

        public GameObject gameObject;

        public DialogueGraphAsset()
        {
            dialogueNodes = new List<DialogueGraphNode>();
            connections = new List<DialogueGraphConnection>();
        }

        public void Init(GameObject gameObject)
        {
            this.gameObject = gameObject;
            nodeDictionary = new Dictionary<string, DialogueGraphNode>();

            foreach (DialogueGraphNode dialogueGraphNode in DialogueNodes)
            {
                nodeDictionary.Add(dialogueGraphNode.id, dialogueGraphNode);
            }
        }

        public DialogueGraphNode GetStartNode()
        {
            StartNode[] startNodes = DialogueNodes.OfType<StartNode>().ToArray();

            if (startNodes.Length == 0)
            {
                return null;
            }

            return startNodes[0];
        }

        public DialogueGraphNode GetNode(string nodeId)
        {
            if (nodeDictionary.TryGetValue(nodeId, out DialogueGraphNode node))
            {
                return node;
            }

            return null;
        }

        public DialogueGraphNode GetNodeFromOutput(string outputNodeId, int index)
        {

            foreach (DialogueGraphConnection connection in Connections)
            {
                if (connection.outputPort.nodeId == outputNodeId && connection.outputPort.portIndex == index)
                {
                    string nodeId = connection.inputPort.nodeId;

                    DialogueGraphNode inputNode = nodeDictionary[nodeId];
                    return inputNode;
                }
            }

            return null;
        }
        
        public List<DialogueGraphNode> GetNodesFromOutput(string outputNodeId, int index)
        {

            List<DialogueGraphNode> dialogueGraphNodes = new List<DialogueGraphNode>();
            
            foreach (DialogueGraphConnection connection in Connections)
            {
                if (connection.outputPort.nodeId == outputNodeId && connection.outputPort.portIndex == index)
                {
                    string nodeId = connection.inputPort.nodeId;
                    dialogueGraphNodes.Add(nodeDictionary[nodeId]);
                }
            }
        
            return dialogueGraphNodes;
        }

    }
}