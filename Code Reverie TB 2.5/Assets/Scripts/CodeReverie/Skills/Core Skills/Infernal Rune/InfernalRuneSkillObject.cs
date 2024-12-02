using System;
using UnityEngine;

namespace CodeReverie
{
    public class InfernalRuneSkillObject : SkillObject
    {
        
        public InfernalRuneGameObject infernalRuneGameObjectPF;
        public GameObject spawnPoint;

        private void Awake()
        {
            
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
            
            
            InfernalRuneGameObject infernalRuneGameObject = Instantiate(infernalRuneGameObjectPF, spawnPoint.transform.position, transform.rotation);

            //fireballSkillProjectile.source = characterUnitSource;
            infernalRuneGameObject.transform.localScale = Vector3.one * 2;
            infernalRuneGameObject.gameObject.SetActive(true);
            
        }

        public override void OnSkillEnd()
        {
            throw new NotImplementedException();
        }
    }
}