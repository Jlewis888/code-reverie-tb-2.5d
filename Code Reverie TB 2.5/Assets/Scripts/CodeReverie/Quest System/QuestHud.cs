using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class QuestHud : SerializedMonoBehaviour
    {
        public GameObject trackedQuestHudHolder;
        public TrackedQuestHud trackedQuestHudPF;
        public List<TrackedQuestHud> trackedQuestHuds = new List<TrackedQuestHud>();
        private const int MAX_ACTIVE_QUEST = 3;
        public List<LayoutGroup> layoutGroups;


        private void OnEnable()
        {
            EventManager.Instance.questEvents.onQuestTrackingStatusChange += AddRemoveTrackedQuest;
            UpdateTrackedQuest();
            //GetComponent<ContentFitterRefresh>().RefreshContentFitters();
            // foreach (TrackedQuestHud trackedQuestHud in trackedQuestHuds)
            // {
            //     LayoutRebuilder.ForceRebuildLayoutImmediate(trackedQuestHud.transform as RectTransform);
            // }
            
        }
        
        private void OnDisable()
        {
            EventManager.Instance.questEvents.onQuestTrackingStatusChange -= AddRemoveTrackedQuest;
            Clear();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
               
                
                Quest quest = QuestManager.Instance.GetQuestById("TestQuest");

                quest.IsTracking = !quest.IsTracking;
            }
        }

        public void UpdateTrackedQuest()
        {
            Clear();

            foreach (Quest quest in QuestManager.Instance.quests.Values)
            {
                if (quest.IsTracking && quest.Status == QuestStatus.Active)
                {
                    TrackedQuestHud trackedQuestHud = Instantiate(trackedQuestHudPF, trackedQuestHudHolder.transform);
                    trackedQuestHud.gameObject.SetActive(false);
                    trackedQuestHud.quest = quest;
                    trackedQuestHud.gameObject.SetActive(true);
                    trackedQuestHuds.Add(trackedQuestHud);
                }
            }
        }

        public void Clear()
        {
            foreach (Transform child in trackedQuestHudHolder.transform)
            {
                Destroy(child.gameObject);
            }

            trackedQuestHuds = new List<TrackedQuestHud>();
        }

        public void AddRemoveTrackedQuest(Quest quest, bool isTracking)
        {
            layoutGroups = new List<LayoutGroup>(GetComponentsInChildren<LayoutGroup>());
            if (isTracking)
            {
                //Canvas.ForceUpdateCanvases();
                
                TrackedQuestHud trackedQuestHud = Instantiate(trackedQuestHudPF, trackedQuestHudHolder.transform);
                trackedQuestHud.gameObject.SetActive(false);
                trackedQuestHud.quest = quest;
                trackedQuestHud.gameObject.SetActive(true);
                trackedQuestHuds.Add(trackedQuestHud);
                //trackedQuestHud.UpdateLayout();
                // LayoutRebuilder.ForceRebuildLayoutImmediate(trackedQuestHudHolder.transform as RectTransform);
                // LayoutRebuilder.ForceRebuildLayoutImmediate(trackedQuestHudHolder.transform as RectTransform);
                //
               
                // trackedQuestHudHolder.GetComponent<VerticalLayoutGroup>().enabled = false;
                // trackedQuestHudHolder.GetComponent<VerticalLayoutGroup>().enabled = true;
                //
                // Debug.Log("Here");
              
            }
            // GetComponent<VerticalLayoutGroup>().enabled = false;
            // GetComponent<VerticalLayoutGroup>().enabled = true;
            // foreach (TrackedQuestHud trackedQuestHud in trackedQuestHuds)
            // {
            //     trackedQuestHud.UpdateLayout();
            // }
            
            UpdateTrackedQuest();
            GetComponent<ContentFitterRefresh>().RefreshContentFitters();
            
            //RebuildLayout();
        }
        
        // public void RebuildLayout()
        // {
        //     if (gameObject.activeInHierarchy)
        //     {
        //         StartCoroutine(WaitOneFrameThenRebuild());
        //     }
        //     
        // }
        //
        // private IEnumerator WaitOneFrameThenRebuild()
        // {
        //     layoutGroups = new List<LayoutGroup>(GetComponentsInChildren<LayoutGroup>());
        //     foreach (var layoutgroup in layoutGroups)
        //     {
        //
        //         // if (layoutgroup.gameObject != this.gameObject)
        //         // {
        //         //     layoutgroup.gameObject.SetActive(false);
        //         // } 
        //         
        //         LayoutRebuilder.ForceRebuildLayoutImmediate(layoutgroup.transform as RectTransform);
        //         LayoutRebuilder.ForceRebuildLayoutImmediate(layoutgroup.transform as RectTransform);
        //         LayoutRebuilder.MarkLayoutForRebuild(layoutgroup.transform as RectTransform);
        //         layoutgroup.enabled = false;
        //         // layoutgroup.gameObject.SetActive(false);
        //     }
        //
        //     yield return new WaitForEndOfFrame();
        //     foreach (var layoutgroup in layoutGroups)
        //     {
        //         layoutgroup.gameObject.SetActive(true);
        //         LayoutRebuilder.ForceRebuildLayoutImmediate(layoutgroup.transform as RectTransform);
        //         LayoutRebuilder.ForceRebuildLayoutImmediate(layoutgroup.transform as RectTransform);
        //         LayoutRebuilder.MarkLayoutForRebuild(layoutgroup.transform as RectTransform);
        //         layoutgroup.enabled = true;
        //         
        //     }
        //
        //   
        // }
    }
}