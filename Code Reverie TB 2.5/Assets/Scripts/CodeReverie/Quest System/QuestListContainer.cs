using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "QuestListContainer", menuName = "Scriptable Objects/Quest/QuestListContainer", order = 1)]

    public class QuestListContainer : SerializedScriptableObject
    {
        public List<QuestDataContainer> questDataContainers = new List<QuestDataContainer>();
        public List<QuestStepDataContainer> questStepDataContainers = new List<QuestStepDataContainer>();


#if UNITY_EDITOR
        [Button("Get All Quests")]
        public void UpdateQuest()
        {
            questDataContainers = AssetDatabase.FindAssets("t:QuestDataContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<QuestDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
        }
        
        // [Button("Update All Quests Step ID's")]
        // public void UpdateQuestStepIDs()
        // {
        //     List<QuestStepDataContainer> questStepDataContainers = AssetDatabase.FindAssets("t:QuestStepDataContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<QuestStepDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
        //     
        // }
        
        
        
        #endif
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            questDataContainers = questDataContainers.Distinct().ToList();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}