using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class CharacterGear
    {
        public GearSlot weaponSlot;
        public Dictionary<int, GearSlot> accessorySlots = new Dictionary<int, GearSlot>();
        public Dictionary<int, GearSlot> artifactSlots = new Dictionary<int, GearSlot>();
        public Dictionary<GearSlotType, GearSlot> relicSlots = new Dictionary<GearSlotType, GearSlot>();
        public Dictionary<int, GearSlot> combatItemSlots = new Dictionary<int, GearSlot>();
        
        public Dictionary<GearSetType, int> gearSetMap = new Dictionary<GearSetType, int>();
        public List<Stat> gearSetBonusStats = new List<Stat>();

        public Dictionary<GemSetType, int> gemSetMap = new Dictionary<GemSetType, int>();
        public List<Stat> gemSetBonusStats = new List<Stat>();
        
        
        public CharacterGear()
        {
            weaponSlot = new GearSlot();
            
            accessorySlots.Add(1, new GearSlot());
            accessorySlots.Add(2, new GearSlot());
            accessorySlots.Add(3, new GearSlot());
            accessorySlots.Add(4, new GearSlot());
            accessorySlots.Add(5, new GearSlot());
            
            artifactSlots.Add(1, new GearSlot());
            artifactSlots.Add(2, new GearSlot());
            
            
            combatItemSlots.Add(1, new GearSlot());
            combatItemSlots.Add(2, new GearSlot());
            combatItemSlots.Add(3, new GearSlot());
            combatItemSlots.Add(4, new GearSlot());
            combatItemSlots.Add(5, new GearSlot());
            
            
            relicSlots.Add(GearSlotType.Gluttony, new GearSlot());
            relicSlots.Add(GearSlotType.Wrath, new GearSlot());
            relicSlots.Add(GearSlotType.Pride, new GearSlot());
            relicSlots.Add(GearSlotType.Envy, new GearSlot());
            relicSlots.Add(GearSlotType.Greed, new GearSlot());
            relicSlots.Add(GearSlotType.Lust, new GearSlot());
            relicSlots.Add(GearSlotType.Sloth, new GearSlot());
            
        }

        public void EquipWeapon(Item item)
        {
            weaponSlot.item = item;
        }
        
        public void EquipRelic(GearSlotType gearSlotType, Item item)
        {
            
            if (item.info.gearSlotType == GearSlotType.None)
            {
                return;
            }
            
            relicSlots[gearSlotType].item = item;
            GearBonusChecks();
        }
        
        // public void EquipRelic(Item item)
        // {
        //
        //     if (item.info.gearSlotType == GearSlotType.None)
        //     {
        //         return;
        //     }
        //     
        //     relicSlots[item.info.gearSlotType].item = item;
        // }


        public void EquipAccessory(Item item, int slot)
        {
            accessorySlots[slot].item = item;
            
            GearBonusChecks();
        }
        
        public void EquipArtifact(Item item, int slot)
        {
            artifactSlots[slot].item = item;
            GearBonusChecks();
        }

        public void EquipCombatItem(Item item, int slot)
        {
            combatItemSlots[slot].item = item;
        }
        

        public void UnequipItem(Item item)
        {
            
            switch (item.info.itemType)
            {
                case ItemType.Weapon:
                    weaponSlot.item = null;
                    break;
                case ItemType.Accessory:
                    foreach (GearSlot gearSlot in accessorySlots.Values)
                    {
                        if (gearSlot.item == item)
                        {
                            gearSlot.item = null;
                            GearBonusChecks();
                            return;
                        }
                    }
                    
                    break;
                case ItemType.Artifact:
                    foreach (GearSlot gearSlot in artifactSlots.Values)
                    {
                        if (gearSlot.item == item)
                        {
                            gearSlot.item = null;
                            return;
                        }
                    }

                    break;
            }
        }

        public void GearBonusChecks()
        {
            GearSetBonusCheck();
            GemSetBonusCheck();
        }
        
        
        public void GearSetBonusCheck()
        {
            Debug.Log("Did the gear set bonust check run?");
            
            gearSetMap = new Dictionary<GearSetType, int>();
            gearSetBonusStats = new List<Stat>();
            
            foreach (GearSlot gearSlot in relicSlots.Values)
            {

                if (gearSlot.item == null)
                {
                    continue;
                }
                
                ItemInfo itemData = gearSlot.item.info;
                    
                if (itemData.gearSetType != GearSetType.None)
                {
                        
                        
                    if (gearSetMap.ContainsKey(itemData.gearSetType))
                    {
                        gearSetMap[itemData.gearSetType] += 1;
                    }
                    else
                    {
                        gearSetMap.Add(itemData.gearSetType, 1);
                    }

                    if (ItemManager.Instance.CheckIfGearSetBonusKeyExist(itemData.gearSetType,
                            gearSetMap[itemData.gearSetType]))
                    {
                          
                            
                        ApplySetBonus(itemData.gearSetType,
                            gearSetMap[itemData.gearSetType]);
                    }
                        
                }
            }
        }
        
        public void GemSetBonusCheck()
        {
            
            Debug.Log("Did the gem set bonust check run?");
            
            gemSetMap = new Dictionary<GemSetType, int>();
            gemSetBonusStats = new List<Stat>();
            
            foreach (GearSlot gearSlot in relicSlots.Values)
            {

                if (gearSlot.item == null)
                {
                    continue;
                }

                Item item = null;

                if (gearSlot.item.gemSlotContainer.gemSlot1 != null)
                {
                    if (gearSlot.item.gemSlotContainer.gemSlot1.equippedItem != null)
                    {
                        item = gearSlot.item.gemSlotContainer.gemSlot1.equippedItem;
                        SetGemBonus(item);
                    }
                }
                
                if (gearSlot.item.gemSlotContainer.gemSlot2 != null)
                {
                    if (gearSlot.item.gemSlotContainer.gemSlot2.equippedItem != null)
                    {
                        item = gearSlot.item.gemSlotContainer.gemSlot2.equippedItem;
                        SetGemBonus(item);
                    }
                }
                
                if (gearSlot.item.gemSlotContainer.gemSlot3 != null)
                {
                    if (gearSlot.item.gemSlotContainer.gemSlot3.equippedItem != null)
                    {
                        item = gearSlot.item.gemSlotContainer.gemSlot3.equippedItem;
                        SetGemBonus(item);
                    }
                }

                if (item == null)
                {
                    continue;
                }
                
                
                
            }
        }

        void SetGemBonus(Item item)
        {
            ItemInfo itemData = item.info;
                    
            if (itemData.gemSetType != GemSetType.None)
            {
                        
                        
                if (gemSetMap.ContainsKey(itemData.gemSetType))
                {
                    gemSetMap[itemData.gemSetType] += 1;
                }
                else
                {
                    gemSetMap.Add(itemData.gemSetType, 1);
                }

                if (ItemManager.Instance.CheckIfGemSetBonusKeyExist(itemData.gemSetType,
                        gemSetMap[itemData.gemSetType]))
                {
                          
                            
                    ApplyGemSetBonus(itemData.gemSetType, gemSetMap[itemData.gemSetType]);
                }
                        
            }
        }
        
        
        public void ApplySetBonus(GearSetType gearSetType, int setBonusNumber)
        {


            switch (ItemManager.Instance.GetGearSetBonus(gearSetType, setBonusNumber).gearSetBonusType)
            {
                case GearSetBonusType.AdditiveStat:
                    GearSetBonusAdditiveStat gearSetBonus1 =
                        ItemManager.Instance.GetGearSetBonus(gearSetType, setBonusNumber) as GearSetBonusAdditiveStat;

                    //Stat stat = new Stat(gearSetBonus1.statAttribute, gearSetBonus1.value, gearSetBonus1.statType);
                    gearSetBonusStats.Add(new Stat(gearSetBonus1.statAttribute, gearSetBonus1.value, gearSetBonus1.statType));
                    //gearSetBonusStats.Add(new StatModifier(gearSetBonus1.statAttribute, gearSetBonus1.value, 10, true));
                    
                    break;
                case GearSetBonusType.PercentageStat:
                    GearSetBonusPercentageStat gearSetBonus2 =
                        ItemManager.Instance.GetGearSetBonus(gearSetType, setBonusNumber) as GearSetBonusPercentageStat;
                
                
                    //gearSetBonusPercentageStats.Add(new StatPercentageModifier(gearSetBonus2.statAttribute, gearSetBonus2.value, 10, true));
                    gearSetBonusStats.Add(new Stat(gearSetBonus2.statAttribute, gearSetBonus2.value, gearSetBonus2.statType));
                    break;
                case GearSetBonusType.Skill:
                    
                    //todo Add Trigger Skill
                    //GearSetBonusSkill gearSetBonusSkill = ItemManager.Instance.GetGearSetBonus(gearSetType, setBonusNumber) as GearSetBonusSkill;

                    
                    //GetComponent<PlayerSkillsManager>().gearSetBonusSkills.Add(SkillsManager.Instance.CreateSkill(gearSetBonusSkill.skillDataContainer));
                    break;
            }
        }
        
        public void ApplyGemSetBonus(GemSetType gemSetType, int setBonusNumber)
        {


            switch (ItemManager.Instance.GetGemSetBonus(gemSetType, setBonusNumber).gemSetBonusType)
            {
                case GemSetBonusType.AdditiveStat:
                    GemSetBonusAdditiveStat gemSetBonus1 =
                        ItemManager.Instance.GetGemSetBonus(gemSetType, setBonusNumber) as GemSetBonusAdditiveStat;

                    //Stat stat = new Stat(gearSetBonus1.statAttribute, gearSetBonus1.value, gearSetBonus1.statType);
                    gemSetBonusStats.Add(new Stat(gemSetBonus1.statAttribute, gemSetBonus1.value, gemSetBonus1.statType));
                    //gearSetBonusStats.Add(new StatModifier(gearSetBonus1.statAttribute, gearSetBonus1.value, 10, true));
                    
                    break;
                case GemSetBonusType.PercentageStat:
                    GemSetBonusPercentageStat gemSetBonus2 =
                        ItemManager.Instance.GetGemSetBonus(gemSetType, setBonusNumber) as GemSetBonusPercentageStat;
                
                
                    //gearSetBonusPercentageStats.Add(new StatPercentageModifier(gearSetBonus2.statAttribute, gearSetBonus2.value, 10, true));
                    gemSetBonusStats.Add(new Stat(gemSetBonus2.statAttribute, gemSetBonus2.value, gemSetBonus2.statType));
                    break;
                case GemSetBonusType.Skill:
                    
                    //todo Add Trigger Skill
                    //GearSetBonusSkill gearSetBonusSkill = ItemManager.Instance.GetGearSetBonus(gearSetType, setBonusNumber) as GearSetBonusSkill;

                    
                    //GetComponent<PlayerSkillsManager>().gearSetBonusSkills.Add(SkillsManager.Instance.CreateSkill(gearSetBonusSkill.skillDataContainer));
                    break;
            }
        }

       
    }
}