using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeReverie
{


    
    
    public class SearchQuestObjectiveObject : SerializedMonoBehaviour
    {
        public QuestStepDataContainer questStepDataContainer;
        public SearchQuestStepObjective questStepObjective;
        public QuestStepObjectiveListener questStepObjectiveListener;
        protected bool isComplete;
        private string questId;
        public bool searched;
        public bool targetInRange;
        public bool playerInRange;

        public void Start()
        {
            // foreach (Quest quest in QuestManager.Instance.quests.Values)
            // {
            //     
            // }
            
            foreach (QuestStep questStep in QuestManager.Instance.questSteps.Values)
            {
                if (questStep.info.id == questStepDataContainer.id)
                {
                    foreach (QuestStepObjective objective in questStep.questObjectives)
                    {
                        if (questStepObjectiveListener.id == objective.questStepObjectiveListener.id)
                        {
                            questStepObjective = objective as SearchQuestStepObjective;
                            break;
                        }
                    }

                    break;
                }
            }
            
        }

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


        private void Update()
        {

            if (questStepObjective != null)
            {
                if (!searched)
                {
                    switch (questStepObjective.searchQuestType)
                    {
                        case SearchQuestType.Area:
                            if (playerInRange)
                            {
                                searched = true;
                                EventManager.Instance.playerEvents.OnSearch(questStepObjectiveListener.id);
                            }
                            break;
                        case SearchQuestType.Character:
                            if (targetInRange && playerInRange)
                            {
                                searched = true;
                                EventManager.Instance.playerEvents.OnSearch(questStepObjectiveListener.id);
                                //Destroy(gameObject);
                            }
                            break;
                    }
                }
            }
            

            
            

            
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!searched)
            {
                if (other.TryGetComponent(out ComponentTagManager playerComponentTagManager ))
                {
                    if(playerComponentTagManager.HasTag(ComponentTag.Player))
                    {
                        playerInRange = true;
                    }
               
                }
            
                if (other.GetComponent<CharacterController>())
                {

                    if (questStepObjective.characterQuestTriggerList.Find(x =>
                            x.id == other.GetComponent<CharacterController>().character.info.id))
                    {
                        targetInRange = true;
                        //EventManager.Instance.questEvents.OnEscortTargetEnter(escortQuestObjectiveData.questId);
                    }
                
                }
            }
            
            
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (!searched)
            {
                if (other.TryGetComponent(out ComponentTagManager playerComponentTagManager ))
                {
                    if(playerComponentTagManager.HasTag(ComponentTag.Player))
                    {
                        playerInRange = false;
                    }
               
                }
            
                if (other.GetComponent<CharacterController>())
                {

                    if (questStepObjective.characterQuestTriggerList.Find(x =>
                            x.id == other.GetComponent<CharacterController>().character.info.id))
                    {
                        targetInRange = false;
                        //EventManager.Instance.questEvents.OnEscortTargetEnter(escortQuestObjectiveData.questId);
                    }
                
                }
            }
        }
    }
}