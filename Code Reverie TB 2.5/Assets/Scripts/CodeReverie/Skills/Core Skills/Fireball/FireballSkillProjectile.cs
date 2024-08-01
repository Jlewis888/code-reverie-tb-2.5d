using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class FireballSkillProjectile : SerializedMonoBehaviour
    {
        public CharacterBattleManager source;
        public GameObject target;
        private Vector2 targetDirection;
        public float speed;

        private void Awake()
        {
            speed = 5f;
        }


        private void Update()
        {
            transform.position += transform.right * Time.deltaTime * speed;
            
            if (Vector3.Distance(transform.position, target.transform.position) <= 1f)
            {
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

            transform.right = targetDirection;
        }
        
    }
}