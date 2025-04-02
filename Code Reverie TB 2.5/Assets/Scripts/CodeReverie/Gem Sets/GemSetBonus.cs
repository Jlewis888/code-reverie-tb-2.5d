using Sirenix.OdinInspector;

namespace CodeReverie
{
    public abstract class GemSetBonus : SerializedScriptableObject
    {
        public GemSetBonusType gemSetBonusType;
        public StatAttribute statAttribute;
        public float value;
        public StatType statType;
        
        public virtual Stat GetStat()
        {
            return StatsManager.Instance.CreateStat(statAttribute, value, statType);
        }
    }
}