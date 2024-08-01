using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "WorldTierStatsModifier", menuName = "Scriptable Objects/World Tier/WorldTierStatsModifier", order = 1)]

    public class WorldTierStatsModifier : SerializedScriptableObject
    {
        public Dictionary<StatAttribute, float> stats = new Dictionary<StatAttribute, float>();
        
        public Dictionary<DamageTypes, DamageEffectiveTypes> damageEffectiveTypes = new Dictionary<DamageTypes, DamageEffectiveTypes>();

        
        
        private void OnValidate()
        {
            
#if UNITY_EDITOR
            SetAllStats();
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }


        public void SetAllStats()
        {
            Array stat = Enum.GetValues(typeof(StatAttribute));

            for (int i = 0; i < stat.Length; i++)
            {
                if (!stats.ContainsKey((StatAttribute)stat.GetValue(i)))
                {
                    switch ((StatAttribute)stat.GetValue(i))
                    {
                        case StatAttribute.AlchemicRecharge:
                        case StatAttribute.AtkSpd:
                            stats.Add((StatAttribute)stat.GetValue(i), 100);
                            break;
                        default:
                            stats.Add((StatAttribute)stat.GetValue(i), 0);
                            break;
                    }
                }
            }
            
            
            
            Array damageType = Enum.GetValues(typeof(DamageTypes));
            
            for (int i = 0; i < damageType.Length; i++)
            {
                if (!damageEffectiveTypes.ContainsKey((DamageTypes)stat.GetValue(i)))
                {
                    damageEffectiveTypes.Add((DamageTypes)stat.GetValue(i), DamageEffectiveTypes.NormalDamage);
                }
            }
        }
    }
}