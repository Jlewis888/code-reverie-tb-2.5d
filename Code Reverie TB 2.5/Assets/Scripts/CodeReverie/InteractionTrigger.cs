using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class InteractionTrigger  : SerializedMonoBehaviour
    {
        public bool playerInRange;
        public List<IInteractable> interactables = new List<IInteractable>();


        private void Update()
        {
            if (playerInRange)
            {
                
            }
        }
        
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.TryGetComponent(out CharacterGroundCollider groundCheck))
            {
                if (groundCheck.GetComponentInParent<ComponentTagManager>().HasTag(ComponentTag.Player))
                {
                    playerInRange = true;
                    interactables = GetComponentsInParent<IInteractable>().OrderBy(x => x.Priority).ToList();
                } 
                
               
            }
        }
        
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out CharacterGroundCollider groundCheck))
            {
                if (groundCheck.GetComponentInParent<ComponentTagManager>().HasTag(ComponentTag.Player))
                {
                    playerInRange = false;
                } 
                
            }
        }
        
    }
}