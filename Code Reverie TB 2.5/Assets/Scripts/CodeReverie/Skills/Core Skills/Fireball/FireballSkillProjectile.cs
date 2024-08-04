using System;
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
            transform.position += transform.forward * Time.deltaTime * speed;
            
            if (Vector3.Distance(transform.position, target.transform.position) <= 1f)
            {
                Debug.Log("THis is complete");
                EventManager.Instance.combatEvents.OnSkillComplete(source);
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

            transform.forward = targetDirection;
        }
        
    }
}