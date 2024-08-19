using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class DialogueTrigger : SerializedMonoBehaviour, IInteractable
    {
        private const int priority = 3;
        public DialogueSpeaker dialogueSpeaker;
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

        public void Interact()
        {
            
        }

        public void InteractOnPress(Action onComplete)
        {
            EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.dialogueManager);
                    
            EventManager.Instance.playerEvents.OnDialogueStart(dialogueSpeaker.dialogueTextAsset, dialogueSpeaker, storyPath);
            onComplete();
        }
        public void InteractOnHold(Action onComplete) { }
        public void InteractOnPressUp(Action onComplete) {}


        public bool CanInteract { get; set; }
    }
}