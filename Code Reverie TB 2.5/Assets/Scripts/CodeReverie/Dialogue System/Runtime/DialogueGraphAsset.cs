using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "Dialogue Graph", menuName = "Dialogue/Dialogue Graph")]
    public class DialogueGraphAsset : ScriptableObject
    {
        [SerializeReference] 
        public BlackboardAsset Blackboard;

        [SerializeReference] 
        private List<DialogueNode> dialogueNodes = new List<DialogueNode>();

        public List<DialogueNode> DialogueNodes => dialogueNodes;

        public DialogueGraphAsset()
        {
            dialogueNodes = new List<DialogueNode>();
        }

    }
}