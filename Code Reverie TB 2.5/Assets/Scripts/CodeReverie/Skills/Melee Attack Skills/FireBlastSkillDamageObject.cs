using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace CodeReverie
{
    public class FireBlastSkillDamageObject : SerializedMonoBehaviour
    {
        public CharacterBattleManager characterUnitSource;
        public List<DamageTypes> damageTypes;
        
        
        // private void Awake()
        // {
        //     characterUnitSource = GetComponentInParent<CharacterController>();
        // }

       

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Health health))
            {
                
                if (other.TryGetComponent(out ComponentTagManager enemyComponentTagManager))
                {
                    // if (enemyComponentTagManager.HasTag(ComponentTag.Enemy) &&
                    //     GetComponentInParent<ComponentTagManager>().HasTag(ComponentTag.Player))
                    // {
                    //     DamageProfile damage = new DamageProfile(characterUnitSource, health, damageTypes);
                    // }
                    
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