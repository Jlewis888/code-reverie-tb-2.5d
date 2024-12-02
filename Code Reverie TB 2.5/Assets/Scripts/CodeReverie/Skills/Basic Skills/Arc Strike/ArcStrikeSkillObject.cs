using System;
using Unity.Mathematics;
using UnityEngine;

namespace CodeReverie
{
    public class ArcStrikeSkillObject : SkillObject
    {
        public ArcStrikeGameObject arcStrikeGameObjectPF;
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

            ArcStrikeGameObject arcStrikeGameObject =
                Instantiate(arcStrikeGameObjectPF, spawnPoint.transform.position, quaternion.identity);
            
            
            arcStrikeGameObject.characterUnitSource = characterUnitSource;
            //arcStrikeGameObject.transform.localScale = Vector3.one * 2;
            arcStrikeGameObject.gameObject.SetActive(true);

        }

        public override void OnSkillEnd()
        {
            throw new NotImplementedException();
        }
    }
}