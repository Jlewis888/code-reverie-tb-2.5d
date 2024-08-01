using System.Collections.Generic;

namespace CodeReverie
{
    public class Damage
    {
        public CharacterUnit source;
        public float damageAmount;
        public List<DamageTypes> damageTypes;
        

        public Damage(CharacterUnit source, float damageAmount, List<DamageTypes> damageTypes)
        {
            this.source = source;
            this.damageAmount = damageAmount;
            this.damageTypes = damageTypes;
        }
        
    }
}