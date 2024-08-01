using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "GearSetBonusPercentageStat", menuName = "Scriptable Objects/Gear Sets/GearSetBonusPercentageStat",
        order = 1)]
    public class GearSetBonusPercentageStat : GearSetBonus
    {
       
        // public float value;
        //
        // public StatPercentageAttributeTypes statPercentageAttributeType;
        //
        // public StatPercentageAttributeTypes GetPercentageAttributeTypes()
        // {
        //     return statPercentageAttributeType;
        // }
        //
        // public override Stat GetStat()
        // {
        //     return StatsManager.Instance.CreateStat(GetPercentageAttributeTypes(), value);
        // }
    }
}