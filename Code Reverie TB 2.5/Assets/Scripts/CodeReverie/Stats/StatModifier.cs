namespace CodeReverie
{
    public class StatModifier
    {
        public Stat stat;
        public float value;
        public int duration;
        public bool isPermanent;
        public StatusEffect statusEffect;
    
        
        public StatModifier(Stat stat, int duration, bool isPermanent = false)
        {
            this.stat = stat;
            this.duration = duration;
            this.isPermanent = isPermanent;
        }

        public StatModifier(Stat stat, StatusEffect statusEffect)
        {
            this.stat = stat;
            this.statusEffect = statusEffect;
            isPermanent = true;
            duration = 1000;

        }
        
        
        //
        // public void ChangeStat(StatAttribute newStat)
        // {
        //     //stat = newStat;
        // }
        //
        // public void ChangeStatValue(float newValue)
        // {
        //     value = newValue;
        // }
        //
        // public void UpdateDuration(float durationUpdate)
        // {
        //     duration += durationUpdate;
        //     
        // }
        //
        // public void UpdateModifier(StatAttribute stat, float value)
        // {
        //     this.stat = stat;
        //     this.value = value;
        // }
        
    }
}