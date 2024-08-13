using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class LoadSlotButton : DataSlotButton
    {
        
        // private void Start()
        // {
        //     SetHasDataPanel();
        // }

        private void OnEnable()
        {
            SetHasDataPanel();
        }

        public void LoadGame()
        {
            
            
            if (DataPersistenceManager.Instance.SaveFileExist(gameSlot))
            {
                
                // DataPersistenceManager.Instance.LoadGame(gameSlot);
                // GameSceneManager.Instance.fromLoadedData = true;
                //
                // if (ES3.KeyExists("currentScene", $"{gameSlot}/SaveFile.es3"))
                // {
                //     string scene = ES3.Load<string>("currentScene", $"{gameSlot}/SaveFile.es3");
                //     SceneManager.LoadScene(scene);
                // }

                CanvasManager.Instance.loadDataConfirmationPopup.gameSlot = gameSlot;
                CanvasManager.Instance.loadDataConfirmationPopup.gameObject.SetActive(true);
                


            }
        }
        
    }
}