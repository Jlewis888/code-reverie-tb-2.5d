using System;
using UnityEngine.EventSystems;

namespace CodeReverie
{
    public class LearnSkillPauseMenuNavigationButton : PauseMenuNavigationButton, ISelectHandler
    {
        public ArchetypeSkillContainer archetypeSkillContainer;


        // private void OnEnable()
        // {
        //     EventManager.Instance.generalEvents.onPauseMenuCharacterSwap += Init;
        // }
        //
        // private void OnDisable()
        // {
        //     EventManager.Instance.generalEvents.onPauseMenuCharacterSwap -= Init;
        // }


        public void Init()
        {
            
        }

        public void OnSelect(BaseEventData eventData)
        {
            EventManager.Instance.generalEvents.OnLearnSkillSlotSelect(this);
        }
    }
}