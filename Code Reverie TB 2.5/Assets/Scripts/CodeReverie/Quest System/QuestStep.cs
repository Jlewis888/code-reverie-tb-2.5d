using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CodeReverie
{
    public class QuestStep
    {
        public QuestStepDataContainer info;
        public List<QuestStepObjective> questObjectives = new List<QuestStepObjective>();
        public QuestObjectiveStatus questObjectiveStatus;
        public int objectivesComplete = 0;
        
        
        public QuestStep(QuestStepDataContainer info)
        {
            this.info = info;

            // foreach (QuestStepObjectiveData questObjectiveData in info.questObjectiveDataList)
            // {
            //     questObjectives.Add(new QuestStepObjective(questObjectiveData));
            // }
            questObjectives = info.questObjectives;
            questObjectiveStatus = QuestObjectiveStatus.Inactive;
        }

        public QuestObjectiveStatus Status
        {
            get { return questObjectiveStatus; }

            set
            {
              
                if (value != questObjectiveStatus)
                {
                    questObjectiveStatus = value;
                    
                    switch (value)
                    {
                        case QuestObjectiveStatus.Complete:
                            OnQuestStepComplete();
                            UnsubscribeQuestTriggers();
                            EventManager.Instance.questEvents.OnQuestStepComplete(this);

                            break;
                        
                        case QuestObjectiveStatus.Active:
                            SubscribeQuestTriggers();
                            foreach (QuestStepObjective questObjective in questObjectives)
                            {
                                questObjective.ObjectiveStatus = QuestObjectiveStatus.Active;
                            }
                            
                            break;
                        default:
                            UnsubscribeQuestTriggers();
                            foreach (QuestStepObjective questObjective in questObjectives)
                            {
                                questObjective.ObjectiveStatus = QuestObjectiveStatus.Inactive;
                            }
                            break;
                    }
                }
            }
        }
        
        protected virtual void SubscribeQuestTriggers()
        {

            EventManager.Instance.questEvents.onQuestObjectiveComplete += QuestStepCompleteCheck;
            
        }
        
        protected virtual void UnsubscribeQuestTriggers()
        {
            EventManager.Instance.questEvents.onQuestObjectiveComplete -= QuestStepCompleteCheck;
        }

        public void QuestStepCompleteCheck(QuestStepObjective questObjective)
        {
            if (questObjectives.Contains(questObjective))
            {
                objectivesComplete++;

                if (objectivesComplete == questObjectives.Count)
                {
                    Status = QuestObjectiveStatus.Complete;
                }
                
            }
        }

        public virtual void OnQuestStepComplete()
        {

            if (String.IsNullOrEmpty(info.onComplete))
            {
                return;
            }
            
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(info.onComplete);
            theMethod.Invoke(this, null);
        }

        public void TestOnCompleteFunction()
        {
            Debug.Log("Test On Complete Function");
        }

        public void CecilJoinParty()
        {
            PlayerManager.Instance.AddCharacterToAvailablePartyPool("Cecil");
            PlayerManager.Instance.AddCharacterToActiveParty("Cecil");
        }
        
        public void ArcaliaJoinParty()
        {
            PlayerManager.Instance.AddCharacterToAvailablePartyPool("Arcalia");
            PlayerManager.Instance.AddCharacterToActiveParty("Arcalia");
        }
    }
}