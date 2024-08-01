using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "DamageTypeDataContainerList", menuName = "Scriptable Objects/Combat/DamageTypeDataContainerList", order = 1)]

    public class DamageTypeDataContainerList: SerializedScriptableObject
    {
        public List<DamageTypeDataContainer> damageTypeDetailsList;
        
        
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            damageTypeDetailsList = damageTypeDetailsList.Distinct().ToList();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}