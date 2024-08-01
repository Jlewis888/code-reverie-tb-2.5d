using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class QuestObjectiveHud : SerializedMonoBehaviour
    {

        public QuestStepObjective questObjective;
        public TMP_Text objectiveText;
        public Color importantTextColor;
        public string importantColorText;
        
        private void OnEnable()
        {
            EventManager.Instance.questEvents.onQuestObjectiveUpdate += UpdateQuestObjectiveHudText;
            
            UpdateQuestObjectiveText();
        }

        private void OnDisable()
        {
            EventManager.Instance.questEvents.onQuestObjectiveUpdate -= UpdateQuestObjectiveHudText;
        }


        public void UpdateQuestObjectiveText()
        {
           
            if (questObjective != null)
            {
                //string questObjectiveDescription = questObjective.info.questObjectiveDescription.Replace("{{color}}", importantColorText);
                string questObjectiveDescription = questObjective.questObjectiveDescription.Replace("{{color}}", importantColorText);
                
                if (questObjective.showRequiredAmount)
                {
                    objectiveText.text = $"{questObjective.CurrentCount}/{questObjective.requiredAmount} {questObjectiveDescription}";
                }
                else
                {
                    objectiveText.text = $"{questObjectiveDescription}";
                }
                
                //objectiveText.text.Replace("{{color}}", importantTextColor.ToString());
            }

           

        }
        
        public void UpdateQuestObjectiveHudText(QuestStepObjective questObjective)
        {
            if (questObjective == this.questObjective)
            {
                UpdateQuestObjectiveText();
            }
        }
        
    }
}