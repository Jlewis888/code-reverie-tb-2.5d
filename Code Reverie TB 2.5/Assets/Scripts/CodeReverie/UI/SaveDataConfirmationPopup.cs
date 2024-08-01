using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SaveDataConfirmationPopup : SerializedMonoBehaviour
    {
        
        public Button confirmationButton;
        public Button exitButton;
        public int gameSlot;
        public TMP_Text saveMessageText;
        
        
        public void Awake()
        {
            confirmationButton.onClick.AddListener(SaveGame);
            exitButton.onClick.AddListener(() =>
            {
                Debug.Log("Close the popup");
                gameObject.SetActive(false);
            });
        }

        private void OnEnable()
        {
            if (DataPersistenceManager.Instance.SaveFileExist(gameSlot))
            {
                saveMessageText.text = "Overwrite existing game?";
            }
            else
            {
                saveMessageText.text = "Save your game in this slot?";
            }
        }


        public void SaveGame()
        {
            DataPersistenceManager.Instance.SaveGame(gameSlot);
            gameObject.SetActive(false);
        }
        
        
    }
}