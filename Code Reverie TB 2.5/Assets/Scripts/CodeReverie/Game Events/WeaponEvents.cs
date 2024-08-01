using System;

namespace CodeReverie
{
    public class WeaponEvents
    {
        public Action toggleWeapon;

        public void ToggleWeapon()
        {
            toggleWeapon?.Invoke();
        }
        
        
    }
}