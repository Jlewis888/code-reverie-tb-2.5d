using System.Collections.Generic;

namespace CodeReverie
{
    public abstract class StatusEffect
    {
        public CharacterBattleManager source;
        public CharacterBattleManager target;
        public StatusEffectAttributeType attribute;
        public int stacks;
        public int duration;
        public bool isPermanent;
        public List<DamageTypes> damageTypes = new List<DamageTypes>();


        public StatusEffect()
        {
            
        }

        public StatusEffect(StatusEffectAttributeType statusEffectAttributeType, int stacks = 1, int duration = 1,
            bool isPermanent = false)
        {
            attribute = statusEffectAttributeType;
            this.stacks = stacks;
            this.duration = duration;
            this.isPermanent = isPermanent;
        }

        public virtual void TriggerStatusEffect()
        {
            
        }
    }
}