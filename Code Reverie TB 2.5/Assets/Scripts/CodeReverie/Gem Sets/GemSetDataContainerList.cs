using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "GemSetDataContainerList", menuName = "Scriptable Objects/Gem Sets/GemSetDataContainerList",
        order = 1)]
    public class GemSetDataContainerList : SerializedScriptableObject
    {
        public List<GemSetDataContainer> gemSets = new List<GemSetDataContainer>();
        public Dictionary<GemSetType, GemSetDataContainer> GemSetDataContainersMap =
            new Dictionary<GemSetType, GemSetDataContainer>();
        
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            gemSets = gemSets.Distinct().ToList();
            SetGetSetDataContainerMap();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
        
        
        public void SetGetSetDataContainerMap()
        {
            GemSetDataContainersMap = new Dictionary<GemSetType, GemSetDataContainer>();

            foreach (GemSetDataContainer gemSet in gemSets)
            {
                GemSetDataContainersMap.Add(gemSet.gemSetType, gemSet);
            }
            
        }
        
    }
}