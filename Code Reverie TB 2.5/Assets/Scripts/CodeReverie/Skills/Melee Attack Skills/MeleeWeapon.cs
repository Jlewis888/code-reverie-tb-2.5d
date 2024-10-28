using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace CodeReverie
{
    public class MeleeWeapon : WeaponObject, IWeapon
    {

        public Animator animator;
        public PlayerMovementController playerMovementController;
        public Transform attackPoint;
        public float attackRange = 1f;
        public bool attacking;
        private CinemachineImpulseSource impulseSource;
        public float impulseForce = 1f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerMovementController = GetComponentInParent<PlayerMovementController>();

            if (attackRange <= 0)
            {
                attackRange = 1f;
            }

            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public override void Attack(Transform firePoint)
        {
            // PlayerController.Instance.GetComponent<PlayerMovementController>().characterMovementState =
            //     CharacterMovementState.AttackMoving;
            //
            // StartCoroutine(MeleeAttack());
        }

        public IEnumerator MeleeAttack()
        {
            animator.SetTrigger("Attack");

            attacking = true;
            bool screenShake = false;
            
            List<Health> damagedTarget = new List<Health>();
            
            while (attacking)
            {
                //yield return null;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
                
                 foreach (Collider2D enemy in hitEnemies)
                 {

                     if (enemy.TryGetComponent(out ComponentTagManager enemyComponentTagManager))
                     {
                         if (enemyComponentTagManager.HasTag(ComponentTag.Player) &&
                             GetComponentInParent<ComponentTagManager>().HasTag(ComponentTag.Player))
                         {
                             continue;
                         }
                     
                         if (enemyComponentTagManager.HasTag(ComponentTag.Enemy) &&
                             GetComponentInParent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                         {
                             continue;
                         }      
                         
                     }
                     
                     if (enemy.TryGetComponent(out Health health) && !damagedTarget.Contains(enemy.GetComponent<Health>()) && enemy.GetComponent<CharacterUnitController>().character.characterState != CharacterState.Dead)
                     {
                         new DamageProfile(GetComponentInParent<CharacterBattleManager>(), health,
                             new List<DamageTypes>());
                         damagedTarget.Add(health);

                         if (!screenShake)
                         {
                             impulseSource.GenerateImpulseWithForce(impulseForce);
                             screenShake = true;
                         }
                         
                         //impulseSource.GenerateImpulse();

                     }
                 } 
                 
                yield return null;
                
            }
            
            
        }

        public void StopAttack()
        {
            // attacking = false;
            // PlayerController.Instance.GetComponent<PlayerMovementController>().characterMovementState =
            //     CharacterMovementState.Idle;
            // gameObject.SetActive(false);
            
           
        }
        
        void AttackMovementStart()
        {
            // if (characterUnit.GetComponent<PlayerMovementController>().isActiveAndEnabled)
            // {
            //     characterUnit.GetComponent<PlayerMovementController>().playerMovementState = CharacterMovementState.AttackMoving;
            // }
            Debug.Log("you");
            
            
        }
        
        void AttackMovementEnd()
        {
            // if (characterUnit.GetComponent<PlayerMovementController>().isActiveAndEnabled)
            // {
            //     characterUnit.GetComponent<PlayerMovementController>().playerMovementState = CharacterMovementState.Moving;
            // }
            Debug.Log("you 2");
            
            // PlayerController.Instance.GetComponent<PlayerMovementController>().characterMovementState =
            //     CharacterMovementState.Idle;
        }
        
        
        void OnDrawGizmosSelected()
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        
    }
}
