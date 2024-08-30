using IEVO.UI.uGUIDirectedNavigation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SkillSlotUI : Selectable
    {

        public ItemSubType itemSubType;
        public int slotIndex;
        [SerializeField]public SkillSlot skillSlot;
        
        protected override void Awake()
        {
            GetComponent<DirectedNavigation>().ConfigRight.Type = DirectedNavigationType.Value.SelectableList;
            GetComponent<DirectedNavigation>().ConfigLeft.Type = DirectedNavigationType.Value.SelectableList;
            GetComponent<DirectedNavigation>().ConfigUp.Type = DirectedNavigationType.Value.SelectableList;
            GetComponent<DirectedNavigation>().ConfigDown.Type = DirectedNavigationType.Value.SelectableList;
        }

        public override void OnSelect(BaseEventData eventData)
        {
            //base.OnDeselect(eventData);
            GetComponent<Image>().color = Color.red;
            EventManager.Instance.generalEvents.OnSkillSlotSelect(this);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            //base.OnDeselect(eventData);
            GetComponent<Image>().color = Color.white;
        }
    }
}