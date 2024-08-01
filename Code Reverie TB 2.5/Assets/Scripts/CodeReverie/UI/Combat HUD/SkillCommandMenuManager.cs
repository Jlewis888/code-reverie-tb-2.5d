using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class SkillCommandMenuManager : CommandMenuManager
    {
        public GameObject skillCommandMenuNavigationButtonHolder;
        public SkillCommandMenuNavigationButton skillCommandMenuNavigationButtonPF;
        
        
        private void Awake()
        {
            if (commandMenuNavigation == null)
            {
                SetCommandNavigation();
            }

        }
        
        private void OnEnable()
        {
            SetSkillButtons();
            commandMenuNavigation.SetFirstItem();
            //EventManager.Instance.combatEvents.OnPlayerSelectTarget(SelectedNavigationButton.GetComponent<TargetCommandMenuNavigationButton>().characterBattleManager);
        }


        private void Update()
        {
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
            
            commandMenuNavigation.NavigationInputUpdate();
        }
        
        public void ConfirmAction()
        {

            Skill selectedSkill = commandMenuNavigation.SelectedNavigationButton
                .GetComponent<SkillCommandMenuNavigationButton>().skill;
            
            if (BattleManager.Instance.selectedPlayerCharacter.currentSkillPoints >= selectedSkill.info.skillPointsCost)
            {
                BattleManager.Instance.SetSelectableTargets();
                BattleManager.Instance.selectedPlayerCharacter.selectedSkill = selectedSkill;
                CanvasManager.Instance.hudManager.combatHudManager.commandMenu.ToggleTargetMenu();
            }
            else
            {
                Debug.Log("Not enough Skill Points");
            }
        }

        public void SetSkillButtons()
        {
            
            if (commandMenuNavigation == null)
            {
                SetCommandNavigation();
            }
            
            commandMenuNavigation.ClearNavigationList();
            
            Clear();
            foreach (Skill skill in BattleManager.Instance.selectedPlayerCharacter.GetComponent<CharacterController>().character.characterSkills.equippedActionSkills.Values)
            {

                if (skill != null)
                {
                    SkillCommandMenuNavigationButton skillCommandMenuNavigationButton =
                        Instantiate(skillCommandMenuNavigationButtonPF, skillCommandMenuNavigationButtonHolder.transform);


                    skillCommandMenuNavigationButton.skill = skill;
                    skillCommandMenuNavigationButton.nameText.text = skill.info.skillName;
                    commandMenuNavigation.Add(skillCommandMenuNavigationButton);
                }
                
                
            }
        }
        
        public void Clear()
        {
            foreach (Transform child in skillCommandMenuNavigationButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        public void SetCommandNavigation()
        {
            commandMenuNavigation = new CommandMenuNavigation();
        }
        
    }
}