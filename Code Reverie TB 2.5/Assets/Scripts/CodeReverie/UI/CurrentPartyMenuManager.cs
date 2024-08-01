using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CurrentPartyMenuManager : SerializedMonoBehaviour
    {
        public PauseMenuSubNavigationState pauseMenuSubNavigationState;
        public PartyMenuSlot partyMenuSlotPF;
        public GameObject partySlotMenuHolder;
        
        public List<PauseMenuNavigationButton> pauseMenuNavigationButtons;
        public PauseMenuNavigationButton selectedNavigationButton;
        public int navigationButtonsIndex;
        public float navigationDelay = 0.35f;
        public float navigationDelayTimer;
        public Item selectedItem;

        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange += OnPauseMenuSubNavigationStateChange;
            EventManager.Instance.inventoryEvents.onItemMenuSelect += OnItemMenuSelect;
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange -= OnPauseMenuSubNavigationStateChange;
            EventManager.Instance.inventoryEvents.onItemMenuSelect -= OnItemMenuSelect;
        }


        private void Update()
        {
            if (pauseMenuSubNavigationState == PauseMenuSubNavigationState.CurrentPartyMenuManager)
            {
                if (GameManager.Instance.playerInput.GetButtonDown("Cancel"))
                {
                    EventManager.Instance.generalEvents.onPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState.None);
                    selectedItem = null;
                }

                if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                {
                    Confirm();
                }

                if (navigationDelayTimer <= 0)
                {
                    if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical Axis") < 0)
                    {
                        navigationDelayTimer = navigationDelay;
                        if (navigationButtonsIndex + 1 > pauseMenuNavigationButtons.Count - 1)
                        {
                            navigationButtonsIndex = 0;
                        }
                        else
                        {
                            navigationButtonsIndex++;
                        }

                        SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
                    }

                    if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical Axis") > 0)
                    {
                        navigationDelayTimer = navigationDelay;
                        if (navigationButtonsIndex == 0)
                        {
                            navigationButtonsIndex = pauseMenuNavigationButtons.Count - 1;
                        }
                        else
                        {
                            navigationButtonsIndex--;
                        }

                        SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
                    }
                }
                else
                {
                    navigationDelayTimer -= Time.unscaledDeltaTime;
                }


                if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Menu Vertical Button"))
                {
                    if (navigationButtonsIndex + 1 > pauseMenuNavigationButtons.Count - 1)
                    {
                        navigationButtonsIndex = 0;
                    }
                    else
                    {
                        navigationButtonsIndex++;
                    }

                    SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
                }
                else if (GameManager.Instance.playerInput.GetButtonDown("Navigate Menu Vertical Button"))
                {
                    navigationDelayTimer = navigationDelay;
                    if (navigationButtonsIndex == 0)
                    {
                        navigationButtonsIndex = pauseMenuNavigationButtons.Count - 1;
                    }
                    else
                    {
                        navigationButtonsIndex--;
                    }

                    SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
                }
            }
        }

        private void Confirm()
        {
            Debug.Log("Use Item on Character");
            selectedItem.UseMenuItemOnCharacter(SelectedNavigationButton.GetComponent<PartyMenuSlot>().character.characterController.GetComponent<CharacterBattleManager>());
        }


        public void SetParty()
        {
            Clear();

            foreach (Character character in PlayerManager.Instance.currentParty)
            {
                PartyMenuSlot partyMenuSlot =
                    Instantiate(partyMenuSlotPF, transform);

                partyMenuSlot.character = character;
                partyMenuSlot.characterPortrait.sprite = character.GetCharacterPortrait();
                partyMenuSlot.selector.SetActive(false);
                partyMenuSlot.nameText.text = character.info.characterName;
                pauseMenuNavigationButtons.Add(partyMenuSlot);
            }
        }

        public void Clear()
        {
            pauseMenuNavigationButtons = new List<PauseMenuNavigationButton>();
            
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        public PauseMenuNavigationButton SelectedNavigationButton
        {
            get => selectedNavigationButton;

            set
            {
                if (selectedNavigationButton == value)
                {
                    return;
                }

                selectedNavigationButton = value;

                
                
                
                PauseMenuNavigationUpdate();
                
                // if (selectedNavigationButton == null)
                // {
                //     
                // }
                //selectedNavigationButton.selector.SetActive(true);

                //OnSelectedNavigationButtonChange();

            }
        }
        
        
        
        public void PauseMenuNavigationUpdate()
        {
            foreach (PauseMenuNavigationButton pauseMenuNavigationButton in pauseMenuNavigationButtons)
            {
           
                if (SelectedNavigationButton != pauseMenuNavigationButton)
                {
                    pauseMenuNavigationButton.selector.SetActive(false);
                }
                else
                {
                    pauseMenuNavigationButton.selector.SetActive(true);
                }
            }

            if (selectedNavigationButton != null)
            {
                selectedNavigationButton.selector.SetActive(true);
            }
            
        }
        
        public void OnPauseMenuSubNavigationStateChange(PauseMenuSubNavigationState pauseMenuNavigationState)
        {
            pauseMenuSubNavigationState = pauseMenuNavigationState;

            if (pauseMenuSubNavigationState == PauseMenuSubNavigationState.CurrentPartyMenuManager)
            {
                navigationButtonsIndex = 0;
                SelectedNavigationButton = pauseMenuNavigationButtons[navigationButtonsIndex];
            }
            else
            {
                SelectedNavigationButton = null;
            }
            
        }

        public void OnItemMenuSelect(Item item)
        {
            selectedItem = item;
        }
        
    }
}