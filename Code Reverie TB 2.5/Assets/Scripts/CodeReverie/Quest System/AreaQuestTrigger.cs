using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class AreaQuestTrigger : SerializedMonoBehaviour
    {
        public string questId;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.Instance.questEvents.StartQuest(questId); 
            }
            
        }
    }
}