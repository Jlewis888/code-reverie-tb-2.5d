using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public abstract class DataSlotButton : PauseMenuNavigationButton
    {
        public int gameSlot;
        public List<CharacterPortrait> characterPortraits;
        public CharacterPortrait partySlotPortrait1;
        public CharacterPortrait partySlotPortrait2;
        public CharacterPortrait partySlotPortrait3;
        public CharacterPortrait partySlotPortrait4;
        public TMP_Text timeText;
        public TMP_Text dateText;
        public TMP_Text location1Text;
        public TMP_Text location2Text;
        public TMP_Text levelText;
        public TMP_Text playTimeText;
        public TMP_Text autosaveText;
        public TMP_Text saveSlotNumberText;
        public GameObject hasDataPanel;
        public GameObject noDataPanel;


        public void SetHasDataPanel()
        {
            string path = $"{gameSlot}/SaveFile.es3";

            characterPortraits = new List<CharacterPortrait>();
            
            characterPortraits.Add(partySlotPortrait1);
            characterPortraits.Add(partySlotPortrait2);
            characterPortraits.Add(partySlotPortrait3);
            characterPortraits.Add(partySlotPortrait4);
            
            characterPortraits[0].gameObject.SetActive(false);
            characterPortraits[1].gameObject.SetActive(false);
            characterPortraits[2].gameObject.SetActive(false);
            characterPortraits[3].gameObject.SetActive(false);

            if (gameSlot != 0)
            {
                saveSlotNumberText.text = gameSlot.ToString();
            }
            else
            {
                saveSlotNumberText.gameObject.SetActive(false);
            }
            
            if (ES3.FileExists(path))
            {
                
                hasDataPanel.SetActive(true);
                noDataPanel.SetActive(false);
                
                if (ES3.KeyExists("currentLevel", path))
                {
                    levelText.text = $"Lvl. {ES3.Load<int>("currentLevel", path).ToString()}";
                }
                else
                {
                    levelText.text = "Lvl. 1";
                }
                if (ES3.KeyExists("activeParty", path))
                {

                    List<Character> currentParty = ES3.Load<List<Character>>("currentParty", path);

                    int count = 0;
                    
                    foreach (var character in currentParty)
                    {
                        characterPortraits[count].characterPortraitImage.sprite = character.info.characterPortrait;
                        characterPortraits[count].gameObject.SetActive(true);
                        count++;
                    }
                    
                    
                }

            }
            else
            {
                hasDataPanel.SetActive(false);
                noDataPanel.SetActive(true);
            }
        }
        
        
        
    }
}