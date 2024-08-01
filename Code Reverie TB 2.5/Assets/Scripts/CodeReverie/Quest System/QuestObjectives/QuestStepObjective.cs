using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;

namespace CodeReverie
{
    public class QuestStepObjective
    {
        //public QuestStepObjectiveData info;
        public string questId;
        public string questObjectiveDescription;
        public string questObjectiveDescriptionHud;
        public string questObjectiveDescriptionMenu;
        public QuestObjectiveType questObjectiveType;
        public int requiredAmount = 1;
        public bool showRequiredAmount;
        public List<string> questTriggerIdList = new List<string>();
        public bool optional;
        public bool requiresItemsToComplete;
        public Dictionary<ItemInfo, int> requiredItems = new Dictionary<ItemInfo, int>();
        protected bool isComplete;
        public QuestObjectiveStatus questObjectiveStatus;
        protected int currentCount = 0;
        public QuestStepObjectiveListener questStepObjectiveListener;


        public QuestStepObjective()
        {
            ObjectiveStatus = QuestObjectiveStatus.Inactive;
        }
        
        // public QuestStepObjective(QuestStepObjectiveData questObjectiveData)
        // {
        //     this.info = questObjectiveData;
        //     ObjectiveStatus = QuestObjectiveStatus.Inactive;
        // }


        public QuestObjectiveStatus ObjectiveStatus
        {
            get { return questObjectiveStatus; }

            set
            {
                if (value != questObjectiveStatus)
                {
                    questObjectiveStatus = value;

                    switch (value)
                    {
                        case QuestObjectiveStatus.Active:
                            SubscribeQuestTriggers();
                            break;
                        case QuestObjectiveStatus.Complete:
                            EventManager.Instance.questEvents.OnQuestObjectiveComplete(this);
                            UnsubscribeQuestTriggers();
                            
                            break;
                    }
                }
            }
        }
        
        
        // public virtual void QuestTrigger(string triggerId)
        // {
        //     
        //     if (!MetRequiredItemsCheck())
        //     {
        //         return;
        //     }
        //     
        //     if (info.questTriggerIdList.Contains(triggerId))
        //     {
        //         CurrentCount++;
        //     }
        //
        //
        //     if (CurrentCount >= info.requiredAmount)
        //     {
        //         ObjectiveStatus = QuestObjectiveStatus.Complete;
        //     }
        //     
        // }

        // public void KillQuestTrigger(string triggerId)
        // {
        //     
        //     if (!MetRequiredItemsCheck())
        //     {
        //         return;
        //     }
        //
        //     if (info is KillQuestObjectiveData)
        //     {
        //         KillQuestObjectiveData killQuestObjectiveData = info as KillQuestObjectiveData;
        //         
        //         if (killQuestObjectiveData.killQuestTriggerList.Any(x => x.id == triggerId))
        //         {
        //             CurrentCount++;
        //         }
        //         
        //         if (CurrentCount >= info.requiredAmount)
        //         {
        //             ObjectiveStatus = QuestObjectiveStatus.Complete;
        //         }
        //     }
        // }
        //
        // public void SpeakToPersonQuestTrigger(string id)
        // {
        //     
        //     if (!MetRequiredItemsCheck())
        //     {
        //         return;
        //     }
        //     
        //    
        //     if (info is SpeakToPersonQuestObjectiveData && info.questId == id)
        //     {
        //        
        //         CurrentCount++;
        //         // SpeakToPersonQuestObjectiveData speakToPersonQuestObjective = info as SpeakToPersonQuestObjectiveData;
        //         //
        //         //
        //         // if (speakToPersonQuestObjective.characterDataContainer != null)
        //         // {
        //         //     CurrentCount++;
        //         // }
        //         
        //         if (CurrentCount >= info.requiredAmount)
        //         {
        //             ObjectiveStatus = QuestObjectiveStatus.Complete;
        //         }
        //         
        //     }
        // }
        //
        // public void EscortQuestTrigger(string id)
        // {
        //     
        //     if (!MetRequiredItemsCheck())
        //     {
        //         return;
        //     }
        //     
        //     if (questObjectiveStatus == QuestObjectiveStatus.Active)
        //     {
        //       
        //         if (info is EscortQuestObjectiveData && info.questId == id)
        //         {
        //             CurrentCount++;
        //         }
        //     
        //         if (CurrentCount >= info.requiredAmount)
        //         {
        //             ObjectiveStatus = QuestObjectiveStatus.Complete;
        //         } 
        //     }
        //     
        // }
        //
        //
        // public void SearchQuestTrigger(string id)
        // {
        //     
        //     if (!MetRequiredItemsCheck())
        //     {
        //         return;
        //     }
        //     
        //     if (questObjectiveStatus == QuestObjectiveStatus.Active)
        //     {
        //         
        //         if (info is SearchQuestObjectiveData && info.questId == id)
        //         {
        //             CurrentCount++;
        //         }
        //     
        //         if (CurrentCount >= info.requiredAmount)
        //         {
        //             ObjectiveStatus = QuestObjectiveStatus.Complete;
        //         } 
        //     }
        //     
        // }
        //
        //
        // public void ItemPickupTrigger(string id, int count)
        // {
        //     
        //     if (!MetRequiredItemsCheck())
        //     {
        //         return;
        //     }
        //     
        //     if (info is GatherQuestObjectiveData)
        //     {
        //         
        //         GatherQuestObjectiveData questObjectiveData = info as GatherQuestObjectiveData;
        //         
        //         if (questObjectiveData.itemInfoList.Any(x => x.id == id))
        //         {
        //             CurrentCount++;
        //         }
        //         
        //         if (CurrentCount >= info.requiredAmount)
        //         {
        //             ObjectiveStatus = QuestObjectiveStatus.Complete;
        //         }
        //     }
        // }
        //
        //
        // public void ItemCraftTrigger(string id)
        // {
        //     
        //     if (!MetRequiredItemsCheck())
        //     {
        //         return;
        //     }
        //     
        //     if (info is CraftItemObjectiveData)
        //     {
        //         
        //         CraftItemObjectiveData questObjectiveData = info as CraftItemObjectiveData;
        //         
        //         if (questObjectiveData.itemInfoList.Any(x => x.id == id))
        //         {
        //             CurrentCount++;
        //         }
        //         
        //         if (CurrentCount >= info.requiredAmount)
        //         {
        //             ObjectiveStatus = QuestObjectiveStatus.Complete;
        //         }
        //     }
        // }
        //
        // public void ObjectInteracted(string id)
        // {
        //
        //     if (!MetRequiredItemsCheck())
        //     {
        //         return;
        //     }
        //     
        //     if (info.questId == id)
        //     {
        //         
        //         CurrentCount++;
        //         
        //         if (CurrentCount >= info.requiredAmount)
        //         {
        //             ObjectiveStatus = QuestObjectiveStatus.Complete;
        //         }
        //     }
        // }
        

        protected virtual void SubscribeQuestTriggers()
        {

            // switch (info.questObjectiveType)
            // {
            //     case QuestObjectiveType.Kill:
            //         EventManager.Instance.combatEvents.onDeath += KillQuestTrigger;
            //         break;
            //     case QuestObjectiveType.Meet:
            //         EventManager.Instance.playerEvents.onDialogueChoiceSelection += SpeakToPersonQuestTrigger;
            //         break;
            //     case QuestObjectiveType.Escort:
            //         EventManager.Instance.questEvents.onEscortTargetEnter += EscortQuestTrigger;
            //         break;
            //     case QuestObjectiveType.Gather:
            //         EventManager.Instance.playerEvents.onItemPickup += ItemPickupTrigger;
            //         break;
            //     case QuestObjectiveType.Search:
            //         EventManager.Instance.playerEvents.onSearch += SearchQuestTrigger;
            //         break;
            //     case QuestObjectiveType.Craft:
            //         EventManager.Instance.playerEvents.onItemCrafted += ItemCraftTrigger;
            //         break;
            //     case QuestObjectiveType.Interact:
            //         EventManager.Instance.playerEvents.onObjectInteracted += ObjectInteracted;
            //         break;
            // }
            
            
            
        }
        
        protected virtual void UnsubscribeQuestTriggers()
        {
            // switch (info.questObjectiveType)
            // {
            //     case QuestObjectiveType.Kill:
            //         EventManager.Instance.combatEvents.onDeath -= KillQuestTrigger;
            //         break;
            //     case QuestObjectiveType.Meet:
            //         EventManager.Instance.playerEvents.onDialogueChoiceSelection -= SpeakToPersonQuestTrigger;
            //         break;
            //     case QuestObjectiveType.Escort:
            //         EventManager.Instance.questEvents.onEscortTargetEnter -= EscortQuestTrigger;
            //         break;
            //     case QuestObjectiveType.Gather:
            //         EventManager.Instance.playerEvents.onItemPickup -= ItemPickupTrigger;
            //         break;
            //     case QuestObjectiveType.Search:
            //         EventManager.Instance.playerEvents.onSearch -= SearchQuestTrigger;
            //         break;
            //     case QuestObjectiveType.Interact:
            //         EventManager.Instance.playerEvents.onObjectInteracted -= ObjectInteracted;
            //         break;
            // }
            
        }

        public int CurrentCount
        {
            get { return currentCount; }

            set
            {
                if (currentCount != value)
                {
                    currentCount = value;
                    
                    EventManager.Instance.questEvents.OnQuestObjectiveUpdate(this);
                    
                }
            }
        }

        public bool MetRequiredItemsCheck()
        {

            if (requiredItems.Count <= 0)
            {
                Debug.Log("Meet Requirements");
                return true;
            }

            foreach (KeyValuePair<ItemInfo, int> requiredItem in requiredItems)
            {
                (bool, int) itemCheck = PlayerManager.Instance.inventory.ItemInInventory(requiredItem.Key);

                if (!itemCheck.Item1)
                {
                    Debug.Log("Do No Meet Requirements: Item Not in Inventory");
                    return false;
                }

                if (PlayerManager.Instance.inventory.items[itemCheck.Item2].amount < requiredItem.Value)
                {
                    Debug.Log($"Do No Meet Requirements: Need {requiredItem.Value} only have {PlayerManager.Instance.inventory.items[itemCheck.Item2].amount}");
                    return false;
                }
                
            }
            
            Debug.Log("Meet Requirements");
            
            return true;
        }
        
    }
}