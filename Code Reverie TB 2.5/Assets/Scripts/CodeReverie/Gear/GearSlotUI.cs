using System.Collections.Generic;
using System.Linq;
using IEVO.UI.uGUIDirectedNavigation;
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
        public GameObject skillSlotHolder;
        public List<SkillSlotUI> skillSlotUIList = new List<SkillSlotUI>();
        
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onPauseMenuCharacterSwap += Init;
            SetSkillSlots();
            
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

        public void SetSkillSlots()
        {
            skillSlotUIList = GetComponentsInChildren<SkillSlotUI>().ToList();
            
            foreach (SkillSlotUI skillSlotUI in skillSlotUIList)
            {
                skillSlotUI.GetComponent<DirectedNavigation>().ConfigRight.SelectableList.SelectableList = skillSlotUIList.ToArray();
                skillSlotUI.GetComponent<DirectedNavigation>().ConfigLeft.SelectableList.SelectableList = skillSlotUIList.ToArray();
                // skillSlotUI.GetComponent<DirectedNavigation>().ConfigUp.SelectableList.SelectableList = skillSlotUIList.ToArray();
                // skillSlotUI.GetComponent<DirectedNavigation>().ConfigDown.SelectableList.SelectableList = skillSlotUIList.ToArray();
            }
        }

        public void SetVerticalUpNavigationSkillSlots(List<SkillSlotUI> skillSlots)
        {
            foreach (SkillSlotUI skillSlotUI in skillSlotUIList)
            {
                skillSlotUI.GetComponent<DirectedNavigation>().ConfigUp.SelectableList.SelectableList =
                    skillSlots.ToArray();
            }
        }

        public void SetVerticalDownNavigationSkillSlots(List<SkillSlotUI> skillSlots)
        {
            foreach (SkillSlotUI skillSlotUI in skillSlotUIList)
            {
                skillSlotUI.GetComponent<DirectedNavigation>().ConfigDown.SelectableList.SelectableList =
                    skillSlots.ToArray();
            }
        }

        public void ToggleSkillSlotOn()
        {
            skillSlotHolder.SetActive(true);
            SetSkillSlots();
            nameText.gameObject.SetActive(false);
        }
        
        public void ToggleSkillSlotOff()
        {
            skillSlotHolder.SetActive(false);
            nameText.gameObject.SetActive(true);
        }
    }
}