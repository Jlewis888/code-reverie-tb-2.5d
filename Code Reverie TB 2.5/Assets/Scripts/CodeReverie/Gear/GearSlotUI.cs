using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeReverie
{
    public class GearSlotUI : PauseMenuNavigationButton
    {
        public GearSlotType gearSlotType;
        public Item item;
        public GameObject highlight;
        
        
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onPauseMenuCharacterSwap += Init;
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onPauseMenuCharacterSwap -= Init;
        }
        
        
        public void Init(Character character)
        {

            if (character.characterGear.relicSlots.ContainsKey(gearSlotType))
            {
                if (character.characterGear.relicSlots[gearSlotType].item != null)
                {
                    item = character.characterGear.relicSlots[gearSlotType].item;
                    nameText.text = item.info.itemName;
                }
            }
            
            
        }
        
    }
}