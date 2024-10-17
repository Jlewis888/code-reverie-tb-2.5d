using System;
using System.Reflection;
using UnityEditor.Experimental.GraphView;

namespace CodeReverie
{
    public class DialogueEditorNode : Node
    {

        private DialogueNode _dialogueNode;
        public DialogueNode Node => _dialogueNode;
        
        public DialogueEditorNode(DialogueNode node)
        {
            this.AddToClassList("dialogue-graph-node");

            _dialogueNode = node;

            Type typeInfo = node.GetType();

            NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

            title = info.Name;


            string[] depths = info.Category.Split('/');

            foreach (string depth in depths)
            {
                this.AddToClassList(depth.ToLower().Replace(' ', '-'));
            }

            this.name = typeInfo.Name;

        }

        public void SavePosition()
        {
            _dialogueNode.SetPosition(GetPosition());
        }
    }
}