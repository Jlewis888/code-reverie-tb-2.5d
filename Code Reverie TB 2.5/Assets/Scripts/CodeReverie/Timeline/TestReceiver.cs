using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace CodeReverie
{
    public class TestReceiver: SerializedMonoBehaviour, INotificationReceiver
    {
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            Debug.Log(notification);
        }
    }
}