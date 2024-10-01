using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeReverie
{
    
    public class CommandWheelExtensionItem : CommandWheelExtension
    { 
        
        
        public List<Item> items = new List<Item>();
        
        public override void InitExtension()
        {
            int count = 0;
            
            foreach (Item item in items)
            {
                
                
                commandWheel.commandWheelOptions[count].gameObject.AddComponent<CommandWheelOptionExtensionItem>();
                CommandWheelOptionExtensionItem commandWheelOptionExtensionItem = commandWheel.commandWheelOptions[count].GetComponent<CommandWheelOptionExtensionItem>();
                commandWheelOptionExtensionItem.item = item;
                commandWheelOptionExtensionItem.Init();
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
            
            
            CombatManager.Instance.selectedPlayerCharacter.selectedItem = commandWheel.selectedCommandWheelOption.GetComponent<CommandWheelOptionExtensionItem>().item;
            CombatManager.Instance.SetSelectableTargets();
            
        }


        public void SetSkillButtons()
        {
            
        }

    }
}