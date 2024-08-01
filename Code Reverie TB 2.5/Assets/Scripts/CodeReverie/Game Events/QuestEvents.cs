using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class QuestEvents
    {
        public event Action<string> onStartQuest;

        public void StartQuest(string id)
        {
            onStartQuest?.Invoke(id);
        }
        
        public event Action<string> onAdvanceQuest;

        public void AdvanceQuest(string id)
        {
            onAdvanceQuest?.Invoke(id);
        }
        
        
        public event Action<string> onCompleteQuest;

        public void CompleteQuest(string id)
        {
            onCompleteQuest?.Invoke(id);
        }
        
        public event Action<Quest> onQuestStateChange;

        public void OnQuestStateChange(Quest quest)
        {
            onQuestStateChange?.Invoke(quest);
        }
        
        
        public event Action<Quest> onQuestStepChange;

        public void OnQuestStepChange(Quest quest)
        {
            onQuestStepChange?.Invoke(quest);
        }
        

        public Action<QuestStepObjective> onQuestObjectiveUpdate;

        public void OnQuestObjectiveUpdate(QuestStepObjective questObjective)
        {
            onQuestObjectiveUpdate?.Invoke(questObjective);
        }
        
        public Action<QuestStepObjective> onQuestObjectiveComplete;

        public void OnQuestObjectiveComplete(QuestStepObjective questObjective)
        {
            onQuestObjectiveComplete?.Invoke(questObjective);
        }
        
        
        public Action<QuestStep> onQuestStepComplete;

        public void OnQuestStepComplete(QuestStep questStep)
        {
            onQuestStepComplete?.Invoke(questStep);
        }


        public Action<Quest, bool> onQuestTrackingStatusChange;

        public void OnQuestTrackingStatusChange(Quest quest, bool isTracking)
        {
            onQuestTrackingStatusChange?.Invoke(quest, isTracking);
        }


        public Action<string> onEscortTargetEnter;

        public void OnEscortTargetEnter(string id)
        {
            onEscortTargetEnter?.Invoke(id);
        }
        
    }
}