using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "New Weapon Info", menuName = "Scriptable Objects/Weapons")]
    public class WeaponInfo : SerializedScriptableObject
    {
        public string name;
        public float damage;
        public float rate;
        public float range;

        public WeaponObject weaponObjectPrefab;
        public Projectile projectileObject;
        
        
    }
}
