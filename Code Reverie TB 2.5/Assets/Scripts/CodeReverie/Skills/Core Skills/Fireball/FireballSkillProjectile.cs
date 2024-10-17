using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class FireballSkillProjectile : SerializedMonoBehaviour
    {
        Rigidbody rb;
        public CharacterBattleManager source;
        public GameObject target;
        private Vector3 targetDirection;
        public float speed;
        public Vector2 Velocity;
        public GameObject ExplosionPrefab;
        public float DestroyExplosion = 4.0f;
        public float DestroyChildren = 2.0f;
        
        private void Awake()
        {
            speed = 5f;
        }
        
        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            //rb.velocity = Velocity;

        }
        
        
        private void Update()
        {
            // transform.position += transform.forward * Time.deltaTime * speed;
            
            transform.position = Vector3.MoveTowards(transform.position,
                target.transform.position,
                speed * Time.deltaTime);
            
            
            if (Vector3.Distance(transform.position, target.transform.position) <= 1f)
            {
                Debug.Log("THis is complete");
                List<DamageTypes> damageTypes = new List<DamageTypes>();
                damageTypes.Add(DamageTypes.Fire);
                DamageProfile damage = new DamageProfile(source, source.target.GetComponent<Health>(), damageTypes);
                var exp = Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);
                Destroy(exp, DestroyExplosion);
                EventManager.Instance.combatEvents.OnSkillComplete(source);
                source.EndTurn();
                Destroy(gameObject);
            }
        }

        public void Init()
        {
            RotateToTarget();
        }

        public void RotateToTarget()
        {
            targetDirection = target.transform.position - transform.position;

            targetDirection.y = target.transform.position.y;

            transform.forward = targetDirection;
        }
        
    }
}