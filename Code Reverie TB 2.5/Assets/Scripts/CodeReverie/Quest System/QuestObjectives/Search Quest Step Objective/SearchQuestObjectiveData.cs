using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    public enum SearchQuestType
    {
        Area,
        Character
    }
    
    
    [CreateAssetMenu(fileName = "SearchQuestObjectiveData", menuName = "Scriptable Objects/Quest/SearchQuestObjectiveData", order = 1)]
    public class SearchQuestObjectiveData : QuestStepObjectiveData
    {
        
        public List<CharacterDataContainer> characterQuestTriggerList = new List<CharacterDataContainer>();
        public SearchQuestType searchQuestType;
        
        
        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
#if UNITY_EDITOR
            //id = name;

            questObjectiveType = QuestObjectiveType.Search;
            characterQuestTriggerList = characterQuestTriggerList.Distinct().ToList();

            questTriggerIdList = new List<string>();
            
            foreach (CharacterDataContainer characterDataContainer in characterQuestTriggerList)
            {
                questTriggerIdList.Add(characterDataContainer.id);
            }
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}