using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class PauseMenu : SerializedMonoBehaviour
    {
        public PauseMenuNavigationState pauseMenuNavigationState;
        public PauseMenuNavigationState pauseMenuNavigationPreviousState;
        public PauseMenuSubNavigationState pauseMenuSubNavigationState;
        public GameObject pauseMenuNavigationHolder;
        public MenuNavigation pauseMenuNavigation;
        
        public void ClearNavigationButtons()
        {
            //pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
            
            foreach (Transform child in pauseMenuNavigationHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void SetListeners()
        {
            EventManager.Instance.generalEvents.onPauseMenuNavigationStateChange += OnPauseMenuNavigationStateChange;
        }

        public void UnsetListeners()
        {
            EventManager.Instance.generalEvents.onPauseMenuNavigationStateChange -= OnPauseMenuNavigationStateChange;
        }
        
        
        public void OnPauseMenuNavigationStateChange(PauseMenuNavigationState pauseMenuNavigationState)
        {
            if (pauseMenuNavigationState == this.pauseMenuNavigationState)
            {
                gameObject.SetActive(true);
                CanvasManager.Instance.pauseMenuManager.SelectedPauseMenu = this;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void OnSelectedNavigationButtonChange()
        {
            
        }

        

        // public void SetInputListeners()
        // {
        //     
        // }
        //
        // public void UnsetInputListeners()
        // {
        //     
        // }
        //
        //
        // public void OnButtonDown(InputActionEventData data)
        // {
        //     
        // }
        //
        // public void OnAxisChange(InputActionEventData data)
        // {
        //     
        // }
        
    }
}