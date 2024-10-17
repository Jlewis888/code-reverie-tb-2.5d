using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CharacterStats
    {
        public Character character;
        public bool ignoreStats;
        public bool shouldUseModifiers = false;

        public List<StatModifierObject> statModifierProviders;

        public CharacterStats(Character character)
        {
            this.character = character;
        }
        

        public float GetStat(StatAttribute stat)
        {

            //if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.IgnoreStats) || ignoreStats)
            if (ignoreStats)
            {
                return 0f;
            }

            float percentageStat = GetBaseStat(StatsManager.Instance.GetStatData(stat).statAttributeType) +
                                   GetPercentageModifier(StatsManager.Instance.GetStatData(stat).statAttributeType);


          
            float statCeiling = (GetBaseStat(StatsManager.Instance.GetStatData(stat).statAttributeType) + GetAdditiveModifier(StatsManager.Instance.GetStatData(stat).statAttributeType)) *
                                (1 + percentageStat / 100);

            return Mathf.Ceil(statCeiling);
        }
        

        float GetBaseStat(StatAttribute stat)
        {
            if (character == null)
            {
                return 0f;
            }
            
            return character.info.baseStats.progressionStatMap[stat].statMap[character.Level];
        }
        
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

        private float GetAdditiveModifier(StatAttribute statAttribute)
        {
            float total = 0;


            // foreach (IStatModifierProvider provider in GetComponents<IStatModifierProvider>())
            // {
            //     
            //     
            //     foreach (float modifier in provider.GetAdditiveStatModifiers(statAttribute))
            //     {
            //         total += modifier;
            //     }
            // }

            
            List<Stat> stats = new List<Stat>();
            
            
            
            if (character != null)
            {

                if (character.characterGear.weaponSlot.item != null)
                {

                    
                    
                    if (character.characterGear.weaponSlot.item.info.baseItemStats != null)
                    {
                        stats.AddRange(character.characterGear.weaponSlot.item.info.baseItemStats);
                    }
                    
                    
                }
                
                foreach (GearSlot gearSlot in character.characterGear.accessorySlots.Values)
                {
                
                    if (gearSlot != null)
                    {
                        if (gearSlot.item != null)
                        {
                            
                            if (gearSlot.item.info.baseItemStats != null)
                            {
                                stats.AddRange(gearSlot.item.info.baseItemStats);
                            }
                            
                            stats.AddRange(gearSlot.item.stats);
                        }
                        
                    }
                }
                
                
                foreach (GearSlot gearSlot in character.characterGear.relicSlots.Values)
                {
                
                    if (gearSlot != null)
                    {
                        if (gearSlot.item != null)
                        {
                            
                            if (gearSlot.item.info.baseItemStats != null)
                            {
                                stats.AddRange(gearSlot.item.info.baseItemStats);
                            }
                            
                            stats.AddRange(gearSlot.item.stats);
                        }
                        
                    }
                }

                stats.AddRange(character.characterGear.gearSetBonusStats);
                
                
                foreach (Skill skill in character.characterSkills.equippedPassivesSkills.Values)
                {
                    if (skill != null)
                    {
                        stats.AddRange(skill.info.statModifiers);
                    }
                    
                }
            }

            foreach (TeamStatModifier teamStatModifier in PlayerManager.Instance.teamStatsModifiers)
            {
                if (teamStatModifier.stat.statAttribute == statAttribute && teamStatModifier.stat.statType == StatType.Additive)
                {
                    total += teamStatModifier.stat.statValue;
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


            
            //TODO May or may not need anymore
            // foreach (IStatModifierProvider provider in GetComponents<IStatModifierProvider>())
            // {
            //     
            //     foreach (float modifier in provider.GetPercentageStatModifiers(statAttribute))
            //     {
            //         total += modifier;
            //     }
            // }


            //TODO May or may not need anymore
            // if (GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
            // {
            //     
            //     if (WorldTierManager.Instance.CurrentWorldTierStatsModifier() != null)
            //     {
            //         total += WorldTierManager.Instance.CurrentWorldTierStatsModifier().stats[statAttribute];
            //     }
            // }
            
            
            
            List<Stat> stats = new List<Stat>();
            
            
            
            if (character != null)
            {

                if (character.characterGear.weaponSlot.item != null)
                {

                    
                    
                    if (character.characterGear.weaponSlot.item.info.baseItemStats != null)
                    {
                        stats.AddRange(character.characterGear.weaponSlot.item.info.baseItemStats);
                    }
                    
                    stats.AddRange(character.characterGear.weaponSlot.item.stats);
                    
                }
                
                foreach (GearSlot gearSlot in character.characterGear.accessorySlots.Values)
                {
                
                    if (gearSlot != null)
                    {
                        if (gearSlot.item != null)
                        {

                            if (gearSlot.item.info.baseItemStats != null)
                            {
                                stats.AddRange(gearSlot.item.info.baseItemStats);
                            }
                            
                            stats.AddRange(gearSlot.item.stats);
                            
                        }
                    }
                }
                
                foreach (GearSlot gearSlot in character.characterGear.relicSlots.Values)
                {
                
                    if (gearSlot != null)
                    {
                        if (gearSlot.item != null)
                        {

                            if (gearSlot.item.info.baseItemStats != null)
                            {
                                stats.AddRange(gearSlot.item.info.baseItemStats);
                            }
                            
                            stats.AddRange(gearSlot.item.stats);
                            
                        }
                    }
                }

                stats.AddRange(character.characterGear.gearSetBonusStats);

                foreach (Skill skill in character.characterSkills.equippedPassivesSkills.Values)
                {
                    if (skill != null)
                    {
                        stats.AddRange(skill.info.statModifiers);
                    }
                    
                }
                
            }
            
            foreach (TeamStatModifier teamStatModifier in PlayerManager.Instance.teamStatsModifiers)
            {
                if (teamStatModifier.stat.statAttribute == statAttribute && teamStatModifier.stat.statType == StatType.Percentage)
                {
                    total += teamStatModifier.stat.statValue;
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