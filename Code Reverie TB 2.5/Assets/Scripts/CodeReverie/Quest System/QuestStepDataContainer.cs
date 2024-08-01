using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "QuestStep", menuName = "Scriptable Objects/Quest/Quest Step", order = 1)]
    public class QuestStepDataContainer : SerializedScriptableObject
    {
        public string id;
        public string questStepDescription;
        public List<QuestStepObjective> questObjectives = new List<QuestStepObjective>();
        public List<QuestStepObjectiveData> questObjectiveDataList;
        public bool startQuest;
        public string onComplete;
        
        public CharacterDataContainer questGiver;
        
        
        private void OnValidate()
        {
#if UNITY_EDITOR

            //id = $"{name}_{Guid.NewGuid()}";
            if (String.IsNullOrEmpty(id))
            {
                id = $"{name}_{Guid.NewGuid()}";
            }
            
            if (questObjectives == null)
            {
                questObjectives = new List<QuestStepObjective>();
            }
            
            
            if (questObjectiveDataList == null)
            {
                questObjectiveDataList = new List<QuestStepObjectiveData>();
            }
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
    
}