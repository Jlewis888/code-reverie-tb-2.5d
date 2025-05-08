using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CodeReverie
{
    public class EndSkillTimelineMarker : Marker, INotification
    {
        public PropertyName id
        {
            get { return new PropertyName(); }
        }
    }
}