using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest/Quest", order = 1)]
    public class QuestDataContainer : SerializedScriptableObject
    {
        [field: SerializeField] public string id { get; private set; }
        
        public string questName;
        public string description;
        public QuestStatus questStatus;
        public List<QuestDataContainer> questDetailsPrerequisites;
        // public List<QuestObjective> questObjectives;
        public List<QuestStepDataContainer> questStepDataContainers = new List<QuestStepDataContainer>();
        public QuestType questType;
        
        
        [Header("Rewards")]
        public int goldReward;
        public int experienceReward;
        
        public CharacterDataContainer questGiver;
        
        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
#if UNITY_EDITOR
            id = Regex.Replace( name, @"\s", string.Empty );
            if (questStepDataContainers == null)
            {
                questStepDataContainers = new List<QuestStepDataContainer>();
            }
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}