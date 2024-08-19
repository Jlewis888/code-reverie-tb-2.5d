using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public interface IInteractable
    {
        public int Priority { get; }

        public InteractableType interactableType { get; set; }
        public string interactableMessage { get; set; }

        public void Interact();
        public void InteractOnPress(Action onComplete);
        public void InteractOnHold(Action onComplete);
        
        public void InteractOnPressUp(Action onComplete);
        
        public bool CanInteract { get; set; }

    }
}