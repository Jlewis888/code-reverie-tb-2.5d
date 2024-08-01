using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CodeReverie
{
    public class PinkSlimeAttack : EnemyAction
    {
        private Vector3 direction;
        private Vector3 targetPosition;
        public float speed = 7f;
        private bool attacking;
        public float attackLength;
        List<Health> damagedTarget = new List<Health>();
        public SharedVector3 sharedTargetPosition;
        

        public override void OnStart()
        {
            
            targetPosition = PlayerManager.Instance.GetCurrentCharacterPosition();
            //direction = targetPosition - transform.position;
            direction = sharedTargetPosition.Value - transform.position;
            direction.Normalize();
            damagedTarget = new List<Health>();
            
            
            enemyAI.attackIndicatorsManager.CloseAllIndicators();
            Attack();
        }


        public override TaskStatus OnUpdate()
        {
            
            // Collider2D[] targetsDetected = Physics2D.OverlapCircleAll(transform.position, 3f);
            //
            // foreach (Collider2D targets in targetsDetected)
            // {
            //     
            //     if (targets.TryGetComponent(out ComponentTagManager componentTagManager))
            //     {
            //
            //         if (componentTagManager.HasTag(ComponentTag.Player))
            //         {
            //             new DamageProfile(GetComponent<CharacterBattleManager>(), targets.GetComponent<Health>(),
            //                 new List<DamageTypes>());
            //         }
            //         
            //        
            //     }
            // }


            if (attacking)
            {
                
                return TaskStatus.Running;
            }
            
            GetComponent<AnimationManager>().ChangeAnimationState("idle");
            return TaskStatus.Success;
        }


        public override void OnFixedUpdate()
        {
            float deltaSpeed = speed * Time.fixedDeltaTime;

            GetComponent<Rigidbody2D>().MovePosition(transform.position + direction * deltaSpeed );
            
            
            // transform.Translate(direction.x * deltaSpeed, direction.y * deltaSpeed, direction.z * deltaSpeed,
            //     Space.World);
        }
        
        
        public void Attack()
        {
            StartCoroutine(MeleeAttack());
        }
        
        
        public IEnumerator MeleeAttack()
        {
            

            attacking = true;
            bool screenShake = false;
            
            List<Health> damagedTarget = new List<Health>();
            
            while (attacking)
            {
                //yield return null;
                // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 3f);
                //
                //  foreach (Collider2D enemy in hitEnemies)
                //  {
                //
                //      if (enemy.TryGetComponent(out ComponentTagManager enemyComponentTagManager))
                //      {
                //          if (enemyComponentTagManager.HasTag(ComponentTag.Player))
                //          {
                //              if (enemy.TryGetComponent(out Health health) && !damagedTarget.Contains(enemy.GetComponent<Health>()))
                //              {
                //                  new DamageProfile(GetComponent<CharacterBattleManager>(), health,
                //                      new List<DamageTypes>());
                //                  damagedTarget.Add(health);
                //
                //                  
                //              }
                //          }
                //      
                //
                //      }
                //  } 
                 
                yield return new WaitForSeconds(attackLength);
                attacking = false;

            }
            
            
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (attacking)
            {
                if (other.TryGetComponent(out ComponentTagManager enemyComponentTagManager))
                {
                    if (enemyComponentTagManager.HasTag(ComponentTag.Player))
                    {
                        if (other.TryGetComponent(out Health health) && !damagedTarget.Contains(other.GetComponent<Health>()))
                        {
                            new DamageProfile(GetComponent<CharacterBattleManager>(), health,
                                new List<DamageTypes>());
                            
                            damagedTarget.Add(health);

                        }
                    }
                     

                }
            }
            
            //base.OnTriggerEnter2D(other);
        }
    }
}