using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class PartyHudPanelManager : SerializedMonoBehaviour
    {
        public PartyHudPanel partyHudPanelPF;

        
        public void SetPartyUnitPanels()
        {
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
            }
            
            
            foreach (Character character in PlayerManager.Instance.currentParty)
            {
                PartyHudPanel partyHudPanel = Instantiate(partyHudPanelPF, transform);

                partyHudPanel.character = character;
                partyHudPanel.InitCharacterHudPanel();
                partyHudPanel.gameObject.SetActive(true);
            }
        }

        public void SetListeners()
        {
            EventManager.Instance.playerEvents.onPartyUpdate += SetPartyUnitPanels;
        }
        
        
    }
}