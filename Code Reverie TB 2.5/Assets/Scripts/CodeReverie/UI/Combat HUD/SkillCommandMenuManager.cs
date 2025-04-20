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
                //Debug.Log("THis is whaat i am looking for");
                ConfirmAction();
            }
            
            // if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            // {
            //     CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleCommandAction();
            // }

            if (Input.GetKeyDown(KeyCode.D))
            {
                commandMenuNavigation.SelectedNavigationButton
                    .GetComponent<SkillCommandMenuNavigationButton>().SetNextSkill();
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                commandMenuNavigation.SelectedNavigationButton
                    .GetComponent<SkillCommandMenuNavigationButton>().SetPrevSkill();
            }
            
            commandMenuNavigation.NavigationInputUpdate();
        }
        
        public void ConfirmAction()
        {

            Skill selectedSkill = commandMenuNavigation.SelectedNavigationButton
                .GetComponent<SkillCommandMenuNavigationButton>().skill;
            
            if (CombatManager.Instance.selectedPlayerCharacter.currentSkillPoints >= selectedSkill.info.skillPointsCost)
            {

                if (CombatManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>().character
                        .availableResonancePoints >= selectedSkill.info.resonancePointsCost)
                {
                    Debug.Log("We in this bithc");
                    CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Skill;
                    CombatManager.Instance.SetSelectableTargets();
                   
                    CombatManager.Instance.selectedPlayerCharacter.selectedSkill = selectedSkill;
                    //CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu(this);
                    CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandMenu.ToggleTargetMenu();
                }
                else
                {
                    Debug.Log("Not enough Resonance Points");
                }
                
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
            //foreach (Skill skill in BattleManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>().character.characterSkills.equippedActionSkills.Values)
            foreach (Skill skill in CombatManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>().character.characterSkills.learnedSkills)
            {

                if (skill != null)
                {
                    SkillCommandMenuNavigationButton skillCommandMenuNavigationButton =
                        Instantiate(skillCommandMenuNavigationButtonPF, skillCommandMenuNavigationButtonHolder.transform);

                    skillCommandMenuNavigationButton.baseSkill = skill;
                    skillCommandMenuNavigationButton.Init();
                    
                    // skillCommandMenuNavigationButton.skill = skill;
                    // skillCommandMenuNavigationButton.nameText.text = skill.info.skillName;
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
            commandMenuNavigation.scrollRect = scrollRect;
        }
        
    }
}