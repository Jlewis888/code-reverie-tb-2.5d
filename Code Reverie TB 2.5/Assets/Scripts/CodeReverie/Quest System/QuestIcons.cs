using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class QuestIcons : SerializedMonoBehaviour
    {
        public bool startQuestIconActive;
        public bool completeQuestIconActive;
        public GameObject startQuestIcon;
        public GameObject completeQuestIcon;


        public void SetQuestIcons()
        {
            if (startQuestIconActive)
            {
                startQuestIcon.SetActive(true);
                completeQuestIcon.SetActive(false);
            }
            
            if (completeQuestIconActive)
            {
                startQuestIcon.SetActive(false);
                completeQuestIcon.SetActive(true);
            }
            
            if (!startQuestIconActive && !completeQuestIconActive)
            {
                startQuestIcon.SetActive(false);
                completeQuestIcon.SetActive(false);
            }
        }
        
        public void SetIcons(bool setStartQuestIcon, bool setCompleteQuestIcon)
        {

            if (setStartQuestIcon)
            {
                startQuestIcon.SetActive(true);
                completeQuestIcon.SetActive(false);
            }
            
            if (setCompleteQuestIcon)
            {
                startQuestIcon.SetActive(false);
                completeQuestIcon.SetActive(true);
            }

            if (!setCompleteQuestIcon && !setStartQuestIcon)
            {
                startQuestIcon.SetActive(false);
                completeQuestIcon.SetActive(false);
            }
            
            
        }

        public void SetStartQuestIcon(bool isActive)
        {
            
            startQuestIconActive = isActive;
            SetQuestIcons();
        }
        
        public void SetCompleteQuestIcon(bool isActive)
        {
          
            completeQuestIconActive = isActive;
            SetQuestIcons();
        }
    }
}