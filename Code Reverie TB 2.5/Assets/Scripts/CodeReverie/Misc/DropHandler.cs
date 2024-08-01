using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeReverie
{
    public class DropHandler : EventTrigger
    {
        public override void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Test");
        }
    }
}