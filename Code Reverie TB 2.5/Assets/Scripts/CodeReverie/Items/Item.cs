using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CodeReverie
{
    
    //todo make Item Abstract
    public class Item
    {
        public ItemInfo info;
        public List<Stat> stats = new List<Stat>();
        public int amount;
        public List<SkillSlot> skillSlots;

        public Item(ItemInfo itemInfo)
        {
            info = itemInfo;
            skillSlots = info.skillSlots;
        }
        
        public virtual void UseItem()
        {
            if (String.IsNullOrEmpty(info.onUse))
            {
                return;
            }
            
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(info.onUse);
            theMethod.Invoke(this, info.onUseParameters.ToArray());
            
            if (info.consumeOnUse)
            {
                RemoveAmount(1);
            }
        }
        
        public virtual void UseItem(ItemUseSectionType itemUseSectionType)
        {
            if (String.IsNullOrEmpty(info.onUse))
            {
                return;
            }

            object[] onUseParameters = info.onUseParameters.ToArray();
            onUseParameters = onUseParameters.Append(itemUseSectionType).ToArray();
            
            
            Type thisType = this.GetType();
            MethodInfo onUseMethod = thisType.GetMethod(info.onUse);
            ParameterInfo[] onUseParametersList = onUseMethod.GetParameters();
            
            //if(onUseParametersList.Contains())
            
            onUseMethod.Invoke(this, onUseParameters);
            
            if (info.consumeOnUse)
            {
                RemoveAmount(1);
            }
        }
        
        
        public virtual void UseMenuItem()
        {
            if (info.consumeOnUse)
            {
                RemoveAmount(1);
            }
        }
        
        public virtual void UseCombatItem(CharacterBattleManager characterBattleManager)
        {
            if (info.consumeOnUse)
            {
                RemoveAmount(1);
            }
        }
        
        public virtual void UseMenuItemOnCharacter(Character character)
        {
            if (info.consumeOnUse)
            {
                RemoveAmount(1);
            }
            
        }
        
        public virtual void UseMenuItemOnCharacter(CharacterBattleManager characterBattleManager)
        {
            if (info.consumeOnUse)
            {
                RemoveAmount(1);
            }
            
        }
        
        public void AddAmount(int value)
        {
            amount += value;
        }
        
        public void RemoveAmount(int value)
        {
            amount -= value;
        }

        // public void ApplyHeal(int healAmount, ItemUseSectionType itemUseSectionType)
        // {
        //     switch (itemUseSectionType)
        //     {
        //         case ItemUseSectionType.InventoryMenu:
        //             Debug.Log("This works here buddy");
        //             break;
        //         case ItemUseSectionType.Combat:
        //             BattleManager.Instance.selectedPlayerCharacter.selectedTargets[0].GetComponent<Health>().ApplyHeal(healAmount);
        //             break;
        //     }
        // }
        
        public void ApplyHeal(string healAmount, ItemUseSectionType itemUseSectionType)
        {
            
            
            int x = 0;

            if (Int32.TryParse(healAmount, out x))
            {
                switch (itemUseSectionType)
                {
                    case ItemUseSectionType.InventoryMenu:

                        if (info.targetType == TargetType.All || info.targetType == TargetType.AllAllies)
                        {

                            foreach (Character character in PlayerManager.Instance.currentParty)
                            {
                                character.characterController.GetComponent<Health>().ApplyHeal(x);
                            }
                            
                        } 
                        else if (info.targetType == TargetType.SingleTarget || info.targetType == TargetType.SingleAlly)
                        {
                            CanvasManager.Instance.pauseMenuManager.SelectedPauseMenu.GetComponent<InventoryPauseMenu>()
                                .selectedPartyMenuSlot.character.characterController.GetComponent<Health>().ApplyHeal(x);
                        }
                        
                        
                        break;
                    case ItemUseSectionType.Combat:
                        BattleManager.Instance.selectedPlayerCharacter.selectedTargets[0].GetComponent<Health>().ApplyHeal(x);
                        break;
                }
            }
        }

        
    }
}