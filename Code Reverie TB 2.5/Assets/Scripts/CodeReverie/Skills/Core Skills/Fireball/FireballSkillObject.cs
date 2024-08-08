using System;
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
                if (fire)
                {
                    Attack();
                }
            }
        }


        public override void Init()
        {
            GameObject fireBallCastObject = Instantiate(fireBallCastPF, spawnPoint.transform );
            //fireBallCastObject.transform.position = characterUnitSource.transform.position + characterUnitSource.transform.forward * 2f;
            

            StartCoroutine(characterUnitSource.Rotate(() =>
            {
                timer = 1.5f;
                fire = true;
                
            }));
            
            
        }

        public override void Attack()
        {
            fire = false;
            FireballSkillProjectile fireballSkillProjectile = Instantiate(fireballSkillProjectilePF, spawnPoint.transform.position, transform.rotation);
            
            fireballSkillProjectile.source = characterUnitSource;
            fireballSkillProjectile.transform.localScale = Vector3.one * 2;
            fireballSkillProjectile.target = characterUnitSource.selectedTargets[0].gameObject;
            fireballSkillProjectile.Init();
            fireballSkillProjectile.gameObject.SetActive(true);
            
            Destroy(gameObject);
            
        }
    }
}