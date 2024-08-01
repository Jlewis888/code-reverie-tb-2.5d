using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "EscortQuestObjectiveData", menuName = "Scriptable Objects/Quest/EscortQuestObjectiveData", order = 1)]
    public class EscortQuestObjectiveData : QuestStepObjectiveData
    {
        public List<CharacterDataContainer> escortQuestTriggerList = new List<CharacterDataContainer>();

        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
#if UNITY_EDITOR
            //id = name;
            
            
            escortQuestTriggerList = escortQuestTriggerList.Distinct().ToList();

            //questTriggerIdList = new List<string>();
            
            // foreach (CharacterDataContainer characterDataContainer in killQuestTriggerList)
            // {
            //     questTriggerIdList.Add(characterDataContainer.id);
            // }
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}