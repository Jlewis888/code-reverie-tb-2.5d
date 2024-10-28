using Sirenix.OdinInspector;
using Unity.Behavior;
using Unity.Cinemachine;
using UnityEngine;

namespace CodeReverie
{
    public class TimelineExtensionManager : SerializedMonoBehaviour
    {
        public void TriggerDialogue(BehaviorGraph behaviorGraph)
        {
            EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.dialogueManager);
            EventManager.Instance.playerEvents.OnDialogueStart(behaviorGraph);
        }

        public void MovePlayerUnits(GameObject movePositionObject)
        {
            PlayerManager.Instance.SetPartyPosition(movePositionObject.transform.position);
        }

        public void AreaManagerInit()
        {
            if (AreaManager.instance != null)
            {
                AreaManager.instance.Init();
            }
        }

        public void SpawnPlayerUnits()
        {
            if (AreaManager.instance != null)
            {
                AreaManager.instance.SpawnPlayerParty();
            }
        }

        public void SetActiveCam(CinemachineVirtualCamera virtualCamera)
        {
            virtualCamera.Priority = 1;
        }
        
    }
}