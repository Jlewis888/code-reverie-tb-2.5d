using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "GemSetDataContainer", menuName = "Scriptable Objects/Gem Sets/GemSetDataContainer",
        order = 1)]
    public class GemSetDataContainer : SerializedScriptableObject
    {
        public GemSetType gemSetType;
        public Dictionary<int, GemSetBonus> gemSetBonusMap = new Dictionary<int, GemSetBonus>();
    }
}