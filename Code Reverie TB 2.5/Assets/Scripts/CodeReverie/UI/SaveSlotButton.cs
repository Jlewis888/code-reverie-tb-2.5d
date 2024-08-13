using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class SaveSlotButton : DataSlotButton
    {
        
        // private void Start()
        // {
        //     SetHasDataPanel();
        // }

        private void OnEnable()
        {
            SetHasDataPanel();
        }
        
        


        public void SaveGame()
        {
            CanvasManager.Instance.saveDataConfirmationPopup.gameSlot = gameSlot;
            CanvasManager.Instance.saveDataConfirmationPopup.gameObject.SetActive(true);
            
        }
        
    }
}