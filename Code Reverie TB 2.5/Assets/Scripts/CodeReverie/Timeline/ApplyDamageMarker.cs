using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CodeReverie
{
    public class ApplyDamageMarker : Marker, INotification
    {

        public GameObject testObject;
        
        public PropertyName id
        {
            get { return new PropertyName(); }
        }
    }
}