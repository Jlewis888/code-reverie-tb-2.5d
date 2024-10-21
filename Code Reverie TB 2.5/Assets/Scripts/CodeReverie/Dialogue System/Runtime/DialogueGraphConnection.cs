using System;

namespace CodeReverie
{
    [Serializable]
    public class DialogueGraphConnection
    {
        public DialogueGraphConnectionPort inputPort;
        public DialogueGraphConnectionPort outputPort;

        public DialogueGraphConnection(DialogueGraphConnectionPort inputPort, DialogueGraphConnectionPort outputPort)
        {
            this.inputPort = inputPort;
            this.outputPort = outputPort;
        }

        public DialogueGraphConnection(string inputPortId, int inputIndex, string outputPortId, int outputIndex)
        {
            this.inputPort = new DialogueGraphConnectionPort(inputPortId, inputIndex);
            this.outputPort = new DialogueGraphConnectionPort(outputPortId, outputIndex);
        }
        
    }
}