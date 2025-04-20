using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Playables;

namespace CodeReverie
{
    public class DialogueReceiver : SerializedMonoBehaviour, INotificationReceiver
    {
        public List<BehaviorGraph> dialogueGraphs = new List<BehaviorGraph>();
        
        
        
        public void OnNotify(Playable origin, INotification notification, object context)
        {
           
            DialogueMarker marker = notification as DialogueMarker;

            if (marker == null)
            {
                return;
            }

            foreach (BehaviorGraph behaviorGraph in dialogueGraphs)
            {
                if (marker.behaviorGraph == behaviorGraph)
                {
                    EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.dialogueManager);
                    EventManager.Instance.playerEvents.OnDialogueStart(behaviorGraph);
                    return;
                }
            }
            
            
            // BehaviorGraph behaviorGraph = marker.behaviorGraph;
            //
            // Debug.Log(behaviorGraph.name);
            //
            // EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.dialogueManager);
            // EventManager.Instance.playerEvents.OnDialogueStart(behaviorGraph);

        }
    }
}