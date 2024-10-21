namespace CodeReverie
{
    [NodeInfo("Dialogue", "", "Process/Dialogue")]
    public class DialogueNode : DialogueGraphNode
    {
        [ExposedProperty] 
        public CharacterDataContainer speaker;
        
        [ExposedProperty]
        public string dialogueText;


        public override void Execute(DialogueGraphAsset dialogueGraphAsset)
        {
            CanvasManager.Instance.dialogueManager.Dialogue(speaker, dialogueText);
        }
    }
}