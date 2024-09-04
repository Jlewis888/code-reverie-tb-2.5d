using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace CodeReverie
{
    public class QuestObjectiveInteractiveHelper : SerializedMonoBehaviour, IInteractable
    {
        public QuestStepObjectiveListener questStepObjectiveListener;
        public float holdTime;
        public float holdTimer;
        public bool canInteract;
        [SerializeField] public bool _canInteract;
        [SerializeField] private string _interactableMessage;
        private void Awake()
        {
            CanInteract = true;
            interactableType = InteractableType.Dialogue;
            removeOnInteractComplete = false;
            
            if (_interactableMessage.IsNullOrWhitespace())
            {
                _interactableMessage = $"NEED TO UPDATE INTERACTABLE MESSAGE ON OBJECT";
            }
        }

        public int Priority { get; }
        public InteractableType interactableType { get; set; }
        
        public string interactableMessage
        {
            get => _interactableMessage;
            set => _interactableMessage = value;
        }

        public bool removeOnInteractComplete { get; set; }

        public void Interact(Action onComplete) { }

        public void InteractOnPress(Action onComplete) { }

        public void InteractOnHold(Action onComplete)
        {
            if (CanInteract)
            {
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.gameObject.SetActive(true);
                
                holdTimer += Time.deltaTime;
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.maxValue = holdTime;
                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.value = holdTimer;

                if (holdTimer >= holdTime)
                {
                  
                    
                    CanInteract = false;
                    Reset();
                    EventManager.Instance.playerEvents.OnObjectInteracted(questStepObjectiveListener.id);
                    onComplete();
                }
            }
        }

        public void InteractOnPressUp(Action onComplete)
        {
            Reset();
        }

        public bool CanInteract
        {
            get => _canInteract;
            set => _canInteract = value;
        }


        public void Reset()
        {
            holdTimer = 0;

            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.value = 0;
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.interactionSlider.gameObject.SetActive(false);
        }
    }
}