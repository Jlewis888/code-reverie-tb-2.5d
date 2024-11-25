using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class FireballSkillObject : SkillObject
    {
        //public CharacterBattleManager characterUnitSource;
        public GameObject fireBallCastPF;
        public FireballSkillProjectile fireballSkillProjectilePF;
        public GameObject spawnPoint;
        public float timer;
        public bool fire;

        private void Awake()
        {
            
            //characterUnitSource = GetComponentInParent<CharacterBattleManager>();
        }

        private void Update()
        {
            if (timer >= 0)
            {
                timer -= Time.fixedDeltaTime;
            }
            else
            {
                // if (fire)
                // {
                //     Attack();
                // }
                
                Attack();
            }
        }


        public override void Init()
        {
            // List<DamageTypes> damageTypes = new List<DamageTypes>();
            // damageTypes.Add(DamageTypes.Fire);
            // DamageProfile damage = new DamageProfile(characterUnitSource, characterUnitSource.target.GetComponent<Health>(), damageTypes);
            // characterUnitSource.EndTurn();
            // Destroy(gameObject);
            // return;
            
            
            GameObject fireBallCastObject = Instantiate(fireBallCastPF, spawnPoint.transform );
            //fireBallCastObject.transform.position = characterUnitSource.transform.position + characterUnitSource.transform.forward * 2f;
            timer = 1.5f;

            // StartCoroutine(characterUnitSource.Rotate(() =>
            // {
            //     timer = 1.5f;
            //     fire = true;
            //     
            // }));
            
            
        }

        public override void Attack()
        {
            
            
            
            fire = false;
            FireballSkillProjectile fireballSkillProjectile = Instantiate(fireballSkillProjectilePF, spawnPoint.transform.position, transform.rotation);
            
            fireballSkillProjectile.source = characterUnitSource;
            fireballSkillProjectile.transform.localScale = Vector3.one * 2;
            fireballSkillProjectile.target = characterUnitSource.target.gameObject;
            fireballSkillProjectile.Init();
            fireballSkillProjectile.gameObject.SetActive(true);
            
            Destroy(gameObject);
            
        }
    }
}