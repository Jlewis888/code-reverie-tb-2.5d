using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "DamageTypeDataContainer", menuName = "Scriptable Objects/Combat/DamageTypeDataContainer", order = 1)]

    public class DamageTypeDataContainer: SerializedScriptableObject
    {
        public DamageTypes damageType;
        public StatAttribute statAttributeType;
    }
}