using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace CodeReverie
{
    public class QuestManager : ManagerSingleton<QuestManager>
    {
        public QuestListContainer questListContainer;
        public Dictionary<string, Quest> quests = new Dictionary<string, Quest>();
        public Dictionary<string, QuestStep> questSteps = new Dictionary<string, QuestStep>();
        public QuestIcon QuestStartMarkerPF;
        public QuestIconUI QuestStartMarkerUIPF;
        public QuestIcon QuestFinishMarkerPF;
        public QuestIconUI QuestFinishMarkerUIPF;


        protected override void Awake()
        {
            base.Awake();
            quests = new Dictionary<string, Quest>();
            Initialize();
        }
        
        public bool Initialized { get; set; }
        
        public void Initialize()
        {
            
            Init();
            Initialized = true;
        }

        private void OnEnable()
        {
            EventManager.Instance.questEvents.onStartQuest += StartQuest;
            // EventManager.Instance.questEvents.onAdvanceQuest += AdvanceQuest;
            // EventManager.Instance.questEvents.onCompleteQuest += CompleteQuest;
        }

        private void OnDisable()
        {
            EventManager.Instance.questEvents.onStartQuest -= StartQuest;
            // EventManager.Instance.questEvents.onAdvanceQuest -= AdvanceQuest;
            // EventManager.Instance.questEvents.onCompleteQuest -= CompleteQuest;
        }

        // Start is called before the first frame update
        void Start()
        {
            foreach (KeyValuePair<string, Quest> quest in quests)
            {
                EventManager.Instance.questEvents.OnQuestStateChange(quest.Value);
            }
            
            //StartQuest("TestEscortMissionQuest");
            //GetQuestById("TestQuest").IsTracking = true;
            StartQuest("TestQuest");
            //CompleteQuest("TestQuest2");
            //StartQuest("TestSideQuest");
            //CompleteQuest("TestSideQuest2");
            
            //StartQuestById("FindCecil", 2);
            
        }

        // Update is called once per frame
        void Update()
        {
            foreach (KeyValuePair<string, Quest> quest in quests)
            {
                if (quest.Value.Status == QuestStatus.Inactive && CheckQuestRequirements(quest.Value))
                {
                    ChangeQuestStatus(quest.Value.info.id, QuestStatus.Available);
                }
            }
        }


        public void Init()
        {
            quests = new Dictionary<string, Quest>();
            quests = GetAllQuest();
            questSteps = GetAllQuestSteps();
            
            Initialized = true;
        }

        Dictionary<string, Quest> GetAllQuest()
        {
            //QuestDataContainer[] questDetailsList = Resources.LoadAll<QuestDataContainer>("Quests");

            Dictionary<string, Quest> allQuest = new Dictionary<string, Quest>();

            foreach (QuestDataContainer questDetails in questListContainer.questDataContainers)
            {
                if (allQuest.ContainsKey(questDetails.id))
                {
                    Debug.Log("Duplicate Quest IDs");
                }
                else
                {
                    allQuest.Add(questDetails.id, new Quest(questDetails));
                }
                
                
                
            }
            
            return allQuest;

        }
        
        Dictionary<string, QuestStep> GetAllQuestSteps()
        {
            //QuestDataContainer[] questDetailsList = Resources.LoadAll<QuestDataContainer>("Quests");

            Dictionary<string, QuestStep> allQuest = new Dictionary<string, QuestStep>();

            foreach (Quest quest in quests.Values)
            {

                foreach (var questStep in quest.questSteps)
                {
                    if (allQuest.ContainsKey(questStep.info.id))
                    {
                        Debug.Log("Duplicate Quest IDs");
                    }
                    else
                    {
                        allQuest.Add(questStep.info.id, questStep);  
                    }
                    
                }
                
            }
            
            return allQuest;

        }
        

        public Quest GetQuestById(string id)
        {
            Quest quest = quests[id];

            if (quest == null)
            {
                Debug.Log("Quest is null");
            }

            return quest;
        }


        public void StartQuest(string id)
        {
            Quest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestObjective(transform);
            ChangeQuestStatus(id, QuestStatus.Active);
            
            if (quest.info.questType == QuestType.Main)
            {
                quest.IsTracking = true;
            }
            
            
        }

        // public void AdvanceQuest(string id)
        // {
        //     Quest quest = GetQuestById(id);
        //     
        //     //quest.NextQuestObjective();
        //     
        //     quest.CompleteCurrentQuestObjective();
        //     
        //     if (quest.LastQuestObjectiveCompleteCheck())
        //     {
        //         ChangeQuestStatus(quest.info.id, QuestStatus.PendingComplete);
        //         //quest.InstantiateCurrentQuestObjective(transform);
        //     }
        //     else
        //     {
        //         quest.NextQuestObjective();
        //         
        //     }
        // }
        //
        // public void CompleteQuest(string id)
        // {
        //     Quest quest = GetQuestById(id);
        //     
        //     ClaimRewards(quest);
        //     
        //     ChangeQuestStatus(quest.info.id, QuestStatus.Complete);
        // }

        public void ChangeQuestStatus(string id, QuestStatus questStatus)
        {
            Quest quest = GetQuestById(id);

            quest.Status = questStatus;
            
            // EventManager.Instance.questEvents.QuestStateChange(quest);
        }

        public bool CheckQuestRequirements(Quest quest)
        {
            bool meetsRequirements = true;


            foreach (QuestDataContainer questDetails in quest.info.questDetailsPrerequisites)
            {
                if (GetQuestById(questDetails.id).Status != QuestStatus.Complete)
                {
                    meetsRequirements = false;
                }
            }
            
            return meetsRequirements;
        }


        public void ClaimRewards(Quest quest)
        {
            //TODO set quest rewards
          
        }

        public List<Quest> GetActiveQuest()
        {
            List<Quest> activeQuests = new List<Quest>();

            foreach (Quest quest in quests.Values)
            {
                if (quest.Status == QuestStatus.Active)
                {
                    activeQuests.Add(quest);
                }
            }
            
            return activeQuests;
        }

        public void StartQuestById(string questID, int questStepIndex = 0)
        {
            StartQuest(questID);

            if (questStepIndex > 0)
            {
                for (int i = 0; i < questStepIndex; i++)
                {
                    GetQuestById(questID).questSteps[i].Status = QuestObjectiveStatus.Complete;
                }
            
                GetQuestById(questID).currentQuestObjectiveIndex = questStepIndex;
                GetQuestById(questID).CurrentQuestObjective = GetQuestById(questID).questSteps[questStepIndex];
                GetQuestById(questID).CurrentQuestObjective.Status = QuestObjectiveStatus.Active;
            }
            
            
           
        }

    }
}
