using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "KillQuestObjectiveData", menuName = "Scriptable Objects/Quest/KillQuestObjectiveData", order = 1)]
    public class KillQuestObjectiveData : QuestStepObjectiveData
    {
        public List<CharacterDataContainer> killQuestTriggerList = new List<CharacterDataContainer>();

        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
#if UNITY_EDITOR
            //id = name;
            questObjectiveType = QuestObjectiveType.Kill;
            
            killQuestTriggerList = killQuestTriggerList.Distinct().ToList();

            questTriggerIdList = new List<string>();
            
            foreach (CharacterDataContainer characterDataContainer in killQuestTriggerList)
            {
                questTriggerIdList.Add(characterDataContainer.id);
            }
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}