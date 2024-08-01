using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeReverie
{
    
    public class EscortQuestObjectiveObject : SerializedMonoBehaviour
    {
        public EscortQuestObjectiveData escortQuestObjectiveData;
        protected bool isComplete;
        private string questId;
        public string questObjectiveDescription;
        public List<string> objectiveIds;
        public QuestObjectiveType questObjectiveType;
        public QuestStatus questStatus;
        public int currentCount = 0;
        public int requiredAmount;
        
        
        public void SetQuestId(string id)
        {
            questId = id;
        }
        
        protected void CompleteQuestObjective()
        {
            if (!isComplete)
            {
                isComplete = true;
                
                EventManager.Instance.questEvents.AdvanceQuest(questId);
                
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // if (other.GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
            // {
            //     
            // }
            
            if (other.GetComponent<CharacterController>())
            {

                // if (escortQuestObjectiveData.escortQuestTriggerList.Find(x =>
                //         x.id == other.GetComponent<CharacterController>().character.info.id))
                // {
                //    
                //     EventManager.Instance.questEvents.OnEscortTargetEnter(escortQuestObjectiveData.questId);
                // }
                
            }
            
            
        }
    }
}