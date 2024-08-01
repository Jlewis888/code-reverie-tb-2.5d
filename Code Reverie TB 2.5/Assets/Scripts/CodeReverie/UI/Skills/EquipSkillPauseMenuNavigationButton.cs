using System;

namespace CodeReverie
{
    public class EquipSkillPauseMenuNavigationButton : PauseMenuNavigationButton
    {
        public Skill skill;
        public int skillSlotIndex;


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
        
    }
}