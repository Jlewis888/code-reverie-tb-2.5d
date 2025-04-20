using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CodeReverie
{
    public class TestMarker : Marker, INotification
    {

        public GameObject testObject;
        
        public PropertyName id
        {
            get { return new PropertyName(); }
        }
    }
}