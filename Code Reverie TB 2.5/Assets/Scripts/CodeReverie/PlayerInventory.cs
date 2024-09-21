using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    public class PlayerInventory
    {
        public List<Item> items = new List<Item>();
        

        public PlayerInventory()
        {
            // for (int i = 0; i < 30; i++)
            // {
            //     if (!inventoryMap.ContainsKey(i))
            //     {
            //         inventoryMap.Add(i, null);
            //     }
            // }
        }

        // public void AddItem(string itemId)
        // {
        //     foreach (int index in inventoryMap.Keys)
        //     {
        //         if (InventorySlotEmpty(index))
        //         {
        //             inventoryMap[index] = new InventorySlot();
        //             break;
        //         }
        //     }
        // }
        
        public void AddItem(ItemInfo item, int amount = 1)
        {

            if (item.stackable)
            {
                (bool, int) inventorySlotCheck = ItemInInventory(item);
               
               
                if (inventorySlotCheck.Item1)
                {
                    //inventorySlotCheck.Item2.AddAmount(amount);
                    items[inventorySlotCheck.Item2].AddAmount(amount);
                }
                else
                {

                    Item itemToAdd = ItemManager.Instance.CreateItem(item);

                    if (itemToAdd != null)
                    {
                        itemToAdd.AddAmount(amount);
                        items.Add(itemToAdd);
                    }
                    else
                    {
                        Debug.Log("Item Could not be created");
                    }
                
                
                }
            }
            else
            {
               
                for (int i = 0; i < amount; i++)
                {
                    Item itemToAdd = ItemManager.Instance.CreateItem(item);

                    if (itemToAdd != null)
                    {
                        itemToAdd.AddAmount(1);
                        items.Add(itemToAdd);
                    }
                    else
                    {
                        Debug.Log("Item Could not be created");
                    }   
                }
            }
            
            
            
            
        }
        
        public void AddItem(Item item, int amount = 1)
        {
            if (item.info.stackable)
            {
                (bool, int) inventorySlotCheck = ItemInInventory(item);
               
               
                if (inventorySlotCheck.Item1)
                {
                    //inventorySlotCheck.Item2.AddAmount(amount);
                    items[inventorySlotCheck.Item2].AddAmount(amount);
                }
                else
                {
                    item.AddAmount(amount);
                    items.Add(item);
                }
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    item.AddAmount(1);
                    items.Add(item);
                }
            }
        }
        
        
        public void RemoveItem(ItemInfo itemInfo, int amount = 1)
        {
            
            int count = 0;
            
            foreach (Item inventorySlot in items)
            {

                if (inventorySlot.info.id == itemInfo.id)
                {
                    items[count].RemoveAmount(amount);
                        
                    if (items[count].amount <= 0)
                    {
                        RemoveItems(count);
                    }
                    return;
                }

                count++;
            }
            
        }

        public void RemoveItem(int index, int amount = 1)
        {
            items[index].RemoveAmount(amount);

            if (items[index].amount <= 0)
            {
                RemoveItems(index);
            }
        }

        public void ItemAmountCheck(Item item)
        {
            
        }


        public void RemoveItems(int index)
        {
            items.RemoveAt(index);
        }
        
        
        public (bool, int) ItemInInventory(ItemInfo item)
        {
            int count = 0;
            
            foreach (Item inventorySlot in items)
            {

                if (inventorySlot.info.id == item.id)
                {
                    return (true, count);
                }

                count++;
            }

            return (false, -1);
        }
        
        
        
        public (bool, int) ItemInInventory(Item item)
        {

            int count = 0;
            
            foreach (Item inventorySlot in items)
            {
                if (inventorySlot.info.id == item.info.id)
                {
                    return (true, count);
                }

                count++;
            }

            return (false, -1);
        }

        public bool HasCombatItem()
        {
            
            foreach (Item item in items)
            {
                
                if (item.info.combatItem)
                {
                    return true;
                }
                
            }
            
            return false;
        }
    }
}