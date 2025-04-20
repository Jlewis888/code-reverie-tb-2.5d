namespace CodeReverie
{
    public class GemSlot
    {
        public int slotLevel;
        public Skill skill;
        public Item equippedItem;


        public GemSlot()
        {
            slotLevel = 1;
        }
        
        
        public void EquipGemSlotItem(Item item)
        {
            equippedItem = item;
            //skill = SkillsManager.Instance.CreateSkill(item.info.skillSlotSkillDataContainer);
        }
    }
}