using Unity.Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public abstract class MeleeSkillObject : SerializedMonoBehaviour
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
        
        
        public abstract void Attack();
    }
}