using System;
using UnityEngine;

namespace CodeReverie
{
    public class PlayerEvents
    {

        public Action<bool> onPlayerLock;

        public void OnPlayerLock(bool locked)
        {
            onPlayerLock?.Invoke(locked);
        }
        
        
        public Action onPlayerSpawn;

        public void OnPlayerSpawn()
        {
            onPlayerSpawn.Invoke();
        }

        public Action onLevelUp;

        public void OnLevelUp()
        {
            onLevelUp?.Invoke();
        }

        public Action onCharacterInitialize;

        public void OnCharacterInitialize()
        {
            onCharacterInitialize?.Invoke();
        }

        public Action<Character> onCharacterSwapComplete;

        public void OnCharacterSwapComplete(Character character)
        {
            onCharacterSwapComplete?.Invoke(character);
        }

        public Action onCharacterSwap;

        public void OnCharacterSwap()
        {
            onCharacterSwap?.Invoke();
        }
        
        
        public Action onCharacterMenuSwap;

        public void OnCharacterMenuSwap()
        {
            onCharacterMenuSwap?.Invoke();
        }
        
        

        public Action onPartyUpdate;

        public void OnPartyUpdate()
        {
            onPartyUpdate?.Invoke();
        }

        public Action<Character> onGearUpdate;

        public void OnGearUpdate(Character character)
        {
            onGearUpdate?.Invoke(character);
        }

        public Action onSkillSlotUpdate;

        public void OnSkillSlotUpdate()
        {
            onSkillSlotUpdate?.Invoke();
        }

        public Action onActionBarSet;

        public void OnActionBarSet()
        {
            onActionBarSet?.Invoke();
        }

        public Action<TextAsset, DialogueSpeaker, String> onDialogueStart;


        public void OnDialogueStart(TextAsset inkJSON, DialogueSpeaker dialogueSpeaker, string storyPath)
        {
            onDialogueStart?.Invoke(inkJSON, dialogueSpeaker, storyPath);
        }
        
        public Action<DialogueSpeaker> onDialogueEnd;


        public void OnDialogueEnd(DialogueSpeaker dialogueSpeaker)
        {
            onDialogueEnd?.Invoke(dialogueSpeaker);
        }
        

        public Action onDodgeStart;
        public Action onDodgeEnd;

        public void OnDodgeStart()
        {
            onDodgeStart?.Invoke();
        }

        public void OnDodgeEnd()
        {
            onDodgeEnd?.Invoke();
        }

        
        // public Action onDashStart;
        // public Action onDashEnd;
        //
        // public void OnDashStart()
        // {
        //     onDashStart?.Invoke();
        // }
        //
        // public void OnDashEnd()
        // {
        //     onDashEnd?.Invoke();
        // }
        
        
        public Action<string> onDialogueChoiceSelection;
        public void OnDialogueChoiceSelection(string id)
        {
            onDialogueChoiceSelection?.Invoke(id);
        }

        public Action<string, int> onItemPickup;

        public void OnItemPickup(string id, int pickupAmount)
        {
            onItemPickup?.Invoke(id, pickupAmount);
        }
        
        
        public Action<string> onSearch;

        public void OnSearch(string id)
        {
            onSearch?.Invoke(id);
        }
        
        
        public Action<string> onItemCrafted;

        public void OnItemCrafted(string id)
        {
            onItemCrafted?.Invoke(id);
        }
        
        public Action<string> onObjectInteracted;

        public void OnObjectInteracted(string id)
        {
            onObjectInteracted?.Invoke(id);
        }
        
    }
}