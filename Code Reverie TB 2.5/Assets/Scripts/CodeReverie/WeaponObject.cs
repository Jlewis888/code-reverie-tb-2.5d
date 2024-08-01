using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class WeaponObject : SerializedMonoBehaviour, IWeapon
    {
        public virtual void Attack(Transform firePoint)
        {
            
        }
        
        
    }
}
