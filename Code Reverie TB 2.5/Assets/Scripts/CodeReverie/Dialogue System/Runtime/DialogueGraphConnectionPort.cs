using System;

namespace CodeReverie
{
    [Serializable]
    public struct DialogueGraphConnectionPort
    {
        public string nodeId;
        public int portIndex;

        public DialogueGraphConnectionPort(string id, int index)
        {
            nodeId = id;
            portIndex = index;
        }
        
    }
}