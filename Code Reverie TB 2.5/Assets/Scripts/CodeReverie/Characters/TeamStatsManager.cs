using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class TeamStatsManager : SerializedMonoBehaviour
    {
        public bool ignoreStats;
        public List<TeamStatModifier> teamStatsModifiers = new List<TeamStatModifier>();



        //public List<StatModifier> statModifiers = new List<StatModifier>();
     
        public List<StatModifierObject> statModifierProviders;
        //public List<StatModifierObject> statModifierProvidersTemp;
        
        private void Awake()
        {
            teamStatsModifiers = new List<TeamStatModifier>();
        }

        private void Update()
        {
            //SetCombatModifiers();

            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log(GetStat(StatAttribute.Atk));
                //tempStats.Add(GenerateTempStatId(), new StatModifier(StatAttribute.Atk, 10, 10, true));
                
                //statModifiers.Add(new StatModifier(StatAttribute.Atk, 10, 10, true));
            }
        }


        private void Start()
        {
            
        }

        public float GetStat(StatAttribute stat)
        {

            if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.IgnoreStats) || ignoreStats)
            {
                return 0f;
            }

            float percentageStat = GetBaseStat(StatsManager.Instance.GetStatData(stat).statAttributeType) +
                                   GetPercentageModifier(StatsManager.Instance.GetStatData(stat).statAttributeType);


          
            float statCeiling = (GetBaseStat(StatsManager.Instance.GetStatData(stat).statAttributeType) + GetAdditiveModifier(StatsManager.Instance.GetStatData(stat).statAttributeType)) *
                                (1 + percentageStat / 100);

            return Mathf.Ceil(statCeiling);
        }
        
        
        // public float GetStat(StatPercentageAttributeTypes stat)
        // {
        //
        //     
        //     float percentageStat = GetBaseStat(StatsManager.Instance.GetStatData(stat).statPercentageAttributeType) +
        //                            GetPercentageModifier(StatsManager.Instance.GetStatData(stat).statPercentageAttributeType);
        //     // float statCeiling = (GetBaseStat(StatsManager.Instance.GetStatAttribute(stat).StatData.statAttributeType) + GetAdditiveModifier(StatsManager.Instance.GetStatAttribute(stat).StatData.statAttributeType)) *
        //     //                     (1 + percentageStat / 100);
        //
        //     return percentageStat;
        // }
        

        float GetBaseStat(StatAttribute stat)
        {
            if (GetComponent<CharacterController>().character == null)
            {
                //Debug.Log("Character not initialized on Character Unit");
                return 0f;
            }
            
            return GetComponent<CharacterController>().character.info.baseStats.progressionStatMap[stat].statMap[GetComponent<CharacterController>().character.Level];
        }
        
        
        // float GetBaseStat(StatPercentageAttributeTypes stat)
        // {
        //     
        //     
        //     
        //     
        //     if (GetComponent<CharacterController>().character == null)
        //     {
        //         //Debug.Log("Character not initialized on Character Unit");
        //         return 0f;
        //     }
        //     
        //     return GetComponent<CharacterController>().character.info.baseStats.basePercentageStats[stat];
        // }
        

        // public StatAttribute GetLowestStat()
        // {
        //     StatAttribute lowestStat = StatAttribute.Atk;
        //     float statNumber = Mathf.Infinity;
        //
        //
        //     foreach (KeyValuePair<StatAttribute, float> stats in baseStats)
        //     {
        //         if (GetStat(stats.Key) < statNumber)
        //         {
        //             lowestStat = stats.Key;
        //             statNumber = GetStat(stats.Key);
        //         }
        //     }
        //
        //     return lowestStat;
        // }

        // public StatAttribute GetHighestStat()
        // {
        //     StatAttribute lowestStat = StatAttribute.Atk;
        //     float statNumber = 0;
        //
        //
        //     foreach (KeyValuePair<StatAttribute, float> stats in baseStats)
        //     {
        //         if (GetStat(stats.Key) > statNumber)
        //         {
        //             lowestStat = stats.Key;
        //             statNumber = GetStat(stats.Key);
        //         }
        //     }
        //
        //     return lowestStat;
        // }

        public StatAttribute DamageTypeStatToDamageBonusStat(DamageTypes damageType)
        {
            switch (damageType)
            {
                case DamageTypes.Physical:
                    return StatAttribute.PhysicalDamage;
                case DamageTypes.Fire:
                    return StatAttribute.FireDamage;
                case DamageTypes.Air:
                    return StatAttribute.AirDamage;
                case DamageTypes.Water:
                    return StatAttribute.WaterDamage;
                case DamageTypes.Ice:
                    return StatAttribute.IceDamage;
                case DamageTypes.Earth:
                    return StatAttribute.EarthDamage;
                case DamageTypes.Lightning:
                    return StatAttribute.LightningDamage;
                case DamageTypes.Poison:
                    return StatAttribute.PoisonDamage;
                case DamageTypes.Piercing:
                    return StatAttribute.PiercingDamage;
                default:
                    return StatAttribute.None;
            }
        }
        

        // public void SetCombatModifiers()
        // {
        //     List<StatModifier> newCombatAdditiveModifiersList = new List<StatModifier>();
        //
        //     if (statModifiers != null)
        //     {
        //         foreach (StatModifier modifier in statModifiers)
        //         {
        //             if (modifier.isPermanent)
        //             {
        //                 newCombatAdditiveModifiersList.Add(modifier);
        //             }
        //             else if (modifier.duration > 0)
        //             {
        //                 modifier.UpdateDuration(-Time.deltaTime);
        //                 newCombatAdditiveModifiersList.Add(modifier);
        //             }
        //         }
        //
        //         statModifiers = newCombatAdditiveModifiersList;
        //     }
        // }

        private float GetAdditiveModifier(StatAttribute statAttribute)
        {
            float total = 0;

            //if (!shouldUseModifiers) return total;

            foreach (IStatModifierProvider provider in GetComponents<IStatModifierProvider>())
            {
                
                
                foreach (float modifier in provider.GetAdditiveStatModifiers(statAttribute))
                {
                    total += modifier;
                }
            }

            
            List<Stat> stats = new List<Stat>();
            
            
            
            if (TryGetComponent(out CharacterController characterController))
            {

                if (characterController.character.characterGear.weaponSlot.item != null)
                {

                    
                    
                    if (characterController.character.characterGear.weaponSlot.item.info.baseItemStats != null)
                    {
                        stats.AddRange(characterController.character.characterGear.weaponSlot.item.info.baseItemStats);
                    }
                    
                    stats.AddRange(characterController.character.characterGear.weaponSlot.item.stats);
                    
                }
                
                foreach (GearSlot gearSlot in characterController.character.characterGear.accessorySlots.Values)
                {
                
                    if (gearSlot != null)
                    {
                        if (gearSlot.item != null)
                        {
                            
                            stats.AddRange(gearSlot.item.stats);
                            
                            // foreach (Stat itemStat in gearSlot.item.stats)
                            // {
                            //     if (itemStat.statAttribute == statAttribute && itemStat.statType == StatType.Additive)
                            //     {
                            //         total += itemStat.StatValue;
                            //     }
                            // }
                        }
                        
                        
                    }
                    
                }



                foreach (Skill skill in characterController.character.characterSkills.equippedPassivesSkills.Values)
                {
                    foreach (Stat statModifier in skill.info.statModifiers)
                    {
                        
                       
                        
                        if (statModifier.statAttribute == statAttribute && statModifier.statType == StatType.Additive)
                        {
                            total += statModifier.statValue;
                        }
                    }
                }
                
                
            }
            
            foreach (Stat stat in stats)
            {
                if (stat.statAttribute == statAttribute && stat.statType == StatType.Additive)
                {
                    total += stat.statValue;
                }
            }
            
            return total;
        }

        private float GetPercentageModifier(StatAttribute statAttribute)
        {
            float total = 0;

            //if (!shouldUseModifiers) return total;


            foreach (IStatModifierProvider provider in GetComponents<IStatModifierProvider>())
            {
                
                foreach (float modifier in provider.GetPercentageStatModifiers(statAttribute))
                {
                    total += modifier;
                }
            }


            if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
            {
                
                if (WorldTierManager.Instance.CurrentWorldTierStatsModifier() != null)
                {
                    total += WorldTierManager.Instance.CurrentWorldTierStatsModifier().stats[statAttribute];
                }
            }
            
            
            
            List<Stat> stats = new List<Stat>();
            
            
            
            if (TryGetComponent(out CharacterController characterController))
            {

                if (characterController.character.characterGear.weaponSlot.item != null)
                {

                    
                    
                    if (characterController.character.characterGear.weaponSlot.item.info.baseItemStats != null)
                    {
                        stats.AddRange(characterController.character.characterGear.weaponSlot.item.info.baseItemStats);
                    }
                    
                    stats.AddRange(characterController.character.characterGear.weaponSlot.item.stats);
                    
                }
                
                foreach (GearSlot gearSlot in characterController.character.characterGear.accessorySlots.Values)
                {
                
                    if (gearSlot != null)
                    {
                        if (gearSlot.item != null)
                        {
                            
                            stats.AddRange(gearSlot.item.stats);
                            
                            // foreach (Stat itemStat in gearSlot.item.stats)
                            // {
                            //     if (itemStat.statAttribute == statAttribute && itemStat.statType == StatType.Additive)
                            //     {
                            //         total += itemStat.StatValue;
                            //     }
                            // }
                        }
                        
                        
                    }
                    
                }



                foreach (Skill skill in characterController.character.characterSkills.equippedPassivesSkills.Values)
                {
                    foreach (Stat statModifier in skill.info.statModifiers)
                    {
                        if (statModifier.statAttribute == statAttribute && statModifier.statType == StatType.Percentage)
                        {
                            total += statModifier.statValue;
                        }
                    }
                }
                
                
            }
            
            foreach (Stat stat in stats)
            {
                if (stat.statAttribute == statAttribute && stat.statType == StatType.Percentage)
                {
                    total += stat.statValue;
                }
            }
            


            return total;
        }

        // public IEnumerable<float> GetAdditiveStatModifiers(StatAttribute stat)
        // {
        //     if (statModifiers == null)
        //     {
        //         statModifiers = new List<StatModifier>();
        //     }
        //
        //     foreach (var modifier in statModifiers)
        //     {
        //         if (modifier.statAttribute == stat)
        //         {
        //             yield return modifier.value;
        //         }
        //     }
        //
        //
        //     // foreach (Stats characterStats in stats)
        //     // {
        //     //     foreach (KeyValuePair<Stat, float> mainStats in characterStats.mainStats)
        //     //     {
        //     //
        //     //         if (mainStats.Key == stat)
        //     //         {
        //     //             yield return mainStats.Value;
        //     //         }
        //     //     }
        //     //     
        //     //     foreach (KeyValuePair<Stat, float> bonusStats in characterStats.bonusStats)
        //     //     {
        //     //
        //     //         if (bonusStats.Key == stat)
        //     //         {
        //     //             yield return bonusStats.Value;
        //     //         }
        //     //     }
        //     //     
        //     //     foreach (KeyValuePair<Stat, float> bonusStats in characterStats.bonusStats)
        //     //     {
        //     //
        //     //         if (bonusStats.Key == stat)
        //     //         {
        //     //             yield return bonusStats.Value;
        //     //         }
        //     //     }
        //     // }
        // }
        //
        // public IEnumerable<float> GetPercentageStatModifiers(StatPercentageAttributeTypes stat)
        // {
        //     if (statPercentageModifiers == null)
        //     {
        //         statPercentageModifiers = new List<StatPercentageModifier>();
        //     }
        //
        //     foreach (var modifier in statPercentageModifiers)
        //     {
        //         if (modifier.statAttribute == stat)
        //         {
        //             yield return modifier.value;
        //         }
        //     }
        // }


        public void SetStats(StatAttribute stat, float statValue)
        {
            //baseStatsList[stat] += statValue;
        }

        public string GenerateTempStatId()
        {
            return $"SM-{GameManager.Instance.GenerateGuid()}";
        }
    }
}