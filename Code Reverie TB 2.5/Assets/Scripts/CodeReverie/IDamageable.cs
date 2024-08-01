namespace CodeReverie
{
    public interface IDamageable
    {
        void ApplyDamage(DamageProfile damageProfile);
        void ApplyHeal(float amount);
    }
}