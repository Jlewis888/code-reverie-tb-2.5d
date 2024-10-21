using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class DialogueGraphObject : SerializedMonoBehaviour
    {
        [SerializeField] 
        private DialogueGraphAsset _dialogueGraphAsset;
        
        private DialogueGraphAsset graphInstance;

        private DialogueGraphNode currentNode;
        private DialogueGraphNode nextNode;

        private void OnEnable()
        {
            // graphInstance = Instantiate(_dialogueGraphAsset);
            // ExecuteAsset();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.dialogueManager);
                EventManager.Instance.playerEvents.OnDialogueStart(_dialogueGraphAsset);
                Debug.Log("Testting");
                //ProcessNextNode();
            }
        }

        private void ExecuteAsset()
        {
            graphInstance.Init(gameObject);

            DialogueGraphNode startNode = graphInstance.GetStartNode();
            ProcessCurrentNode(startNode);
            //ProcessAndMoveToNextNode(startNode);
        }

        private void ProcessCurrentNode(DialogueGraphNode dialogueGraphNode)
        {
            //string nextNodeId = dialogueGraphNode.OnProcess(graphInstance);
            List<string> nextNodeIdList = dialogueGraphNode.OnProcess(graphInstance);
            
            Debug.Log(nextNodeIdList[0]);
            Debug.Log(nextNodeIdList[1]);

            if (nextNodeIdList.Count > 0)
            {
                if (nextNodeIdList.Count == 1)
                {
                    nextNode = graphInstance.GetNode(nextNodeIdList[0]);
                }
            }
            
            // if (!string.IsNullOrEmpty(nextNodeId))
            // {
            //     nextNode = graphInstance.GetNode(nextNodeId);
            // }
        }

        private void ProcessNextNode()
        {
        
            if (nextNode == null)
            {
                return;
            }
            
            //string nextNodeId = nextNode.OnProcess(graphInstance);
            List<string> nextNodeIdList = nextNode.OnProcess(graphInstance);

            nextNode = null;
            
            if (nextNodeIdList.Count > 0)
            {
                if (nextNodeIdList.Count == 1)
                {
                    nextNode = graphInstance.GetNode(nextNodeIdList[0]);
                }
            }
            
            
            
            // if (!string.IsNullOrEmpty(nextNodeId))
            // {
            //     nextNode = graphInstance.GetNode(nextNodeId);
            // }
            // else
            // {
            //     nextNode = null;
            // }
        }
        //
        // private void ProcessAndMoveToNextNode(DialogueGraphNode dialogueGraphNode)
        // {
        //     string nextNodeId = dialogueGraphNode.OnProcess(graphInstance);
        //
        //     if (!string.IsNullOrEmpty(nextNodeId))
        //     {
        //         DialogueGraphNode node = graphInstance.GetNode(nextNodeId);
        //         ProcessAndMoveToNextNode(node);
        //     }
        // }
    }
}