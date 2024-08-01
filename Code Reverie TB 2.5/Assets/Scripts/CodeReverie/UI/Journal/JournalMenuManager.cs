using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class JournalMenuManager : MenuManager
    {
        public QuestMenuItemUI questMenuItemUIPF;
        public GameObject questMenuItemHolder;
        public List<QuestMenuItemUI> questMenuItemUis;

        public Button mainQuestFilterButton;
        public Button sideQuestFilterButton;
        public Button completedQuestFilterButton;

        private void Awake()
        {
            
            mainQuestFilterButton.onClick.AddListener(FilterMainQuest);
            sideQuestFilterButton.onClick.AddListener(FilterSideQuest);
            completedQuestFilterButton.onClick.AddListener(FilterCompletedQuest);
        }

        private void OnEnable()
        {
            Clear();
            foreach (Quest quest in QuestManager.Instance.quests.Values)
            {
                if (quest.Status == QuestStatus.Active || quest.Status == QuestStatus.Complete)
                {
                    QuestMenuItemUI questMenuItemUI = Instantiate(questMenuItemUIPF, questMenuItemHolder.transform);

                    questMenuItemUI.quest = quest;
                    questMenuItemUI.SetQuestData();
                    questMenuItemUis.Add(questMenuItemUI);
                }
            }
            
            FilterMainQuest();
        }

        private void OnDisable()
        {
            Clear();
        }

        public void Clear()
        {
            foreach (Transform child in questMenuItemHolder.transform)
            {
                Destroy(child.gameObject);
            }

            questMenuItemUis = new List<QuestMenuItemUI>();
        }

        public void FilterMainQuest()
        {
            foreach (QuestMenuItemUI questMenuItemUi in questMenuItemUis)
            {
                if (questMenuItemUi.quest.info.questType == QuestType.Main)
                {
                    questMenuItemUi.gameObject.SetActive(true);
                }
                else
                {
                    questMenuItemUi.gameObject.SetActive(false);
                }
            }
        }
        
        public void FilterSideQuest()
        {
            foreach (QuestMenuItemUI questMenuItemUi in questMenuItemUis)
            {
                if (questMenuItemUi.quest.info.questType == QuestType.Side)
                {
                    questMenuItemUi.gameObject.SetActive(true);
                }
                else
                {
                    questMenuItemUi.gameObject.SetActive(false);
                }
            }
        }
        
        public void FilterCompletedQuest()
        {
            foreach (QuestMenuItemUI questMenuItemUi in questMenuItemUis)
            {
                if (questMenuItemUi.quest.Status == QuestStatus.Complete)
                {
                    questMenuItemUi.gameObject.SetActive(true);
                }
                else
                {
                    questMenuItemUi.gameObject.SetActive(false);
                }
            }
        }
    }
}