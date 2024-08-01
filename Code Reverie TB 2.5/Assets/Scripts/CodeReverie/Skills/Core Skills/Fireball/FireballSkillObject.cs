using System;
using UnityEngine;

namespace CodeReverie
{
    public class FireballSkillObject : SkillObject
    {
        //public CharacterBattleManager characterUnitSource;
        public FireballSkillProjectile fireballSkillProjectilePF;
        public GameObject spawnPoint;

        private void Awake()
        {
            
            //characterUnitSource = GetComponentInParent<CharacterBattleManager>();
        }


        public override void Init()
        {
            FireballSkillProjectile fireballSkillProjectile = Instantiate(fireballSkillProjectilePF, spawnPoint.transform.position, transform.rotation);

            fireballSkillProjectile.source = characterUnitSource;
            fireballSkillProjectile.transform.localScale = Vector3.one * 2;
            fireballSkillProjectile.target = characterUnitSource.selectedTargets[0].gameObject;
            fireballSkillProjectile.Init();
            fireballSkillProjectile.gameObject.SetActive(true);
        }

        public override void Attack()
        {
            // if (characterUnitSource == null)
            // {
            //     characterUnitSource = GetComponentInParent<CharacterBattleManager>();
            // }
            //
            //
            // FireballSkillProjectile fireballSkillProjectile = Instantiate(fireballSkillProjectilePF, spawnPoint.transform.position, transform.rotation);
            //
            // fireballSkillProjectile.source = characterUnitSource;
            // fireballSkillProjectile.transform.localScale = Vector3.one * 2;
            // fireballSkillProjectile.gameObject.SetActive(true);
            
        }
    }
}