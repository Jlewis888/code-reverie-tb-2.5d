using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class LoadDataConfirmationPopup : SerializedMonoBehaviour
    {
        public Button confirmationButton;
        public Button exitButton;
        public int gameSlot;

        public void Awake()
        {
            confirmationButton.onClick.AddListener(LoadGame);
            exitButton.onClick.AddListener(() =>
            {
                Debug.Log("Close this shiot");
                gameObject.SetActive(false);
            });
        }


        public void LoadGame()
        {
            
            if (DataPersistenceManager.Instance.SaveFileExist(gameSlot))
            {
                
                DataPersistenceManager.Instance.LoadGame(gameSlot);
                GameSceneManager.Instance.fromLoadedData = true;

                if (ES3.KeyExists("currentScene", $"{gameSlot}/SaveFile.es3"))
                {
                    string scene = ES3.Load<string>("currentScene", $"{gameSlot}/SaveFile.es3");
                    SceneManager.LoadScene(scene);
                    gameObject.SetActive(false);
                }
                
            }
        }
        
        
    }
}