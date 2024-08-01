using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace CodeReverie
{
    public class EnemyActiveWeapon : MonoBehaviour
    {
        public float cooldownTimer;
        
        public Item activeWeaponItem;
        public WeaponObject currentWeapon;
        public Vector2 lookAtRotation;
        public Vector3 aimInput;
        public bool canMove;
        public float angle;
        public float rotationSpeed;
        
        

        public void Attack()
        {
            if (cooldownTimer <= 0)
            {
                    
                if (currentWeapon != null)
                {
                      
                    currentWeapon.Attack(transform);
                    //cooldownTimer = ActiveWeaponItem.info.weaponInfo.rate;
                }
                
            }
        }
        
        
        
        
    }
}
