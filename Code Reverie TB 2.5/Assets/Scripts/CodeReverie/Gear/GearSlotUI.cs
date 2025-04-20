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
        public Character character;
        public GearSlotType gearSlotType;
        public Item item;
        public GameObject highlight;
        public GameObject skillSlotHolder;
        public List<SkillSlotUI> skillSlotUIList = new List<SkillSlotUI>();
        public GameObject gearDetailsPanel;
        public GemSlotPanel gemSlotPanel;
        
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onPauseMenuCharacterSwap += Init;
            ToggleGearDetailsPanel();
            SetSkillSlots();
            
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onPauseMenuCharacterSwap -= Init;
        }
        
        
        public void Init(Character _character)
        {
            character = _character;

            if (character.characterGear.relicSlots.ContainsKey(gearSlotType))
            {
                if (character.characterGear.relicSlots[gearSlotType].item != null)
                {
                    item = character.characterGear.relicSlots[gearSlotType].item;
                    nameText.text = item.info.itemName;
                }
                else
                {
                    item = null;
                    nameText.text = "Empty";
                }
            }
            
        }

        public void SetSkillSlots()
        {


            if (item != null)
            {
                gemSlotPanel.gemSlot1.gemSlot = item.gemSlotContainer.gemSlot1;
                gemSlotPanel.gemSlot2.gemSlot = item.gemSlotContainer.gemSlot2;
                gemSlotPanel.gemSlot3.gemSlot = item.gemSlotContainer.gemSlot3;
            }
            
            gemSlotPanel.Init();
            
            
            
            return;
            skillSlotUIList = new List<SkillSlotUI>();
            
            //TODO Convert to a POOL
            foreach (Transform child in skillSlotHolder.transform)
            {
                Destroy(child.gameObject);
            }

            

            if (item == null)
            {
                return;
            }

            // if (item.skillSlots == null)
            // {
            //     return;
            // }
            //

            if (character.characterSkills.skillSlots[gearSlotType].Count > 0)
            {
                SkillSlotUI skillSlotUIPF = CanvasManager.Instance.pauseMenuManager.SelectedPauseMenu
                    .GetComponent<EquipmentPauseMenu>().skillSlotUIPF;

                foreach (SkillSlot skillSlot in character.characterSkills.skillSlots[gearSlotType])
                {
                    SkillSlotUI skillSlotUI = Instantiate(skillSlotUIPF, skillSlotHolder.transform);
                    skillSlotUI.skillSlot = skillSlot;
                    skillSlotUIList.Add(skillSlotUI);
                }
            }
            
            
            //skillSlotUIList = GetComponentsInChildren<SkillSlotUI>().ToList();
            
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

        public void ToggleGearDetailsPanel()
        {
            gearDetailsPanel.SetActive(true);
            gemSlotPanel.gameObject.SetActive(false);
        }
        
        public void ToggleGearSkillsPanel()
        {
            gearDetailsPanel.SetActive(false);
            gemSlotPanel.gameObject.SetActive(true);
        }

        public void EquipGemSlotItem(int slot, Item item)
        {

            gemSlotPanel.EquipGemSlotItem(slot, item);
        }
        
    }
}