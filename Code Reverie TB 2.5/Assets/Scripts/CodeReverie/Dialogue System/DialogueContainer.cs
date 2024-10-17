using System.Collections.Generic;
using Sirenix.OdinInspector;
//using Subtegral.DialogueSystem.DataContainers;

namespace CodeReverie
{
    public class DialogueContainer : SerializedScriptableObject
    {
        public List<NodeLinkData> NodeLinks = new List<NodeLinkData>();
        public List<DialogueNodeData> DialogueNodeData = new List<DialogueNodeData>();
        public List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
        public List<CommentBlockData> CommentBlockData = new List<CommentBlockData>();
    }
}