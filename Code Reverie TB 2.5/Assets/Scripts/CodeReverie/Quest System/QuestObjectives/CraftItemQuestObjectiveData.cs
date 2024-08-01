using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "CraftItemObjectiveData", menuName = "Scriptable Objects/Quest/CraftItemQuestObjectiveData", order = 1)]
    public class CraftItemObjectiveData : QuestStepObjectiveData
    {
        public List<ItemInfo> itemInfoList = new List<ItemInfo>();

        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
#if UNITY_EDITOR
            //id = name;
            
            
            itemInfoList = itemInfoList.Distinct().ToList();

            questObjectiveType = QuestObjectiveType.Craft;
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}