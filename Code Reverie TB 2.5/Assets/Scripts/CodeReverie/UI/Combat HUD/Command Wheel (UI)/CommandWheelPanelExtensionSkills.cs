using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CommandWheelPanelExtensionSkills : CommandWheelPanelExtension
    {
        public GameObject commandWheelContainer;
        public CommandWheel commandWheelPF;
        public List<CommandWheel> commandWheels = new List<CommandWheel>();
        public List<Skill> characterSkills = new List<Skill>();
        public List<Skill> skills = new List<Skill>();
        public int wheelCount;
        public int navigationIndex;
        
        
        // private void OnEnable()
        // {
        //     
        //     Clear();
        //     
        //     skills = new List<Skill>(CombatManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>()
        //         .character.characterSkills.learnedSkills);
        //
        //     characterSkills = new List<Skill>(CombatManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>()
        //         .character.characterSkills.learnedSkills);
        //     
        //     SetCommandWheels();
        // }

        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {
                EventManager.Instance.combatEvents.OnActionCommandWheelSelect();
            }
        }


        public override void InitExtension()
        {
            Clear();
            
            skills = new List<Skill>();
            
            skills.AddRange(CombatManager.Instance.selectedPlayerCharacter
                .GetComponent<CharacterUnitController>()
                .character.equippedArchetype.learnedSkills);
            
            skills.AddRange(CombatManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>()
                .character.characterSkills.learnedSkills);
            

            characterSkills = new List<Skill>(CombatManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>()
                .character.characterSkills.learnedSkills);
            
            SetCommandWheels();
        }

        private void Clear()
        {
            commandWheels = new List<CommandWheel>();
            characterSkills = new List<Skill>();
            skills = new List<Skill>();

            foreach (Transform child in commandWheelContainer.transform)
            {
                Destroy(child.gameObject);
            }
            
        }

        public void SetCommandWheels()
        {
            if (skills != null)
            {
                
                Debug.Log(Mathf.Ceil(skills.Count / 12f));
                wheelCount = (int)Mathf.Ceil(skills.Count / 12f);
            }

            for (int i = 0; i < wheelCount; i++)
            {
                CommandWheel commandWheel = Instantiate(commandWheelPF, commandWheelContainer.transform);

                commandWheel.gameObject.AddComponent<CommandWheelExtensionSkill>();
                commandWheel.GetComponent<CommandWheelExtensionSkill>();

                
                
                TransferSkills(commandWheel);
                commandWheel.InitExtensions();
                
                if (i == 0)
                {
                    commandWheel.active = true;
                }
            }
        }


        public void TransferSkills(CommandWheel commandWheel)
        {
            foreach (Skill skill in skills)
            {

                if (skill != null)
                {
                    commandWheel.GetComponent<CommandWheelExtensionSkill>().skills.Add(skill);
                }
            }
            
            skills.RemoveRange(0,Math.Min(12, skills.Count));
                
        }
    }
}