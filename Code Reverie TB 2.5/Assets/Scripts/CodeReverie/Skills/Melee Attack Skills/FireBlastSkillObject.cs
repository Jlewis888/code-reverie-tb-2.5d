using System;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace CodeReverie
{
    public class FireBlastSkillObject : SkillObject
    {
        public List<DamageTypes> damageTypes;
        public FireBlastSkillDamageObject fireBlastSkillDamageObject;
        public GameObject spawnPoint;
        
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerMovementController = GetComponentInParent<PlayerMovementController>();

            if (attackRange <= 0)
            {
                attackRange = 1f;
            }

            impulseSource = GetComponent<CinemachineImpulseSource>();
            
            
            characterUnitSource = GetComponentInParent<CharacterBattleManager>();
        }
        
        public override void Init()
        {
            throw new NotImplementedException();
        }

        public override void Attack()
        {

            if (characterUnitSource == null)
            {
                characterUnitSource = GetComponentInParent<CharacterBattleManager>();
            }
            
            FireBlastSkillDamageObject blastSkillDamageObject= Instantiate(fireBlastSkillDamageObject, spawnPoint.transform.position, transform.rotation);

            blastSkillDamageObject.characterUnitSource = characterUnitSource;
            blastSkillDamageObject.transform.localScale = Vector3.one * 2;
            blastSkillDamageObject.gameObject.SetActive(true);
            
        }

        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     if(other.TryGetComponent(out Health health))
        //     {
        //         
        //         if (other.TryGetComponent(out ComponentTagManager enemyComponentTagManager))
        //         {
        //             if (enemyComponentTagManager.HasTag(ComponentTag.Enemy) &&
        //                 GetComponentInParent<ComponentTagManager>().HasTag(ComponentTag.Player))
        //             {
        //                 DamageProfile damage = new DamageProfile(characterUnitSource, health, damageTypes);
        //             }
        //         }
        //
        //     }
        //     
        // }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}