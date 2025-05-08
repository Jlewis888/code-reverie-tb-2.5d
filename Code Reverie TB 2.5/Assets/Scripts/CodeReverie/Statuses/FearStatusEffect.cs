using System.Collections.Generic;

namespace CodeReverie
{
    public class FearStatusEffect : StatusEffect
    {
        
        public FearStatusEffect(int duration = 1) : base()
        {
            attribute = StatusEffectAttributeType.Burn;
            isPermanent = false;
            stacks = duration;
            this.duration = duration;
            
        }

        public override void TriggerStatusEffect()
        {
            Stat stat = new Stat(StatAttribute.Haste, -1, StatType.Additive);
            StatModifier statModifier = new StatModifier(stat, this);
            source.target.GetComponent<CharacterUnitController>().character.activeStatusEffects.Add(this);
            source.target.GetComponent<CharacterUnitController>().character.characterStats.tempStatModifiers.Add(statModifier);
        }
    }
}