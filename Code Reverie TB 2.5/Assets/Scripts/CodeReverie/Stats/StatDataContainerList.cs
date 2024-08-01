using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "StatDataContainerList", menuName = "Scriptable Objects/Stats/StatDataContainerList", order = 1)]

    public class StatDataContainerList: SerializedScriptableObject
    {
        public List<StatDataContainer> statData;
        
        
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            statData = statData.Distinct().ToList();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}