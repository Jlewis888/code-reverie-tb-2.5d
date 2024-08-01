using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class DamageProfile
    {
        
        public CharacterBattleManager damageSource;
        //public CharacterController damageTarget;
        public Health damageTarget;
        public float damageAmount;
        public List<DamageTypes> damageTypes;
        public bool isCrit;
        public bool applyAbsorptionHeal;
        

        public DamageProfile(CharacterBattleManager damageSource, Health damageTarget, List<DamageTypes> damageTypes)
        {
            this.damageSource = damageSource;
            this.damageTarget = damageTarget;
            this.damageTypes = damageTypes;
            isCrit = false;
            
            ApplyDamage();
        }

        public void ApplyDamage()
        {
            CalculateDamage();

            damageTarget.TryGetComponent(out IDamageable iDamageable);

            if (applyAbsorptionHeal)
            {
                iDamageable.ApplyHeal(damageAmount);
                return;
            }
            
            iDamageable.ApplyDamage(this);

        }
        
        
        public void CalculateDamage()
        {
            
            // Debug.Log($"Atk Damage amount: {damageSource.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Atk)}");
            // Debug.Log($"Damage Type amount: {ApplyDamageTypeCheck(damage.damageTypes)}");
            // Debug.Log($"Damage Reduction amount: {ApplyDamageReduction()}");
            // Debug.Log($"Crit Damage amount: {damageSource.ApplyCritModifier()}");
            //
            
            float calculatedDamage = (damageSource.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Atk) + ApplyAdditiveDamage()) * (1 + ApplyDamageBonuses()) * ApplyDamageTypeCheck() * (1-ApplyDamageReduction()) * (1 + ApplyCritModifier());

            damageAmount = calculatedDamage;
            
        }
        
        
        public float ApplyAdditiveDamage()
        {
            
            
            
            return 0;
        }
        
        public float ApplyDamageBonuses()
        {
            float damageBonusAmount = 0f;
            float damageTypeBonus = 0f;

            foreach (DamageTypes damageType in damageTypes)
            {

                float damageTypeStatBonus = 0;
                
                if(StatsManager.Instance.GetStatData(damageType) != null)
                {
                    
                    damageTypeStatBonus = damageSource.GetComponent<CharacterStatsManager>()
                        .GetStat(StatsManager.Instance.GetStatData(damageType).statAttributeType);
                }
                

                if (damageTypeStatBonus > damageTypeBonus)
                {
                    damageTypeBonus = damageTypeStatBonus;
                }

            }
            
            
            return (damageBonusAmount + damageTypeBonus)/ 100f;
        }
        
        
        public float ApplyDamageTypeCheck()
        {
          
            bool applyNormalDamage = false;
            bool applyImmune = false;
            bool applyVulnerable = false;
            bool applyReducedDamage = false;
            
            foreach (DamageTypes damageType in damageTypes)
            {
                DamageEffectiveTypes damageEffectiveType = DamageEffectiveTypes.NormalDamage;
                
                if (damageTarget.GetComponent<ComponentTagManager>().HasTag(ComponentTag.Character))
                {
                    damageEffectiveType = damageTarget.GetComponent<CharacterController>().character.info.baseStats.damageEffectiveTypes[damageType];
                }
                
                
                
                switch (damageEffectiveType)
                {
                    
                    case DamageEffectiveTypes.AbsorptionHeal:
                        applyAbsorptionHeal = true;
                        return 1f;
                    case DamageEffectiveTypes.Immune:
                        applyImmune = true;
                        break;
                    case DamageEffectiveTypes.Vulnerable:
                        applyVulnerable = true;
                        break;
                    case DamageEffectiveTypes.ReducedDamage:
                        applyReducedDamage = true;
                        break;
                }
                
            }

            if (applyImmune && applyVulnerable)
            {
                return 1f;
            }
            
            if (applyImmune)
            {
                return 0f;
            }
            
            if (applyVulnerable && applyReducedDamage)
            {
                return 1f;
            }
            
            if (applyVulnerable)
            {
                return 2f;
            }
            
            if (applyReducedDamage)
            {
                return 0.5f;
            }
           
            return 1f;
            
        }
        
        
        public float ApplyDamageReduction()
        {
            //todo look at this damage calculation
            float damageReduction = (10f + ((.25f * damageTarget.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Defense))/100))/100;

            if (damageTarget.GetComponent<ComponentTagManager>().HasTag(ComponentTag.Character))
            {
                damageReduction = (10f + ((1.5f * damageTarget.GetComponent<CharacterController>().character.level)/100) + ((.25f * damageTarget.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.Defense))/100))/100;
            }

            
            
            return damageReduction;
        }
        
        
        public float ApplyCritModifier()
        {
            if (IsCrit())
            {
                return damageSource.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.CritDmg)/100;
            }

            return 0f;
        }
        
        public bool IsCrit()
        {
            float randomValue = Random.value * 100;

            if (randomValue <= damageSource.GetComponent<CharacterStatsManager>().GetStat(StatAttribute.CritChance))
            {
                //CheckForTriggerConditions(AbilityTrigger.OnCrit);
                isCrit = true;
                return true;
            }

            return false;
        }
        
    }
}