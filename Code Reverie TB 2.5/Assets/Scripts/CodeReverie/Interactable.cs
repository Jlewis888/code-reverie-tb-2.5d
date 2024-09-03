using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class Interactable : SerializedMonoBehaviour
    {
        public List<IInteractable> interactables = new List<IInteractable>();
        public Queue<IInteractable> interactableQueue = new Queue<IInteractable>();

        private void Awake()
        {
            interactables = GetComponentsInParent<IInteractable>().OrderBy(x => x.Priority).ToList();
        }

        public bool HasActiveInteractables()
        {

            foreach (IInteractable interactable in interactables)
            {
                if (interactable.CanInteract)
                {
                    return true;
                }
            }
            
            return false;
        }


        public void Activate()
        {

            // if (GetComponent<Renderer>())
            // {
            //     GetComponent<Renderer>().material.SetFloat("_Thickness", 2f);
            //     GetComponent<Renderer>().material.color = Color.red;
            // }
            //
            // if (GetComponentInChildren<CharacterUnit>())
            // {
            //     GetComponentInChildren<CharacterUnit>().GetComponent<Renderer>().material.SetFloat("_Thickness", 2f);
            //     GetComponentInChildren<CharacterUnit>().GetComponent<Renderer>().material.color = Color.red;
            // }
            
            
        }

        public void Deactivate()
        {
            
            // if (GetComponent<Renderer>())
            // {
            //     GetComponent<Renderer>().material.SetFloat("_Thickness", 0f);
            //     
            // }
            //
            // if (GetComponentInChildren<CharacterUnit>())
            // {
            //     GetComponentInChildren<CharacterUnit>().GetComponent<Renderer>().material.SetFloat("_Thickness", 0f);
            // }
            
        }

        public void SetQueue()
        {
            interactableQueue = new Queue<IInteractable>(interactables);
        }
        
        
        
    }
}