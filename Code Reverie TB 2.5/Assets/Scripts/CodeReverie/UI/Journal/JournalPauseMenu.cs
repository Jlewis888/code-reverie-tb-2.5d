using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class JournalPauseMenu : PauseMenu
    {
        public JournalPauseMenuNavigationButton journalPauseMenuNavigationButtonPF;
        public List<JournalPauseMenuNavigationButton> journalPauseMenuNavigationButtonList;
        public TMP_Text questTitleText;
        public TMP_Text questObjectiveText;
        public TMP_Text trackQuestButtonText;
        public QuestObjectiveHud questObjectiveHudPF;
        public GameObject questObjectiveHudHolder;
        public List<QuestObjectiveHud> questObjectiveHuds = new List<QuestObjectiveHud>();
        public int questFilterIndex;
        const int questFilterIndexMax = 3;
        public MenuNavigation pauseMenuNavigation;

        private void Awake()
        {
            pauseMenuNavigation = new MenuNavigation();
            //pauseMenuNavigation.onSelectedNavigationButtonChangeDelegate = OnSelectedNavigationButtonChange;
            pauseMenuNavigation.callBack = OnSelectedNavigationButtonChange;
        }


        private void OnEnable()
        {
            EventManager.Instance.generalEvents.ToggleCharacterSidePanelUI(false);
            ClearNavigationButtons();
            SetNavigationButtons();
            questFilterIndex = 0;
            SetFilters();
        }

        private void OnDisable()
        {
            ClearNavigationButtons();
        }

        private void Update()
        {
            
            if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
            {
                Confirm();
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
            {
                EventManager.Instance.generalEvents.OnPauseMenuNavigationStateChange(PauseMenuNavigationState.Menu);
            }
            
            if (GameManager.Instance.playerInput.GetButtonDown("Navigate Menu Horizontal Button"))
            {
                questFilterIndex++;
                
                if (questFilterIndex >= questFilterIndexMax)
                {
                    questFilterIndex = 0;
                }
                
                SetFilters();
                
                // SelectedPartySlotNavigationUI = partySlotNavigationUIList[partySlotIndex];

            } 
            else if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Menu Horizontal Button"))
            {
                questFilterIndex--;
                
                if (questFilterIndex < 0)
                {
                    questFilterIndex = questFilterIndexMax - 1;
                }
                SetFilters();
            }
            
            pauseMenuNavigation.NavigationInputUpdate();
            
        }


        public void SetNavigationButtons()
        {

            journalPauseMenuNavigationButtonList = new List<JournalPauseMenuNavigationButton>();
            
            foreach (Quest quest in QuestManager.Instance.quests.Values)
            {
                if (quest.Status == QuestStatus.Active || quest.Status == QuestStatus.Complete)
                {
                    JournalPauseMenuNavigationButton journalPauseMenuNavigationButton = Instantiate(journalPauseMenuNavigationButtonPF, pauseMenuNavigationHolder.transform);

                    journalPauseMenuNavigationButton.quest = quest;
                    journalPauseMenuNavigationButton.nameText.text = quest.info.questName;
                    journalPauseMenuNavigationButtonList.Add(journalPauseMenuNavigationButton);
                }
            }
        }
        
        // public void TrackQuest()
        // {
        //     quest.IsTracking = !quest.IsTracking;
        //
        //     if (quest.IsTracking)
        //     {
        //         trackQuestButtonText.text = "Untrack";
        //     }
        //     else
        //     {
        //         trackQuestButtonText.text = "Track";
        //     }
        //     
        // }
        //
        // public void AbandonQuest()
        // {
        //     quest.Status = QuestStatus.Available;
        //     
        //     Destroy(gameObject);
        // }
        
        public void SetQuestObjectiveText(Quest quest)
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
        
        public void SetQuestObjectives(Quest quest)
        {

            foreach (Transform child in questObjectiveHudHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
            
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

        public void SetFilters()
        {
            switch (questFilterIndex)
            {
                case 0:
                    //Debug.Log("Filtering Main Quest");
                    FilterMainQuest();
                    break;
                case 1:
                    //Debug.Log("Filtering Side Quest");
                    FilterSideQuest();
                    break;
                case 2:
                    //Debug.Log("Filtering Completed Quest");
                    FilterCompletedQuest();
                    break;
            }

            
            pauseMenuNavigation.SetFirstItem();
            
            // navigationButtonsIndex = 0;
            //
            // SelectedNavigationButton = filteredjournalPauseMenuNavigationButtonList[navigationButtonsIndex];
        }
        
        
        public void FilterMainQuest()
        {

            pauseMenuNavigation.ResetNavigationList();
            foreach (PauseMenuNavigationButton questMenuItemUi in journalPauseMenuNavigationButtonList)
            {
                if (questMenuItemUi.GetComponent<JournalPauseMenuNavigationButton>().quest.info.questType == QuestType.Main)
                {
                    questMenuItemUi.gameObject.SetActive(true);
                    pauseMenuNavigation.Add(questMenuItemUi.GetComponent<JournalPauseMenuNavigationButton>());
                }
                else
                {
                    questMenuItemUi.gameObject.SetActive(false);
                }
            }
        }
        
        public void FilterSideQuest()
        {
            pauseMenuNavigation.ResetNavigationList();
            
            foreach (PauseMenuNavigationButton questMenuItemUi in journalPauseMenuNavigationButtonList)
            {
                if (questMenuItemUi.GetComponent<JournalPauseMenuNavigationButton>().quest.info.questType == QuestType.Side)
                {
                    questMenuItemUi.gameObject.SetActive(true);
                    pauseMenuNavigation.Add(questMenuItemUi.GetComponent<JournalPauseMenuNavigationButton>());
                }
                else
                {
                    questMenuItemUi.gameObject.SetActive(false);
                }
            }
        }
        
        public void FilterCompletedQuest()
        {
            pauseMenuNavigation.ResetNavigationList();
            
            foreach (PauseMenuNavigationButton questMenuItemUi in journalPauseMenuNavigationButtonList)
            {
                if (questMenuItemUi.GetComponent<JournalPauseMenuNavigationButton>().quest.Status == QuestStatus.Complete)
                {
                    questMenuItemUi.gameObject.SetActive(true);
                    pauseMenuNavigation.Add(questMenuItemUi.GetComponent<JournalPauseMenuNavigationButton>());
                }
                else
                {
                    questMenuItemUi.gameObject.SetActive(false);
                }
            }
        }
        
        
        public void Confirm()
        {
            
        }

        public void OnSelectedNavigationButtonChange()
        {
            SetQuestObjectiveText(pauseMenuNavigation.SelectedNavigationButton.GetComponent<JournalPauseMenuNavigationButton>().quest);
            SetQuestObjectives(pauseMenuNavigation.SelectedNavigationButton.GetComponent<JournalPauseMenuNavigationButton>().quest);
        }
    }
}