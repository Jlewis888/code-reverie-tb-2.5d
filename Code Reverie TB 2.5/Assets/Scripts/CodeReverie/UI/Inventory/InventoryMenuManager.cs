using System;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class InventoryMenuManager : MenuManager
    {
        
        private PartySlot activePartySlot;
        public SelectPartySlotButton selectPartySlotButtonPF;
        public GameObject selectPartySlotButtonHolder;
        public TMP_Text atkStatText;
        
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.ToggleInventory(true);
            EventManager.Instance.playerEvents.OnPlayerLock(true);
            
            
            foreach (Character character in PlayerManager.Instance.availableCharacters)
            {
                SelectPartySlotButton selectPartySlotButton =
                    Instantiate(selectPartySlotButtonPF, selectPartySlotButtonHolder.transform);


                //selectPartySlotButton.partySlot = character;
                selectPartySlotButton.Init();
                selectPartySlotButton.gameObject.SetActive(true);
            }
            
            //ActivePartySlot = PlayerManager.Instance.currentParty[0];
        }
        
        
        private void OnDisable()
        {
            
            //PlayerController.Instance.CharacterMenuSwap(PlayerManager.Instance.currentParty[0]);
            
            foreach (Transform child in selectPartySlotButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
            
        }

        private void Update()
        {
        }


        public PartySlot ActivePartySlot
        {
            get { return activePartySlot; }

            set
            {
                if (value != activePartySlot)
                {
                    activePartySlot = value;
                    
                    //PlayerController.Instance.CharacterMenuSwap(activePartySlot);
                    EventManager.Instance.playerEvents.OnCharacterMenuSwap();
                }
            }
        }
        
        
    }
}