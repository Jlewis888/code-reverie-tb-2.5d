using Unity.Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public abstract class SkillObject : SerializedMonoBehaviour
    {
        public Animator animator;
        public CharacterBattleManager characterUnitSource;
        public PlayerMovementController playerMovementController;
        public Transform attackPoint;
        public float attackRange = 1f;
        public bool attacking;
        protected CinemachineImpulseSource impulseSource;
        public float impulseForce = 1f;
        
        public abstract void Init();
        public abstract void Attack();

        public abstract void OnSkillEnd();
    }
}