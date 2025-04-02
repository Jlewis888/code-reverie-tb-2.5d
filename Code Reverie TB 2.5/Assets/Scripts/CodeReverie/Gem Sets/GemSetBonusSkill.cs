using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "GemSetBonusSkill", menuName = "Scriptable Objects/Gem Sets/GemSetBonusSkill",
        order = 1)]
    public class GemSetBonusSkill : GemSetBonus
    {
        public SkillDataContainer skillDataContainer;
    }
}