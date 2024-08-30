using System.Collections.Generic;

namespace CodeReverie
{
    public class Stat
    {
       
        public StatAttribute statAttribute;
        public float statValue;
        public StatType statType;
        
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
        
    }
}