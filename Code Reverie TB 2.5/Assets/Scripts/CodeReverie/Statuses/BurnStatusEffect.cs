using System.Collections.Generic;

namespace CodeReverie
{
    public class BurnStatusEffect : StatusEffect
    {
        
        public BurnStatusEffect(StatusEffectAttributeType statusEffectAttributeType, int stacks = 1, int duration = 1, bool isPermanent = false) : base(statusEffectAttributeType, stacks, duration, isPermanent)
        {
            attribute = StatusEffectAttributeType.Burn;
            this.isPermanent = false;
            this.stacks = stacks;
            this.duration = duration;
            damageTypes = new List<DamageTypes>{DamageTypes.Fire};
        }

        public override void TriggerStatusEffect()
        {
            DamageProfile damage = new DamageProfile(this);
            stacks -= 0;
        }
    }
}