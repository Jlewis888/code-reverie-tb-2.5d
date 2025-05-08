using UnityEngine;

namespace CodeReverie
{
    public class MannequinModel2TransformSkill : Skill
    {
        public MannequinModel2TransformSkill(SkillDataContainer skillDetails) : base(skillDetails)
        {
        }
        
        public override void UseSkill()
        {
            base.UseSkill();

            CharacterDataContainer characterDataContainer = CharacterManager.Instance.GetCharacterByCharacterId("Mannequin Doll model 2 full");
            CombatManager.Instance.InstantiateNewEnemy(characterDataContainer, source.transform.position);
            
           
            // source.transform.GetChild(0).gameObject.SetActive(false);
            // source.transform.GetChild(1).gameObject.SetActive(true);
            // source.transform.GetChild(0).gameObject.SetActive(false);
            
            CombatManager.Instance.combatManagerState = CombatManagerState.OnSkillUseEnd;
            //source.EndTurn();
            // source.DequeueAction();
            CombatManager.Instance.DestroyCharacterBattleManager(source);
            // GameObject.DestroyImmediate(source);
            
            //PlaySkillAnimation();
            
            // SkillObject skillObject = GameObject.Instantiate(info.skillGameObject, source.castObjectHolder.transform);
            // skillObject.characterUnitSource = source;
            // skillObject.Init();

            //PlayerController.Instance.activeCharacterController.GetComponent<AnimationManager>().ChangeAnimationState(info.initialAnimation);
        }

        public override void SubscribeSkillListeners()
        {
            throw new System.NotImplementedException();
        }

        public override void UnsubscribeSkillListeners()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonDown()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonDownAttacking()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonHold()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonUp()
        {
            throw new System.NotImplementedException();
        }

        public override void OnButtonUpAttacking()
        {
            throw new System.NotImplementedException();
        }
    }
}