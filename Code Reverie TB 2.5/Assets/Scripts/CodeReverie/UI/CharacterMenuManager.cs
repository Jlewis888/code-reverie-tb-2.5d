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
        
        public List<ArchetypeTree> archetypeTrees;
        public GameObject treeHolder;
        public ArchetypeTree activeArchetypeTree;
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
            ActiveArchetypeTree = null;

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
        
        public ArchetypeTree ActiveArchetypeTree
        {
            get { return activeArchetypeTree; }
            set
            {
                
                if (value != activeArchetypeTree)
                {
                   
                    activeArchetypeTree = value;
                    
                }
                
                
                SetActiveTree();
            }
        }
        
        public void SetActiveTree()
        {
            foreach (ArchetypeTree archetypeTree in archetypeTrees)
            {

                if (ActiveArchetypeTree == null)
                {
                    if (archetypeTree.gameObject.activeInHierarchy)
                    {
                        characterSkillsMenu.gameObject.SetActive(true);
                    }
                    
                    archetypeTree.gameObject.SetActive(false);
                }
                else
                {
                    if (archetypeTree == ActiveArchetypeTree)
                    {
                        archetypeTree.gameObject.SetActive(true);
                    }
                    else
                    {
                        archetypeTree.gameObject.SetActive(false);
                    }
                    
                    characterSkillsMenu.gameObject.SetActive(false);
                }
            }
        }

        public void SetStatMenuPanels()
        {
            foreach (StatMenuPanel statMenuPanel in statMenuPanels)
            {
                statMenuPanel.statValueText.text = ActivePartySlot.character.characterController
                    .GetComponent<CharacterStatsManager>().GetStat(statMenuPanel.statAttribute).ToString();
            }
        }

    }
}