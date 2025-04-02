namespace CodeReverie
{
    public class GemSlot
    {
        public int slotLevel;
        public Skill skill;
        public Item equippedItem;
        
        
        public void EquipSkillSlotItem(Item item)
        {
            equippedItem = item;
            skill = SkillsManager.Instance.CreateSkill(item.info.skillSlotSkillDataContainer);
        }
    }
}