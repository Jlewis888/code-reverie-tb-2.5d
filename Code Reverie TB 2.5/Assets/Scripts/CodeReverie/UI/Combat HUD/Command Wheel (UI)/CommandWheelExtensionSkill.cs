using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeReverie
{
    
    public class CommandWheelExtensionSkill : CommandWheelExtension
    { 
        
        
        public List<Skill> skills = new List<Skill>();
        
        public override void InitExtension()
        {
            int count = 0;
            
            foreach (Skill skill in skills)
            {
                
                
                commandWheel.commandWheelOptions[count].gameObject.AddComponent<CommandWheelOptionExtensionSkill>();
                CommandWheelOptionExtensionSkill commandWheelOptionExtensionSkill = commandWheel.commandWheelOptions[count].GetComponent<CommandWheelOptionExtensionSkill>();
                commandWheelOptionExtensionSkill.skill = skill;
                commandWheelOptionExtensionSkill.Init();
                count++;

            }
        }


        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm Action"))
            {
                ConfirmAction();
            }
        }

        public void ConfirmAction()
        {
            // if (commandWheel.selectedCommandWheelOption == attackCommandWheelOption)
            // {
            //     CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Attack;
            //     CombatManager.Instance.SetSelectableTargets();
            //     CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.commandWheelPanelManager
            //         .previousCommandWheelPanel = GetComponentInParent<CommandWheelPanel>();
            //     gameObject.SetActive(false);
            //     //CanvasManager.Instance.screenSpaceCanvasManager.hudManager.commandMenu.combatCommandMenu.ToggleTargetMenu(this);
            // } 
            
            Skill selectedSkill = commandWheel.selectedCommandWheelOption.GetComponent<CommandWheelOptionExtensionSkill>().skill;
            
            if (CombatManager.Instance.selectedPlayerCharacter.currentSkillPoints >= selectedSkill.info.skillPointsCost)
            {

                if (CombatManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>().character
                        .availableResonancePoints >= selectedSkill.info.resonancePointsCost)
                {
                    CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Skill;
                    CombatManager.Instance.selectedPlayerCharacter.selectedSkill = selectedSkill;
                    CombatManager.Instance.SetSelectableTargets();
                }
                
            }
            else
            {
                Debug.Log("Not enough Skill Points");
            }
            
        }


        public void SetSkillButtons()
        {
            
        }

    }
}