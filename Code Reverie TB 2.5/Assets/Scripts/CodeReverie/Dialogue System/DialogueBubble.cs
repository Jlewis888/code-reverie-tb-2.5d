using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;

namespace CodeReverie
{
    public class DialogueBubble : SerializedMonoBehaviour
    {
        
        public DialogueOption speechOption;
        public DialogueOption activeDialogueChoiceOption;
        public List<DialogueOption> dialogueChoiceOptions;
        public TMP_Text nameText;
        public TMP_Text dialogueText;
    }
}