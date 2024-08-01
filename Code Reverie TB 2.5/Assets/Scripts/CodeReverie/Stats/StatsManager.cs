using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class StatsManager : ManagerSingleton<StatsManager>
    {
        
        public StatDataContainerList StatDataList;

        //public Dictionary<StatAttribute, Stat> statsMap;
        public Dictionary<StatAttribute, StatDataContainer> statDataMap;


        protected override void Awake()
        {
            base.Awake();
            
            Initialize();
        }
        
        public bool Initialized { get; set; }
        
        public void Initialize()
        {
            SetStatDataMap();
            
            Initialized = true;
        }

        public void SetStatDataMap()
        {

            statDataMap = new Dictionary<StatAttribute, StatDataContainer>();
            
            foreach (StatDataContainer statData in StatDataList.statData)
            {
                if (statDataMap.ContainsKey(statData.statAttributeType))
                {
                    Debug.Log("Duplicate Stat Attribute");
                }
                
                statDataMap.Add(statData.statAttributeType, statData);
            }
            
            

        }

        public StatDataContainer GetStatData(StatAttribute statAttributeTypes)
        {
            return statDataMap[statAttributeTypes];
        }
        
        
        // public StatDataContainer GetStatData(StatPercentageAttributeTypes statPercentageAttributeTypes)
        // {
        //     foreach (StatDataContainer statData in statDataMap.Values)
        //     {
        //         if (statData.statPercentageAttributeType == statPercentageAttributeTypes)
        //         {
        //             return statData;
        //         }
        //     }
        //
        //     return null;
        // }
        

        public StatDataContainer GetStatData(DamageTypes damageType)
        {
            //TODO Need to look at and fix
            foreach (StatDataContainer statData in statDataMap.Values)
            {
                // if (statAttribute.StatData.damageBonusType == damageType)
                // {
                //     return statAttribute;
                // }
            }

            return null;
        }
        
        
        public Stat CreateStat(StatAttribute statAttribute, float statValue, StatType statType)
        {
            Stat stat = new Stat(statAttribute, statValue, statType);
            
            return stat;
        }
        

        // public Stat CreateStat(StatAttribute statAttributeTypes, float statValue)
        // {
        //     Stat stat = new Stat(GetStatData(statAttributeTypes), statValue, StatType.Additive);
        //     
        //     return stat;
        // }
        //
        //
        // public Stat CreateStat(StatPercentageAttributeTypes statPercentageAttributeTypes, float statValue)
        // {
        //     Stat stat = new Stat(GetStatData(statPercentageAttributeTypes), statValue, StatType.Percentage);
        //     
        //     return stat;
        // }
        

        // public Stat GetStatss(DamageTypes damageType)
        // {
        //     foreach (Stat statAttribute in statsMap.Values)
        //     {
        //         // if (statAttribute.StatData.damageBonusType == damageType)
        //         // {
        //         //     return statAttribute;
        //         // }
        //     }
        //
        //     return null;
        // }

    }
}