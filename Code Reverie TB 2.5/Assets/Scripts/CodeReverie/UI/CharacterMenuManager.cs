using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CharacterMenuManager : MenuManager
    {
        public GameObject characterDetailsPage;
        public CharacterSkillsMenu characterSkillsMenu;
        public Button archetypeButton;
        public TMP_Text characterName;
        public Image characterSprite;
        private PartySlot activePartySlot;
        
        public List<StatMenuPanel> statMenuPanels;

        private void Awake()
        {
            archetypeButton.onClick.AddListener(ToggleCharacterPages);
        }

        private void OnEnable()
        {
            characterDetailsPage.SetActive(true);
            characterSkillsMenu.gameObject.SetActive(false);
            //ActivePartySlot = PlayerManager.Instance.currentParty[0];
            characterName.text = ActivePartySlot.character.info.characterName;
            characterSprite.sprite = ActivePartySlot.character.info.characterSprite;

        }

        private void OnDisable()
        {
            Clear();
        }

        public void Clear()
        {
            ActivePartySlot = null;
        }

        void ToggleCharacterPages()
        {
            if (characterDetailsPage.gameObject.activeInHierarchy)
            {
                characterDetailsPage.SetActive(false);
                characterSkillsMenu.gameObject.SetActive(true);
            }
            else
            {
                characterDetailsPage.SetActive(true);
                characterSkillsMenu.gameObject.SetActive(false);
            }
        }
        
        public PartySlot ActivePartySlot
        {
            get { return activePartySlot; }

            set
            {
                if (value != activePartySlot)
                {
                    activePartySlot = value;
                    characterSkillsMenu.ActivePartySlot = value;
                    
                    //PlayerController.Instance.CharacterMenuSwap(activePartySlot);
                    EventManager.Instance.playerEvents.OnCharacterMenuSwap();
                    SetStatMenuPanels();
                    
                }
            }
        }
        

        public void SetStatMenuPanels()
        {
            foreach (StatMenuPanel statMenuPanel in statMenuPanels)
            {
                statMenuPanel.statValueText.text = ActivePartySlot.character.characterStats.GetStat(statMenuPanel.statAttribute).ToString();
            }
        }

    }
}