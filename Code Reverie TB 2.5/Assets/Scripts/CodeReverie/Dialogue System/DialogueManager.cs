using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityString;
using Ink.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class DialogueManager : MenuManager
    {
        
        public Story currentStory;
        public GameObject dialogueTextPanel;
        public DialogueChoiceButton dialogueChoiceButtonPF;
        public GameObject dialogueChoiceHolder;
        public DialogueChoiceButton activeDialogueChoiceButton;
        
        public List<DialogueChoiceButton> dialogueChoiceButtons;
        public bool isDialogueActive;
        public TMP_Text dialogueText;
        public TMP_Text dialogueName;
        public Image speakerPortrait;
        public float typingSpeed = 0.04f;
        public DialogueSpeaker speaker;
        public DialogueSpeaker playerSpeaker;

        public GameObject continueIcon;
        private Coroutine activeCoroutine;

        private bool canContinue = false;
        public bool canCompleteText = true;
        public bool completeText = false;

        public Choice choice;

        public int currentChoiceIndex = 0;
        public int currentChoiceCount = 0;

        public int navigationButtonsIndex;
        public float navigationDelay = 0.35f;
        public float navigationDelayTimer;

        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onDialogueStart += EnterDialogueMode;
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 1);
        }

        private void OnDisable()
        {
            EventManager.Instance.playerEvents.onDialogueStart -= EnterDialogueMode;
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 0);
        }
        
        private void Start()
        {
            // isDialogueActive = false;
            // ClearDialogueOptions();
            //dialogueTextPanel.SetActive(false);
        }

        private void Update()
        {
            // if (isDialogueActive)
            // {
                
                if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                {

                    if (canCompleteText)
                    {
                        completeText = true;
                    }
                    
                    if (activeDialogueChoiceButton != null)
                    {
                        activeDialogueChoiceButton.SelectDialogueChoice();
                    }
                }

                if (navigationDelayTimer <= 0 && dialogueChoiceButtons.Count > 0 && dialogueChoiceHolder.activeInHierarchy) 
                {
                    if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical Axis") < 0)
                    {
                        navigationDelayTimer = navigationDelay;
                        if (navigationButtonsIndex + 1 > dialogueChoiceButtons.Count - 1)
                        {
                            navigationButtonsIndex = 0;
                        }
                        else
                        {
                            navigationButtonsIndex++;
                        }
                    
                        NavigateChoices();
                   
                    }
                
                    if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical Axis") > 0)
                    {
                        navigationDelayTimer = navigationDelay;
                        if (navigationButtonsIndex == 0)
                        {
                            navigationButtonsIndex = dialogueChoiceButtons.Count - 1;
                        }
                        else
                        {
                            navigationButtonsIndex--;
                        }
                
                        NavigateChoices();
                    }
                } 
                else if (navigationDelayTimer > 0)
                { 
                    navigationDelayTimer -= Time.deltaTime;
                }
                
                
                
                if (GameManager.Instance.playerInput.GetNegativeButtonDown("Navigate Menu Vertical Button"))
                {
                    navigationDelayTimer = navigationDelay;
                    if (navigationButtonsIndex + 1 > dialogueChoiceButtons.Count - 1)
                    {
                        navigationButtonsIndex = 0;
                    }
                    else
                    {
                        navigationButtonsIndex++;
                    }
                    
                    NavigateChoices();
                }
                else if (GameManager.Instance.playerInput.GetButtonDown("Navigate Menu Vertical Button"))
                {
                    
                    
                    
                    navigationDelayTimer = navigationDelay;
                    if (navigationButtonsIndex == 0)
                    {
                        navigationButtonsIndex = dialogueChoiceButtons.Count - 1;
                    }
                    else
                    {
                        navigationButtonsIndex--;
                    }
                
                    NavigateChoices();
                }
            //}
        }

        public void SetFirstItem()
        {
            navigationButtonsIndex = 0;
        }
        
        public void NavigateChoices()
        {
        
            activeDialogueChoiceButton = dialogueChoiceButtons[navigationButtonsIndex];
            activeDialogueChoiceButton.selector.SetActive(true);
            
            foreach (DialogueChoiceButton dialogueChoiceButton in dialogueChoiceButtons)
            {
                if (dialogueChoiceButton != activeDialogueChoiceButton)
                {
                    dialogueChoiceButton.selector.SetActive(false);
                }
            }
        }
        
        public void EnterDialogueMode(TextAsset inkJSON, CharacterDataContainer dialogueSpeaker, String storyPath = "")
        {
            
            //speaker = dialogueSpeaker;
            speakerPortrait.sprite = dialogueSpeaker.characterSprite;
            dialogueName.text = dialogueSpeaker.characterName;
            
            //CameraManager.Instance.UpdateCamera(speaker.transform);
            CameraManager.Instance.ToggleDialogueCamera();
            
            
            ClearDialogueOptions();
            
            
            currentStory = new Story(inkJSON.text);

            if (!string.IsNullOrEmpty(storyPath))
            {
                currentStory.ChoosePathString(storyPath);
            }

            
            
            //currentStory.ChoosePathString("chosen");
            
            isDialogueActive = true;
            dialogueTextPanel.SetActive(true);
            
            ContinueDialogue();
        }

        public void EnterDialogueMode(TextAsset inkJSON, DialogueSpeaker dialogueSpeaker, String storyPath = "")
        {
            
            speaker = dialogueSpeaker;
            speakerPortrait.sprite = dialogueSpeaker.GetComponent<CharacterUnitController>().character.GetCharacterPortrait();
            dialogueName.text = dialogueSpeaker.GetComponent<CharacterUnitController>().character.info.characterName;
            
            //CameraManager.Instance.UpdateCamera(speaker.transform);
            CameraManager.Instance.ToggleDialogueCamera();
            
            
            ClearDialogueOptions();
            
            
            currentStory = new Story(inkJSON.text);

            if (!string.IsNullOrEmpty(storyPath))
            {
                currentStory.ChoosePathString(storyPath);
            }

            
            
            //currentStory.ChoosePathString("chosen");
            
            isDialogueActive = true;
            dialogueTextPanel.SetActive(true);
            
            ContinueDialogue();
        }

        public void ExitDialogue()
        {
            isDialogueActive = false;
            
            ClearDialogueOptions();
            
            DialogueChoiceButton dialogueChoiceButton =
                Instantiate(dialogueChoiceButtonPF, dialogueChoiceHolder.transform);
            dialogueChoiceButton.endDialogueButton = true;
            dialogueChoiceButton.dialogueText.text = "Leave";
            dialogueChoiceButtons.Add(dialogueChoiceButton);
            SetFirstItem();
            NavigateChoices();
            
        }
        
        public void SelectDialogueChoice()
        {
            if (canContinue)
            {
                if (currentChoiceCount > 0 && dialogueChoiceHolder.activeInHierarchy)
                {
                    MakeChoice(currentChoiceIndex);
                }
                        
            }
            
        }

        public bool CanContinue
        {
            get { return canContinue; }
        }

        public void ContinueDialogue()
        {
            if (currentStory.canContinue)
            {
                if (activeCoroutine != null)
                {
                    StopCoroutine(activeCoroutine);
                }
                
                activeCoroutine = StartCoroutine(DisplayDialogue(currentStory.Continue()));
                HandleTags();
            }
            else
            {
                ExitDialogue();
            }
        }

        public void DisplayChoices()
        {
            List<Choice> currentChoices = currentStory.currentChoices;
            currentChoiceCount = currentChoices.Count;
            if (currentChoices.Count > 0)
            {
                int index = 0;
                foreach (Choice choice in currentChoices)
                {
                    DialogueChoiceButton dialogueChoiceButton =
                        Instantiate(dialogueChoiceButtonPF, dialogueChoiceHolder.transform);
                    dialogueChoiceButton.dialogueText.text = choice.text;
                    dialogueChoiceButton.choiceIndex = index;
                    dialogueChoiceButtons.Add(dialogueChoiceButton);
                    
                    index++;
                }
                SetFirstItem();
                NavigateChoices();
            }
            else
            {
                if (canContinue && isDialogueActive)
                {
                    DialogueChoiceButton dialogueChoiceButton =
                        Instantiate(dialogueChoiceButtonPF, dialogueChoiceHolder.transform);
                    dialogueChoiceButton.continueButton = true;
                    dialogueChoiceButton.dialogueText.text = "Continue";
                    dialogueChoiceButtons.Add(dialogueChoiceButton);
                    SetFirstItem();
                    NavigateChoices();
                }
            }
        }

        public void MakeChoice(int index)
        {
            currentStory.ChooseChoiceIndex(index);
            ClearDialogueOptions();
            ContinueDialogue();
            
        }

        public void HandleTags()
        {
            foreach (var tag in currentStory.currentTags)
            {
                string[] splitTag = tag.Split(':');
                
                if (splitTag.Length != 2)
                {
                    Debug.Log("Tag not working");
                }

                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();


                DialogueTags dialogueTag = Enum.Parse<DialogueTags>(tagKey);

                switch (dialogueTag)
                {
                    case DialogueTags.Speaker:
                        SetCurrentSpeaker(tagValue);
                        break;
                    case DialogueTags.ExitDialogue:
                        ExitDialogue();
                        break;
                    case DialogueTags.AcceptQuest:
                        QuestManager.Instance.StartQuest(tagValue);
                        break;
                    case DialogueTags.CompleteQuestObjective:
                        EventManager.Instance.playerEvents.OnDialogueChoiceSelection(tagValue);
                        break;
                    case DialogueTags.CheckQuestDialogue:
                        
                        string[] splitValueTag = splitTag[1].Split(';');
                        
                        string questIdTag = splitValueTag[0].Trim();
                        string questStatusTag = splitValueTag[1].Trim();
                        string choiceVariable = splitValueTag[2].Trim();


                        Quest quest = QuestManager.Instance.GetQuestById(questIdTag);

                        if (quest != null)
                        {
                            QuestStatus questStatus = Enum.Parse<QuestStatus>(questStatusTag);

                            if (questStatus == quest.Status)
                            {
                                currentStory.variablesState[choiceVariable] = true;
                            }
                            
                        }
                        
                        break;
                }
                
            }
        }
        

        public void SetCurrentSpeaker(string speaker)
        {
            dialogueName.text = speaker;
        }
        
        IEnumerator DisplayDialogue(string line)
        {
            //yield return new WaitForSeconds(0.1f);
            dialogueText.text = "";
            //continueIcon.SetActive(false);
            ClearDialogueOptions();
            canContinue = false;

            canCompleteText = false;
            completeText = false;
            foreach (char letter in line)
            {
                if (completeText)
                {
                    dialogueText.text = line;
                    break;
                }
                
                dialogueText.text += letter;
                //yield return new WaitForSeconds(typingSpeed);
                yield return new WaitForSecondsRealtime(typingSpeed);
                canCompleteText = true;
            }
            
            //continueIcon.SetActive(true);
            dialogueChoiceHolder.SetActive(true);
            
            canContinue = true;
            DisplayChoices();
        }

        private void ClearDialogueOptions()
        {
            foreach (Transform child in dialogueChoiceHolder.transform)
            {
                Destroy(child.gameObject);
            }

            dialogueChoiceButtons = new List<DialogueChoiceButton>();
            activeDialogueChoiceButton = null;
            dialogueChoiceHolder.SetActive(false);
            
            
        }

        // public void NavigateChoices()
        // {
        //
        //     activeDialogueChoiceButton = dialogueChoiceButtons[navigationButtonsIndex];
        //     activeDialogueChoiceButton.selector.SetActive(true);
        //     
        //     foreach (DialogueChoiceButton dialogueChoiceButton in dialogueChoiceButtons)
        //     {
        //         if (dialogueChoiceButton != activeDialogueChoiceButton)
        //         {
        //             dialogueChoiceButton.selector.SetActive(false);
        //         }
        //     }
        // }

    }
}