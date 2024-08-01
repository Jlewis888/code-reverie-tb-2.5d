using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "GearSetDataContainer", menuName = "Scriptable Objects/Gear Sets/GearSetDataContainer",
        order = 1)]
    public class GearSetDataContainer : SerializedScriptableObject
    {
        public GearSetType gearSetType;
        public Dictionary<int, GearSetBonus> gearSetBonusMap = new Dictionary<int, GearSetBonus>();
    }
}