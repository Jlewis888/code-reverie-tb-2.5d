using System;
using UnityEngine;

namespace CodeReverie
{
    public class RevolverSkillObject : SkillObject
    {
        public Projectile projectilePF;
        public Transform firePoint;
        
        public override void Init()
        {
            throw new NotImplementedException();
        }
        
        public override void Attack()
        {
            Debug.Log("Fire Revolver Projectiles");
            FireProjectileShot();
        }
        
        
        public void FireProjectileShot()
        {
            Projectile projectile = Instantiate(projectilePF, firePoint.position, firePoint.rotation);

            projectile.source = GetComponentInParent<CharacterBattleManager>();
            
            projectile.gameObject.SetActive(true);
            
        }
    }
}