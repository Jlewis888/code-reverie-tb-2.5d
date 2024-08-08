using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public abstract class MenuManager : SerializedMonoBehaviour
    {

        public CameraType cameraType;
        public bool openGameMenu = true;
        public bool pauseGame = true;
        
        public void OpenMenuManager(MenuManager menuManager)
        {
            
            if (menuManager != this)
            {
                gameObject.SetActive(false);
            }
            else
            {
                
                if (pauseGame)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
                
                gameObject.SetActive(true);
            }
        }


        public void SetListeners()
        {
            EventManager.Instance.generalEvents.openMenuManager += OpenMenuManager;
        }

        public void UnsetListeners()
        {
            EventManager.Instance.generalEvents.openMenuManager -= OpenMenuManager;
        }

   
        private void OnDestroy()
        {
            EventManager.Instance.generalEvents.openMenuManager -= OpenMenuManager;
        }
    }
}