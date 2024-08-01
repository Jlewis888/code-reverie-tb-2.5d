using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class TrackedQuestHud : SerializedMonoBehaviour
    {
        public Quest quest;
        public TMP_Text questTitleText;
        public List<QuestObjectiveHud> questObjectiveHuds = new List<QuestObjectiveHud>();
        public QuestObjectiveHud questObjectiveHudPF;
        public GameObject questObjectiveHudHolder;
        public Image mainQuestImage;

        private void OnEnable()
        {

            if (quest != null)
            {
                if (quest.info.questType == QuestType.Main)
                {
                    mainQuestImage.gameObject.SetActive(true);
                
                    if (ColorUtility.TryParseHtmlString("#FFAC36", out Color hexColor))
                    {
                        mainQuestImage.color = hexColor;
                    }

                    questTitleText.text = $"<color=#FFAC36>{quest.info.questName}</color>";
                    //questTitleText.SetText(quest.info.questName, 0);
                }
                else
                {
                    questTitleText.text = $"<color=blue>{quest.info.questName}</color>";
                }
            }
            
            
            
            EventManager.Instance.questEvents.onQuestTrackingStatusChange += AddRemoveTrackedQuest;
            EventManager.Instance.questEvents.onQuestStepComplete += UpdateQuestStep;
            SetQuestObjectives();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }

        private void OnDisable()
        {
            EventManager.Instance.questEvents.onQuestTrackingStatusChange -= AddRemoveTrackedQuest;
            EventManager.Instance.questEvents.onQuestStepComplete -= UpdateQuestStep;
            foreach (Transform child in questObjectiveHudHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        
        public void AddRemoveTrackedQuest(Quest quest, bool isTracking)
        {
            if (!isTracking && quest == this.quest)
            {
                Destroy(gameObject);
            }
            
            UpdateLayout();
        }


        public void SetQuestObjectives()
        {
            foreach (QuestStepObjective questObjective in quest.CurrentQuestObjective.questObjectives)
            {
                QuestObjectiveHud questObjectiveHud =
                    Instantiate(questObjectiveHudPF, questObjectiveHudHolder.transform);
                questObjectiveHud.questObjective = questObjective;
                
                questObjectiveHud.gameObject.SetActive(true);
                questObjectiveHuds.Add(questObjectiveHud);
                
                if (ColorUtility.TryParseHtmlString("#FFAC36", out Color hexColor))
                {
                    questObjectiveHud.importantTextColor = hexColor;
                }

                questObjectiveHud.importantColorText = "#FFAC36";

                questObjectiveHud.UpdateQuestObjectiveText();
            }
        }

        public void UpdateQuestStep(QuestStep questStep)
        {

            if (quest.questSteps.Contains(questStep))
            {


                if (questObjectiveHudHolder != null)
                {
                    foreach (Transform child in questObjectiveHudHolder.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
                
                SetQuestObjectives();
            }
            
        }

        public void UpdateLayout()
        {
            // Debug.Log("dhjfalkhfdkjasfhdlaskfhaslk");
            // GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            // GetComponent<VerticalLayoutGroup>().enabled = false;
            //LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            //LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            // GetComponent<VerticalLayoutGroup>().enabled = true;
            // GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            // GetComponent<ContentSizeFitter>().SetLayoutVertical();
        }
        
        
        
    }
}