using System;
using Sirenix.OdinInspector;
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

        private void Awake()
        {
            CanInteract = true;
        }

        public int Priority { get; }
        public void Interact() { }

        public void InteractOnPress(Action onComplete) { }

        public void InteractOnHold(Action onComplete)
        {
            if (CanInteract)
            {
                CanvasManager.Instance.hudManager.interactionSlider.gameObject.SetActive(true);
                
                holdTimer += Time.deltaTime;
                CanvasManager.Instance.hudManager.interactionSlider.maxValue = holdTime;
                CanvasManager.Instance.hudManager.interactionSlider.value = holdTimer;

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

            CanvasManager.Instance.hudManager.interactionSlider.value = 0;
            CanvasManager.Instance.hudManager.interactionSlider.gameObject.SetActive(false);
        }
    }
}