using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

namespace CodeReverie
{
    public class QuestMenuItemUI : SerializedMonoBehaviour
    {
        public Quest quest;
        public TMP_Text questTitleText;
        public TMP_Text questObjectiveText;
        public Button trackQuestButton;
        public TMP_Text trackQuestButtonText;
        public Button abandonQuestButton;


        private void Awake()
        {
            trackQuestButton.onClick.AddListener(TrackQuest);
            abandonQuestButton.onClick.AddListener(AbandonQuest);
        }

        public void SetQuestData()
        {
            questTitleText.text = $"<color=#FFAC36>{quest.info.questName}";
            SetQuestObjectiveText();
            
            if (quest.IsTracking)
            {
                trackQuestButtonText.text = "Untrack";
            }
            else
            {
                trackQuestButtonText.text = "Track";
            }
        }



        public void TrackQuest()
        {
            quest.IsTracking = !quest.IsTracking;

            if (quest.IsTracking)
            {
                trackQuestButtonText.text = "Untrack";
            }
            else
            {
                trackQuestButtonText.text = "Track";
            }
            
        }

        public void AbandonQuest()
        {
            quest.Status = QuestStatus.Available;
            
            Destroy(gameObject);
        }


        public void SetQuestObjectiveText()
        {
            string questStringBuilder = "";
            
            foreach (QuestStepObjective questObjective in quest.currentQuestStep.questObjectives)
            {
                string questObjectiveDescription = questObjective.questObjectiveDescription.Replace("{{color}}", "#FFAC36");
                
                if (questObjective.showRequiredAmount)
                {
                    questStringBuilder = $"{questObjective.CurrentCount}/{questObjective.requiredAmount} {questObjectiveDescription} \n";
                }
                else
                {
                    questStringBuilder = $"{questObjectiveDescription} \n";
                }
            }

            questObjectiveText.text = questStringBuilder;
        }
        
        
    }
}