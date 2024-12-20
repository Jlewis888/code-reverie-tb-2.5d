﻿using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using Unity.Behavior;
using UnityEngine;
using Action = System.Action;

namespace CodeReverie
{
    public class DialogueTrigger : SerializedMonoBehaviour, IInteractable
    {
        private const int priority = 3;
        public DialogueSpeaker dialogueSpeaker;
        public DialogueGraphAsset dialogueGraphAsset;
        public BehaviorGraph behaviorGraph;
        public bool playerInRange;
        public bool instantTrigger;
        public string storyPath;
        [SerializeField] public string _interactableMessage;
        
        private void Awake()
        {

            // if (GetComponentInParent<DialogueSpeaker>())
            // {
            //     dialogueSpeaker = GetComponentInParent<DialogueSpeaker>();
            // }
            
            dialogueSpeaker = GetComponent<DialogueSpeaker>();
            interactableType = InteractableType.Dialogue;
            removeOnInteractComplete = false;

            CanInteract = true;
            
            
        }

        private void Start()
        {
            if (_interactableMessage.IsNullOrWhitespace())
            {
                _interactableMessage = $"Speak with {dialogueSpeaker.GetComponent<CharacterUnitController>().character.info.characterName}";
            }
        }


        public int Priority
        { 
            get { return priority; }
        }

        public InteractableType interactableType { get; set; }


        public string interactableMessage
        {
            get => _interactableMessage;
            set => _interactableMessage = value;
        }

        public bool removeOnInteractComplete { get; set; }

        public void Interact(Action onComplete)
        {
            Debug.Log("Start Dialogue");
            EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.dialogueManager);
                    
            //EventManager.Instance.playerEvents.OnDialogueStart(dialogueSpeaker.dialogueTextAsset, dialogueSpeaker.GetComponent<CharacterUnitController>().character.info, storyPath);
            EventManager.Instance.playerEvents.OnDialogueStart(behaviorGraph);
            onComplete();
        }

        public void InteractOnPress(Action onComplete)
        {
            EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.dialogueManager);
                    
            //EventManager.Instance.playerEvents.OnDialogueStart(dialogueSpeaker.dialogueTextAsset, dialogueSpeaker.GetComponent<CharacterUnitController>().character.info, storyPath);
            EventManager.Instance.playerEvents.OnDialogueStart(behaviorGraph);
            onComplete();
        }
        public void InteractOnHold(Action onComplete) { }
        public void InteractOnPressUp(Action onComplete) {}


        public bool CanInteract { get; set; }
    }
}