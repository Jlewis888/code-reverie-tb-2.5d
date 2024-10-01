using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CommandWheelPanelExtensionItems : CommandWheelPanelExtension
    {
        public GameObject commandWheelContainer;
        public CommandWheel commandWheelPF;
        public List<CommandWheel> commandWheels = new List<CommandWheel>();
        public List<Item> characterItems = new List<Item>();
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
            
            characterItems = new List<Item>(PlayerManager.Instance.inventory.items);
            
            SetCommandWheels();
        }

        private void Clear()
        {
            commandWheels = new List<CommandWheel>();
            characterItems = new List<Item>();

            foreach (Transform child in commandWheelContainer.transform)
            {
                Destroy(child.gameObject);
            }
            
        }

        public void SetCommandWheels()
        {
            if (characterItems != null)
            {
                
                Debug.Log(Mathf.Ceil(characterItems.Count / 12f));
                wheelCount = (int)Mathf.Ceil(characterItems.Count / 12f);
            }

            for (int i = 0; i < wheelCount; i++)
            {
                CommandWheel commandWheel = Instantiate(commandWheelPF, commandWheelContainer.transform);

                commandWheel.gameObject.AddComponent<CommandWheelExtensionItem>();
                commandWheel.GetComponent<CommandWheelExtensionItem>();

                
                
                TransferItems(commandWheel);
                commandWheel.InitExtensions();
                
                if (i == 0)
                {
                    commandWheel.active = true;
                }
            }
        }


        public void TransferItems(CommandWheel commandWheel)
        {
            foreach (Item item in characterItems)
            {
                
                if (item.info.combatItem)
                {
                    commandWheel.GetComponent<CommandWheelExtensionItem>().items.Add(item);
                }
                
                
            }
            
            characterItems.RemoveRange(0,Math.Min(12, characterItems.Count));
                
        }
    }
}