using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "CharacterBaseStats", menuName = "Scriptable Objects/Stats/CharacterBaseStats", order = 1)]

    public class CharacterBaseStats : SerializedScriptableObject
    {
        public Dictionary<StatAttribute, float> baseStats = new Dictionary<StatAttribute, float>();
        
        public Dictionary<DamageTypes, DamageEffectiveTypes> damageEffectiveTypes = new Dictionary<DamageTypes, DamageEffectiveTypes>();

        public CharacterBaseStats()
        {
            SetAllStats();
        }
        
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
                if (!baseStats.ContainsKey((StatAttribute)stat.GetValue(i)))
                {
                    switch ((StatAttribute)stat.GetValue(i))
                    {
                        case StatAttribute.AlchemicRecharge:
                        case StatAttribute.AtkSpd:
                            baseStats.Add((StatAttribute)stat.GetValue(i), 100);
                            break;
                        default:
                            baseStats.Add((StatAttribute)stat.GetValue(i), 0);
                            break;
                    }
                }
            }
            
            
            // Array statPercentage = Enum.GetValues(typeof(StatPercentageAttributeTypes));
            //
            // for (int i = 0; i < statPercentage.Length; i++)
            // {
            //     if (!basePercentageStats.ContainsKey((StatPercentageAttributeTypes)statPercentage.GetValue(i)))
            //     {
            //         basePercentageStats.Add((StatPercentageAttributeTypes)statPercentage.GetValue(i), 0);
            //     }
            // }
            
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