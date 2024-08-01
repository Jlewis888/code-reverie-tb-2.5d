using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "StatDataContainer", menuName = "Scriptable Objects/Stats/StatDataContainer", order = 1)]

    public class StatDataContainer : SerializedScriptableObject
    {
        public StatType statType;
        public StatAttribute statAttributeType;
        //public StatPercentageAttributeTypes statPercentageAttributeType;

        public string statAttributeName;
        public string statPercentageAttributeName;

        // public DamageType damageBonusType;
    }
}