using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "GearSetDataContainerList", menuName = "Scriptable Objects/Gear Sets/GearSetDataContainerList",
        order = 1)]
    public class GearSetDataContainerList : SerializedScriptableObject
    {
        public List<GearSetDataContainer> gearSets = new List<GearSetDataContainer>();
        public Dictionary<GearSetType, GearSetDataContainer> gearSetDataContainersMap =
            new Dictionary<GearSetType, GearSetDataContainer>();
        
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            gearSets = gearSets.Distinct().ToList();
            SetGetSetDataContainerMap();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
        
        
        public void SetGetSetDataContainerMap()
        {
            gearSetDataContainersMap = new Dictionary<GearSetType, GearSetDataContainer>();

            foreach (GearSetDataContainer gearSet in gearSets)
            {
                gearSetDataContainersMap.Add(gearSet.gearSetType, gearSet);
            }
            
        }
        
    }
}