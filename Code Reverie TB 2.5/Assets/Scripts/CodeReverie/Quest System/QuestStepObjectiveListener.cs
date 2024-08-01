using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "QuestStepObjectiveListener", menuName = "Scriptable Objects/Quest/QuestStepObjectiveListener", order = 1)]
    
    public class QuestStepObjectiveListener : SerializedScriptableObject
    {
        public string id;
        
        
        
        private void OnValidate()
        {
#if UNITY_EDITOR

            //id = $"{name}_{Guid.NewGuid()}";
            if (String.IsNullOrEmpty(id))
            {
                id = $"{name}_{Guid.NewGuid()}";
            }
         
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}