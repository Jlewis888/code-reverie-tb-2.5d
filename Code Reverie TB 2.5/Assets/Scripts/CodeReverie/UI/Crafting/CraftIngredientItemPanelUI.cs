using System;
using Sirenix.OdinInspector;
using TMPro;

namespace CodeReverie
{
    public class CraftIngredientItemPanelUI : SerializedMonoBehaviour
    {
        public Item item;
        public TMP_Text nameText;
        public TMP_Text countText;
        public int requiredAmount = 0;
        
        
        private void Update()
        {
            if (item != null)
            {
                countText.text = $"{item.amount}/{requiredAmount}";
            }
            else
            {
                countText.text = $"0/{requiredAmount}";
            }
        }
    }
}