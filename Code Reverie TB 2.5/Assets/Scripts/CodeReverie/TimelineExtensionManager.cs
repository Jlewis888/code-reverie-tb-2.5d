using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class TimelineExtensionManager : SerializedMonoBehaviour
    {
        public void TriggerDialogue(DialogueGraphAsset dialogueGraphAsset)
        {
            EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.dialogueManager);
            EventManager.Instance.playerEvents.OnDialogueStart(dialogueGraphAsset);
        }

        public void MovePlayerUnits(GameObject movePositionObject)
        {
            PlayerManager.Instance.SetPartyPosition(movePositionObject.transform.position);
        }
    }
}