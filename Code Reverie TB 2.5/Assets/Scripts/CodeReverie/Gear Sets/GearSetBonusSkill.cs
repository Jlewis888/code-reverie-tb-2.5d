using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "GearSetBonusSkill", menuName = "Scriptable Objects/Gear Sets/GearSetBonusSkill",
        order = 1)]
    public class GearSetBonusSkill : GearSetBonus
    {
        public SkillDataContainer skillDataContainer;
    }
}