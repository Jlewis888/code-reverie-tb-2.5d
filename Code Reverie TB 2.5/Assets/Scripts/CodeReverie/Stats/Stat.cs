using System.Collections.Generic;

namespace CodeReverie
{
    public class Stat
    {
        //public StatDataContainer statData;
        public StatAttribute statAttribute;
        public float statValue;
        public StatType statType;
        
        // public Stat(StatData statData)
        // {
        //     this.statData = statData;
        // }
        
        // public Stat(StatDataContainer statData, float value, StatType statType)
        // {
        //     this.statData = statData;
        //     StatValue = value;
        //     this.statType = statType;
        // }
        
        public Stat(StatAttribute statAttribute, float value, StatType statType)
        {
            this.statAttribute = statAttribute;
            StatValue = value;
            this.statType = statType;
        }
        

        public float StatValue
        {
            get { return statValue; }
            set { statValue = value; }
        }

        // public string StatText()
        // {
        //     switch (statType)
        //     {
        //         case StatType.Additive:
        //             return $"<b><color=white>{statValue}</color></b> {statData.statAttributeName}";
        //         case StatType.Percentage:
        //             return $"<b><color=white>{statValue}%</color></b> {statData.statPercentageAttributeName}";
        //     }
        //
        //     return "";
        // }
        
        
        
        
       
        
    }
}