using Sirenix.OdinInspector;

namespace CodeReverie
{
    public class InventorySlot
    {
        public int slotIndex;
        public Item item;
        public int amount;

        public InventorySlot(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
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