using Unity.Behavior;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CodeReverie
{
    public class DialogueMarker : Marker, INotification
    {

        [SerializeField] private BehaviorGraph _behaviorGraph;
        
        public PropertyName id
        {
            get { return new PropertyName(); }
        }

        public BehaviorGraph behaviorGraph => _behaviorGraph;
    }
}