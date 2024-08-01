using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeReverie
{
    public class Quest
    {
        
        //Quest Hierarchy:
        //Quest
        //Quest Steps
        //Quest Objectives
        
        
        public QuestDataContainer info;
        public QuestStatus questStatus;
        public int currentQuestObjectiveIndex;
        public QuestStep currentQuestStep;
        public List<QuestStep> questSteps = new List<QuestStep>();
        private bool isTracked;

        public Quest(QuestDataContainer questDetails)
        {
            this.info = questDetails;
            questStatus = QuestStatus.Inactive;
            currentQuestObjectiveIndex = 0;

            foreach (QuestStepDataContainer questStepDataContainer in questDetails.questStepDataContainers)
            {
                QuestStepDataContainer _questStepDataContainer = ScriptableObject.Instantiate(questStepDataContainer);
                questSteps.Add(new QuestStep(_questStepDataContainer));
            }

            if (questSteps.Count > 0)
            {
                CurrentQuestObjective = questSteps[currentQuestObjectiveIndex];
                //CurrentQuestObjective.Status = QuestObjectiveStatus.Active;
            }
            else
            {
                Status = QuestStatus.NoQuestObjectives;
            }

            
            
        }


        public bool IsTracking
        {
            get { return isTracked; }

            set
            {
                if (value != isTracked)
                {
                    isTracked = value;
                    
                    EventManager.Instance.questEvents.OnQuestTrackingStatusChange(this, value);
                }
            }
        }
        

        public QuestStatus Status
        {
            get { return questStatus; }

            set
            {
                if (value != questStatus)
                {
                    questStatus = value;

                    switch (value)
                    {
                        case QuestStatus.Complete:
                            UnsubscribeQuestTriggers();
                            break;
                        case QuestStatus.Active:
                          
                            SubscribeQuestTriggers();
                            CurrentQuestObjective = questSteps[currentQuestObjectiveIndex];
                            CurrentQuestObjective.Status = QuestObjectiveStatus.Active;
                            
                      
                            break;
                        default:
                            UnsubscribeQuestTriggers();
                            break;
                    }
                    
                    EventManager.Instance.questEvents.OnQuestStateChange(this);
                }
            }
        }
        

        public QuestStep CurrentQuestObjective
        {
            get { return currentQuestStep; }

            set
            {
                if (value != currentQuestStep)
                {
                    currentQuestStep = value;
                    
                    EventManager.Instance.questEvents.OnQuestStepChange(this);
                }
            }
        }

        public void CompleteCurrentQuestObjective()
        {
           // CurrentQuestObjective.ObjectiveStatus = QuestObjectiveStatus.Complete;
        }

        public void NextQuestObjective()
        {
            currentQuestObjectiveIndex++;
            
            CurrentQuestObjective = questSteps[currentQuestObjectiveIndex];
            CurrentQuestObjective.Status = QuestObjectiveStatus.Active;
        }

        public bool CurrentQuestObjectiveIndexCheck()
        {
            return true;
            // return (currentQuestObjectiveIndex < info.questObjectiveObjects.Count);
        }

        public bool LastQuestObjectiveCompleteCheck()
        {
            int count = questSteps.Where(x => x.Status == QuestObjectiveStatus.Complete).ToList().Count;

            if (count >= questSteps.Count)
            {
                return true;
            }


            return false;

    }

        public void InstantiateCurrentQuestObjective(Transform transform)
        {
            QuestObjectiveObject questDetailsPrefab = GetCurrentQuestObjectivePrefab();

            if (questDetailsPrefab != null)
            {
                QuestObjectiveObject questObjective = Object.Instantiate(questDetailsPrefab, transform).GetComponent<QuestObjectiveObject>();
                
                questObjective.SetQuestId(info.id);
            }
        }

        QuestObjectiveObject GetCurrentQuestObjectivePrefab()
        {
            QuestObjectiveObject questObjective = null;

            if (CurrentQuestObjectiveIndexCheck())
            {
                //questObjective = info.questObjectiveObjects[currentQuestObjectiveIndex];
            }
            else
            {
                
            }

            return questObjective;

        }
        
        
        protected virtual void SubscribeQuestTriggers()
        {

           EventManager.Instance.questEvents.onQuestStepComplete += QuestStepCompleteCheck;
            
        }
        
        protected virtual void UnsubscribeQuestTriggers()
        {
            EventManager.Instance.questEvents.onQuestStepComplete -= QuestStepCompleteCheck;
        }

        public void QuestStepCompleteCheck(QuestStep questStep)
        {
            if (questSteps.Contains(questStep))
            {
                if (LastQuestObjectiveCompleteCheck())
                {
                    Status = QuestStatus.Complete;
                }
                else
                {
                   NextQuestObjective();
                }
                
            }
        }

    }
}