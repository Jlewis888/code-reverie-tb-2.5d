using Sirenix.OdinInspector;

namespace CodeReverie
{
    
    public abstract class SkillModifier
    {
        public bool isActive;

        public abstract void ActivateSkillModifier();

        public abstract void DeactivateSkillModifier();
    }
}