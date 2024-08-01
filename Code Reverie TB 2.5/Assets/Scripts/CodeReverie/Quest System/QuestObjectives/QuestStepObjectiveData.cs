using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "QuestStepObjective", menuName = "Scriptable Objects/Quest/QuestStepObjective", order = 1)]
    public class QuestStepObjectiveData : SerializedScriptableObject
    {
        public string questId;
        public string questObjectiveDescription;
        public string questObjectiveDescriptionHud;
        public string questObjectiveDescriptionMenu;
        public QuestObjectiveType questObjectiveType;
        public int requiredAmount = 1;
        public bool showRequiredAmount;
        public List<string> questTriggerIdList = new List<string>();
        public bool optional;
        public bool requiresItemsToComplete;
        public Dictionary<ItemInfo, int> requiredItems = new Dictionary<ItemInfo, int>();

        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
#if UNITY_EDITOR
            //id = name;

            if (requiredItems == null)
            {
                requiredItems = new Dictionary<ItemInfo, int>();
            }

            foreach (KeyValuePair<ItemInfo, int> requiredItem in requiredItems)
            {
                if (requiredItem.Value <= 0)
                {
                    requiredItems[requiredItem.Key] = 1;
                }
            }
            
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}