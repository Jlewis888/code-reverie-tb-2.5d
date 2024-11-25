using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace CodeReverie
{
    public class FireBlastSkillObject : SkillObject
    {
        public List<DamageTypes> damageTypes;
        public ParticleSystem explosionParticleSystem;
        public bool isExplosionParticleSystemPlaying;
        public float applyDamageTimer;
        public bool damageApplied;
        
        private void Awake()
        {
            applyDamageTimer = 2f;
        }

        private void Update()
        {
            // if (explosionParticleSystem.isPlaying)
            // {
            //     Debug.Log("Explosion is playing");
            // }

            if (applyDamageTimer > 0)
            {
                applyDamageTimer -= Time.deltaTime;
            }
            else
            {
                if (!damageApplied)
                {
                    Attack();
                }
                
            }
            
            
        }

        private void OnParticleSystemStopped()
        {
            Debug.Log("This has stopped");
        }

        public override void Init()
        {
           
        }

        public override void Attack()
        {
            damageApplied = true;
            // Debug.Log("Apply Damage");
            // return;
            
            if (characterUnitSource == null)
            {
                Debug.LogError("No Character Unit Source");
                return;
            }
            
            
            foreach (CharacterBattleManager characterBattleManager in CombatManager.Instance.selectedSkillTargets)
            {
                DamageProfile damage = new DamageProfile(characterUnitSource, characterBattleManager, damageTypes);
            }
            
            
        }
        

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}