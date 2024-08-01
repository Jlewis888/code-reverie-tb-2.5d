using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    
    //todo make Item Abstract
    public class Item
    {
        public ItemInfo info;
        public List<Stat> stats = new List<Stat>();
        public int amount;

        public Item(ItemInfo itemInfo)
        {
            info = itemInfo;
        }
        
        public virtual void UseItem()
        {
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
        
    }
}