using System;
using UnityEngine;

namespace CodeReverie
{
    [Serializable]
    public class DialogueNode
    {
        public string GUID;
        public string id => GUID;
        
        public bool EntyPoint = false;
        //public string DialogueText;
        public string typeName;
        private Rect _position;
        public Rect position => _position;

        public DialogueNode()
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
        
    }
}