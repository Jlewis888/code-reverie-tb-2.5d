using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class Projectile : SerializedMonoBehaviour
    {
        public CharacterBattleManager source;
        public float speed;
        public GameObject projectileGameObject;
        public GameObject impactGameObject;
        private CinemachineImpulseSource impulseSource;
        public float impulseForce = 1f;
        
        [SerializeField] private float projectileRange = 10f;
        private Vector3 startPosition;

        private void Awake()
        {
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }


        // Start is called before the first frame update
        void Start()
        {
            startPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            // MoveProjectile();
            // DetectFireDistance();
        }
        
        public void UpdateMoveSpeed(float moveSpeed)
        {
            speed = moveSpeed;
        }
        
        
        private void MoveProjectile()
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }

        void DetectFireDistance()
        {
            if (Vector3.Distance(transform.position, startPosition) > projectileRange) {
                Destroy(gameObject);
            }
        }

        public float ProjectileRange
        {
            get { return projectileRange; }

            set
            {
                projectileRange = value;
            }
        }
        
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // if (other.tag == "Enemy" && source.GetComponent<PlayerCharacterUnit>())
            // {
            //     if (other.TryGetComponent(out CharacterUnit characterUnit))
            //     {
            //         projectileGameObject.SetActive(false);
            //         impactGameObject.SetActive(true);
            //         speed = 0;
            //         //characterUnit.ApplyDamage(damage);
            //         StartCoroutine(ApplyKnockBack(characterUnit));
            //         Destroy(gameObject, 3f);
            //     }
            // }
            
            if (other.TryGetComponent(out ComponentTagManager otherComponentTagManager))
            {
                
                if (otherComponentTagManager.HasTag(ComponentTag.Enemy) && source.GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                {
                    
                    
                    if (other.TryGetComponent(out Health health) && other.GetComponent<CharacterController>().character.characterState != CharacterState.Dead)
                    {
                        //impulseSource.GenerateImpulseWithForce(impulseForce);
                        projectileGameObject.SetActive(false);
                        impactGameObject.SetActive(true);
                        speed = 0;
                        
                        new DamageProfile(source, health, new List<DamageTypes>());
                        //StartCoroutine(ApplyKnockBack(characterUnit));
                        Destroy(gameObject, 3f);

                    }
                    
                    
                }
                
                
                if (otherComponentTagManager.HasTag(ComponentTag.Player) && source.GetComponent<ComponentTagManager>().HasTag(ComponentTag.Enemy))
                {
                    
                    
                    if (other.TryGetComponent(out Health health) && other.GetComponent<CharacterController>().character.characterState != CharacterState.Dead)
                    {

                        if (health.canTakeDamage)
                        {
                            //impulseSource.GenerateImpulseWithForce(impulseForce);
                            projectileGameObject.SetActive(false);
                            impactGameObject.SetActive(true);
                            speed = 0;
                        
                            new DamageProfile(source, health, new List<DamageTypes>());
                            //StartCoroutine(ApplyKnockBack(characterUnit));
                            Destroy(gameObject, 3f);
                        }
                        
                    }
                }

                if (otherComponentTagManager.HasTag(ComponentTag.Environment))
                {
                    Destroy(gameObject);
                }
                
            }
        }
        
    }
}
