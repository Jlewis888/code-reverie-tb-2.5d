using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class ArcStrikeGameObject : SerializedMonoBehaviour
    {
        
        public CharacterBattleManager characterUnitSource;
        public List<DamageTypes> damageTypes;

        private void Awake()
        {
            
        }

        private void Update()
        {
            if (TryGetComponent(out ParticleSystem particleSystem))
            {
                if (!particleSystem.IsAlive())
                {
                    DestroyObject();
                }
            }
           
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Health health))
            {
                
                if (other.TryGetComponent(out ComponentTagManager enemyComponentTagManager))
                {
                    
                    if (enemyComponentTagManager.HasTag(ComponentTag.Enemy) &&
                        characterUnitSource.GetComponent<ComponentTagManager>().HasTag(ComponentTag.Player))
                    {
                        DamageProfile damage = new DamageProfile(characterUnitSource, health, damageTypes);
                    }
                }

            }
            
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}