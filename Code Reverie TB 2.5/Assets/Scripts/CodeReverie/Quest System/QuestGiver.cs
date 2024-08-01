using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class QuestGiver : SerializedMonoBehaviour
    {
        public QuestIcons questIcons;
        public QuestIconsUI questIconsUI;
        public List<QuestGiverContainer> questGiverContainers = new List<QuestGiverContainer>();

        
        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            EventManager.Instance.questEvents.onQuestStateChange += QuestStateChange;
            EventManager.Instance.questEvents.onQuestStepChange += QuestStepChange;
        }

        private void OnDisable()
        {
            EventManager.Instance.questEvents.onQuestStateChange -= QuestStateChange;
            EventManager.Instance.questEvents.onQuestStepChange -= QuestStepChange;
        }


        public void Init()
        {
            foreach (Quest quest in QuestManager.Instance.quests.Values)
            {
                QuestStateChange(quest);
               
            }
            
            QuestStepInit();
        }
        

        public void QuestStateChange(Quest quest)
        {
            if (quest.info.questGiver == null)
            {
                return;
            } 
            
         
            if (GetComponent<CharacterController>())
            {
                
                if (GetComponent<CharacterController>().character.info.id == quest.info.questGiver.id)
                {
                    
                    
                    if (quest.Status == QuestStatus.Available)
                    {
                        
                        QuestIcon questIcon = Instantiate(QuestManager.Instance.QuestStartMarkerPF, transform);
                        questIcon.transform.localPosition = Vector3.zero;
                        questIcon.gameObject.SetActive(true);
                        questIcon.quest = quest;
                        
                       
                        
                        QuestIconUI questIconUI = Instantiate(QuestManager.Instance.QuestStartMarkerUIPF, questIconsUI.transform);
                        questIconUI.transform.localPosition = Vector3.zero;
                        questIconUI.gameObject.SetActive(true);
                        questIconUI.quest = quest;
                        
                        
                        //questIcons.SetStartQuestIcon(questGiverContainer.startQuest);
                        //questIconsUI.SetStartQuestIcon(true);
                    }
                    else
                    {
                        DestroyIcons(quest);
                        
                    }
                }
            }
            
            
            // if (questGiverContainers.Any(x => x.questDataContainer.id == quest.info.id))
            // {
            //     
            //     foreach (QuestGiverContainer questGiverContainer in questGiverContainers)
            //     {
            //     
            //         if (quest.info.id == questGiverContainer.questDataContainer.id)
            //         {
            //
            //
            //             if (quest.Status == QuestStatus.Available)
            //             {
            //                 questIcons.SetStartQuestIcon(questGiverContainer.startQuest);
            //                 questIconsUI.SetStartQuestIcon(questGiverContainer.startQuest);
            //             }
            //             else
            //             {
            //                 questIcons.SetStartQuestIcon(false);
            //                 questIconsUI.SetStartQuestIcon(false);
            //             }
            //
            //             // if (questGiverContainer.questStepDataContainer != null)
            //             // {
            //             //     if (quest.currentQuestStep.info.id == questGiverContainer.questStepDataContainer.id)
            //             //     {
            //             //         if (quest.currentQuestStep.Status == QuestObjectiveStatus.Active)
            //             //         {
            //             //     
            //             //             setCompleteQuestIcon = true;
            //             //         }
            //             //     }
            //             // }
            //         
            //         }
            //     }
            //     //
            //     // questIcons.SetIcons(setStartQuestIcon, setCompleteQuestIcon);
            // }
            
        }

        public void QuestStepInit()
        {
            foreach (Quest quest in QuestManager.Instance.quests.Values)
            {
                if (quest.Status != QuestStatus.Active)
                {
                    
                    continue;
                }

                if (quest.currentQuestStep.info.questGiver == null)
                {
                    continue;
                }
            
            
         
                if (GetComponent<CharacterController>())
                {
                
                    if (GetComponent<CharacterController>().character.info.id == quest.currentQuestStep.info.id)
                    {
                        DestroyIcons();
                        
                        QuestIcon questIcon = Instantiate(QuestManager.Instance.QuestFinishMarkerPF, transform);
                        questIcon.transform.localPosition = Vector3.zero;
                        questIcon.gameObject.SetActive(true);
                        questIcon.quest = quest;
                        
                        QuestIconUI questIconUI = Instantiate(QuestManager.Instance.QuestFinishMarkerUIPF, questIconsUI.transform);
                        questIconUI.transform.localPosition = Vector3.zero;
                        questIconUI.gameObject.SetActive(true);
                        questIconUI.quest = quest;
                        
                        return;
                    }
                }
            }
        }

        public void QuestStepChange(Quest quests = null)
        {
            foreach (Quest quest in QuestManager.Instance.quests.Values)
            {
                if (quest.Status != QuestStatus.Active)
                {
                    
                    continue;
                }

                if (quest.currentQuestStep.info.questGiver == null)
                {
                    continue;
                }
            
            
         
                if (GetComponent<CharacterController>())
                {
                
                    if (GetComponent<CharacterController>().character.info.id == quest.currentQuestStep.info.questGiver.id)
                    {
                        DestroyIcons();
                        
                        QuestIcon questIcon = Instantiate(QuestManager.Instance.QuestFinishMarkerPF, transform);
                        questIcon.transform.localPosition = Vector3.zero;
                        questIcon.gameObject.SetActive(true);
                        questIcon.quest = quest;
                        
                        QuestIconUI questIconUI = Instantiate(QuestManager.Instance.QuestFinishMarkerUIPF, questIconsUI.transform);
                        questIconUI.transform.localPosition = Vector3.zero;
                        questIconUI.gameObject.SetActive(true);
                        questIconUI.quest = quest;
                        
                        return;
                    }
                }
            }
            
            
            DestroyIcons();
            
        }

        // public void QuestStepChange(Quest quest)
        // {
        //     
        //     
        //     
        //     if (quest.Status != QuestStatus.Active)
        //     {
        //         questIcons.SetCompleteQuestIcon(false);
        //         questIconsUI.SetCompleteQuestIcon(false);
        //         return;
        //     }
        //
        //     if (quest.currentQuestStep.info.questGiver == null)
        //     {
        //         return;
        //     }
        //     
        //     
        //  
        //     if (GetComponent<CharacterController>())
        //     {
        //         
        //         if (GetComponent<CharacterController>().character.info.id == quest.currentQuestStep.info.id)
        //         {
        //             
        //             if (quest.currentQuestStep.Status == QuestObjectiveStatus.Complete)
        //             {
        //                 
        //                 QuestIcon questIcon = Instantiate(QuestManager.Instance.QuestFinishMarkerPF, transform);
        //                 questIcon.transform.localPosition = Vector3.zero;
        //                 questIcon.gameObject.SetActive(true);
        //                 questIcon.quest = quest;
        //                 
        //                
        //                 
        //                 QuestIconUI questIconUI = Instantiate(QuestManager.Instance.QuestFinishMarkerUIPF, questIconsUI.transform);
        //                 questIconUI.transform.localPosition = Vector3.zero;
        //                 questIconUI.gameObject.SetActive(true);
        //                 questIconUI.quest = quest;
        //                 
        //                 
        //                 //questIcons.SetStartQuestIcon(questGiverContainer.startQuest);
        //                 //questIconsUI.SetStartQuestIcon(true);
        //             }
        //             else
        //             {
        //                 DestroyIcons(quest);
        //             }
        //         }
        //     }
        //     
        //     ////////OLD////////
        //     
        //     // if (questGiverContainers.Any(x => x.questDataContainer.id == quest.info.id))
        //     // {
        //     //     
        //     //     if (quest.Status != QuestStatus.Active)
        //     //     {
        //     //         questIcons.SetCompleteQuestIcon(false);
        //     //         questIconsUI.SetCompleteQuestIcon(false);
        //     //         return;
        //     //     }
        //     //
        //     //
        //     //     string questStepDataContainerId = "";
        //     //     
        //     //     if (questGiverContainers.Any(x =>
        //     //         {
        //     //             
        //     //             if (x.questStepDataContainer != null)
        //     //             {
        //     //                 questStepDataContainerId = x.questStepDataContainer.id;
        //     //                 return x.questStepDataContainer.id == quest.currentQuestStep.info.id;
        //     //             }
        //     //
        //     //             return false;
        //     //         }))
        //     //     {
        //     //         questIcons.SetCompleteQuestIcon(true);
        //     //         questIconsUI.SetCompleteQuestIcon(true);
        //     //     }
        //     //     else
        //     //     {
        //     //         questIcons.SetCompleteQuestIcon(false);
        //     //         questIconsUI.SetCompleteQuestIcon(false);
        //     //     }
        //     //
        //     //
        //     //     // foreach (QuestGiverContainer questGiverContainer in questGiverContainers)
        //     //     // {
        //     //     //     if (questGiverContainer.questStepDataContainer != null)
        //     //     //     {
        //     //     //         if (quest.currentQuestStep.info.id == questGiverContainer.questStepDataContainer.id)
        //     //     //         {
        //     //     //             if (quest.currentQuestStep.Status == QuestObjectiveStatus.Active)
        //     //     //             {
        //     //     //             
        //     //     //                 questIcons.SetCompleteQuestIcon(true);
        //     //     //             }
        //     //     //         }
        //     //     //     } 
        //     //     // }
        //     // }
        //     // else
        //     // {
        //     //     questIcons.SetCompleteQuestIcon(false);
        //     //     questIconsUI.SetCompleteQuestIcon(false);
        //     // }
        // }


        public void DestroyIcons()
        {
            List<QuestIcon> questMarkerIcons = GetComponentsInChildren<QuestIcon>().ToList();
            List<QuestIconUI> questMarkerUIIcons = GetComponentsInChildren<QuestIconUI>().ToList();
            
            foreach (var questMarkerIcon in questMarkerIcons)
            {
                Destroy(questMarkerIcon.gameObject);
            }
            
            foreach (var questMarkerIcon in questMarkerUIIcons)
            {
                Destroy(questMarkerIcon.gameObject);
            }
            
        }

        public void DestroyIcons(Quest quest)
        {
            
            List<QuestIcon> questMarkerIcons = GetComponentsInChildren<QuestIcon>().ToList();
            List<QuestIconUI> questMarkerUIIcons = GetComponentsInChildren<QuestIconUI>().ToList();

            QuestIcon questIcon = questMarkerIcons.Find(x => x.quest.info.id == quest.info.id);
            QuestIconUI questIconUI = questMarkerUIIcons.Find(x => x.quest.info.id == quest.info.id);

            if (questIcon != null)
            {
                Destroy(questIcon.gameObject);
            }
                        
            if (questIconUI != null)
            {
                Destroy(questIconUI.gameObject);
            }
        }

    }
}