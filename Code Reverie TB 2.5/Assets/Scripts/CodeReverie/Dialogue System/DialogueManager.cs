using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityString;
using Ink.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using Unity.Behavior;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class DialogueManager : MenuManager
    {
        [SerializeField] 
        private DialogueGraphAsset dialogueGraphAsset;
        private DialogueGraphAsset graphInstance;
        
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
        public bool continueDialogue = false;
        public bool canCompleteText = true;
        public bool completeText = false;
        public bool canEndDialogue = false;


        public int currentChoiceIndex = 0;
        public int currentChoiceCount = 0;

        public int navigationButtonsIndex;
        public float navigationDelay = 0.35f;
        public float navigationDelayTimer;
        
        [SerializeField]
        private DialogueGraphNode currentNode;
        private DialogueGraphNode nextNode;

        public ChoiceNode currentChoiceNode;

        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onDialogueStart += EnterDialogueMode;
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 1);
            canEndDialogue = false;
        }

        private void OnDisable()
        {
            EventManager.Instance.playerEvents.onDialogueStart -= EnterDialogueMode;
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 0);
        }
        
        private void Update()
        {
            // if (isDialogueActive)
            // {
                
                // if (GameManager.Instance.playerInput.GetButtonDown("Confirm"))
                // {
                //    
                //     return;
                //     
                //     if (canCompleteText)
                //     {
                //         completeText = true;
                //     }
                //
                //     if (canEndDialogue && currentNode is EndNode)
                //     {
                //         currentNode.Execute(graphInstance);
                //     }
                //     
                //     if (activeDialogueChoiceButton != null && dialogueChoiceButtons.Count > 0 && dialogueChoiceHolder.activeInHierarchy)
                //     {
                //        // activeDialogueChoiceButton.SelectDialogueChoice();
                //        currentNode = activeDialogueChoiceButton.choiceNode;
                //        activeDialogueChoiceButton.choiceNode.Execute(graphInstance);
                //        activeDialogueChoiceButton = null;
                //        dialogueChoiceHolder.SetActive(false);
                //     }
                // }

                if (navigationDelayTimer <= 0 && dialogueChoiceButtons.Count > 0 && dialogueChoiceHolder.activeInHierarchy) 
                {
                    if (GameManager.Instance.playerInput.GetAxis("Navigate Menu Vertical Axis") < 0)
                    {
                        navigationDelayTimer = navigationDelay;
                        if (navigationButtonsIndex > dialogueChoiceButtons.Count - 1)
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
                    navigationDelayTimer -= Time.unscaledDeltaTime;;
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

        public void CompleteDialogueText()
        {
            if (canCompleteText)
            {
                completeText = true;
            }
        }

        public void Continue()
        {
            if (canContinue)
            {
                continueDialogue = true;
            }
        }

        public void OnConfirmAction()
        {
            

            // if (canEndDialogue && currentNode is EndNode)
            // {
            //     currentNode.Execute(graphInstance);
            // }
            //         
            // if (activeDialogueChoiceButton != null && dialogueChoiceButtons.Count > 0 && dialogueChoiceHolder.activeInHierarchy)
            // {
            //     // activeDialogueChoiceButton.SelectDialogueChoice();
            //     currentNode = activeDialogueChoiceButton.choiceNode;
            //     activeDialogueChoiceButton.choiceNode.Execute(graphInstance);
            //     activeDialogueChoiceButton = null;
            //     dialogueChoiceHolder.SetActive(false);
            // }
        }

        public (bool, int) ConfirmChoice()
        {
            bool choiceSelected = false;
            int choiceIndex = -1;
            
            
            if (activeDialogueChoiceButton != null && dialogueChoiceButtons.Count > 0 && dialogueChoiceHolder.activeInHierarchy)
            {
                // activeDialogueChoiceButton.SelectDialogueChoice();
                // currentNode = activeDialogueChoiceButton.choiceNode;
                // activeDialogueChoiceButton.choiceNode.Execute(graphInstance);
                
                choiceSelected = true;
                choiceIndex = activeDialogueChoiceButton.choiceIndex;
                
                
            }
            return (choiceSelected, choiceIndex);
        }

        public void ChoiceSelected()
        {
            activeDialogueChoiceButton = null;
            dialogueChoiceHolder.SetActive(false);
        }
        
        public void EnterDialogueMode(BehaviorGraph behaviorGraph)
        {

            //Unsupported.SmartReset(GetComponent<BehaviorGraphAgent>());
            
            DestroyImmediate(gameObject.GetComponent<BehaviorGraphAgent>());
            gameObject.AddComponent<BehaviorGraphAgent>();
            //
            GetComponent<BehaviorGraphAgent>().Graph = behaviorGraph;
            GetComponent<BehaviorGraphAgent>().Init();

            // if (GetComponent<BehaviorGraphAgent>().Graph != behaviorGraph || GetComponent<BehaviorGraphAgent>().Graph == null)
            // {
            //     GetComponent<BehaviorGraphAgent>().Graph = behaviorGraph;
            //     GetComponent<BehaviorGraphAgent>().Init();
            // }
            
            
           
            
            isDialogueActive = true;
            dialogueTextPanel.SetActive(true);
        }
        
        
        public void EnterDialogueMode(DialogueGraphAsset dialogueGraphAsset)
        {
            graphInstance = Instantiate(dialogueGraphAsset);
            ExecuteAsset();
            //currentNode.Execute(graphInstance);
            
            // DialogueGraphNode startNode = graphInstance.GetStartNode();
            // currentNode = startNode;

            //var property = currentNode.GetType().GetField("dialogueText");
            // Debug.Log(currentNode.GetType());
            // Debug.Log(property);
            // Debug.Log(property.GetType());
            //
            // if (currentNode is StartNode || currentNode is DialogueNode)
            // {
            //     Debug.Log("this is true");
            // }

            // if (property.GetValue(currentNode).GetType() == typeof(string))
            // {
            //     
            //     //dialogueText.text = (string)property.GetValue(currentNode);
            //     
            //     if (activeCoroutine != null)
            //     {
            //         StopCoroutine(activeCoroutine);
            //     }
            //     
            //     activeCoroutine = StartCoroutine(DisplayDialogue((string)property.GetValue(currentNode)));
            //     
            // }
            
            //Debug.Log(currentNode.GetType().GetProperty("dialogueText").GetValue(currentNode, null));
            // Debug.Log(property.GetValue(currentNode));
            // Debug.Log(property.GetValue(currentNode).GetType());
            
            isDialogueActive = true;
            dialogueTextPanel.SetActive(true);
            
            
            
        }
        
        private void ExecuteAsset()
        {
            graphInstance.Init(gameObject);

            DialogueGraphNode startNode = graphInstance.GetStartNode();
            currentNode = startNode;
            //ProcessCurrentNode();
        }
        
        private void ProcessCurrentNode()
        {
            if (currentNode == null)
            {
                return;
            }
            
            List<string> nextNodeIdList = currentNode.OnProcess(graphInstance);

            if (nextNodeIdList.Count > 0)
            {
                if (nextNodeIdList.Count == 1)
                {
                    nextNode = graphInstance.GetNode(nextNodeIdList[0]);
                }
            }
        }

        public void Dialogue(CharacterDataContainer speaker, string line)
        {
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }

            if (speaker != null)
            {
                speakerPortrait.sprite = speaker.characterPortrait;
                dialogueName.text = speaker.characterName;
            }
                
            activeCoroutine = StartCoroutine(DisplayDialogue(line));

        }
        
        IEnumerator DisplayDialogue(string line)
        {
            //yield return new WaitForSeconds(0.1f);
            dialogueText.text = "";
            //continueIcon.SetActive(false);
            ClearDialogueOptions();
            canContinue = false;
            continueDialogue = false;
        
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
            
            //SetNextNode();
            
            
            
            
            
            canContinue = true;
            //DisplayChoices();
        }

        public void SetNextNode()
        {
            List<DialogueGraphNode> graphNodes = currentNode.GetConnectedOutputNodes(graphInstance);

            if (graphNodes.Count > 1)
            {
                // if (IsNextNodeBranchNode())
                // {
                //     dialogueChoiceHolder.SetActive(true);
                // }
            }
            else if(graphNodes.Count == 1)
            {
                currentNode = graphNodes[0];

                if (currentNode is EndNode)
                {
                    canEndDialogue = true;
                }
                else
                {
                    currentNode.Execute(graphInstance);
                }
            }
        }

        public bool IsNextNodeBranchNode()
        {
            List<DialogueGraphNode> graphNodes = currentNode.GetConnectedOutputNodes(graphInstance);
            
            Debug.Log(graphNodes.Count);
            
            foreach (DialogueGraphNode node in graphNodes)
            {
                
                if (node is BranchNode)
                {
                    currentNode = node;
                    currentNode.Execute(graphInstance);
                    return true;
                }
            }
            
            

            return false;
        }
        

        // private void DisplayChoices()
        // {
        //     if (currentNode.GetType() != typeof(BranchNode))
        //     {
        //         return;
        //     }
        //     
        //     
        //     BranchNode branchNode = currentNode as BranchNode;
        //     branchNode.Execute(graphInstance);
        //     
        //     List<ChoiceNode> currentChoices = branchNode.choiceNodes;
        //
        //     if (currentChoices.Count > 0)
        //     {
        //         int index = 0;
        //         foreach (ChoiceNode choiceNode in currentChoices)
        //         {
        //             DialogueChoiceButton dialogueChoiceButton = Instantiate(dialogueChoiceButtonPF, dialogueChoiceHolder.transform);
        //             dialogueChoiceButton.dialogueText.text = choiceNode.text;
        //             dialogueChoiceButton.choiceIndex = index;
        //             dialogueChoiceButtons.Add(dialogueChoiceButton);
        //             
        //             index++;
        //         }
        //         
        //         SetFirstItem();
        //         NavigateChoices();
        //     }
        //     
        // }

        public void DisplayChoices(BranchNode branchNode)
        {
            List<ChoiceNode> currentChoices = branchNode.choiceNodes;

            if (currentChoices.Count > 0)
            {
                int index = 0;
                foreach (ChoiceNode choiceNode in currentChoices)
                {
                    DialogueChoiceButton dialogueChoiceButton = Instantiate(dialogueChoiceButtonPF, dialogueChoiceHolder.transform);
                    dialogueChoiceButton.dialogueText.text = choiceNode.text;
                    dialogueChoiceButton.choiceIndex = index;
                    dialogueChoiceButton.choiceNode = choiceNode;
                    dialogueChoiceButtons.Add(dialogueChoiceButton);
                    
                    index++;
                }
                DisplayChoices();
            }
        }

        public void AddChoice(string choice)
        {
            DialogueChoiceButton dialogueChoiceButton = Instantiate(dialogueChoiceButtonPF, dialogueChoiceHolder.transform);
            dialogueChoiceButton.dialogueText.text = choice;
            dialogueChoiceButton.choiceIndex = dialogueChoiceButtons.Count;
            dialogueChoiceButtons.Add(dialogueChoiceButton);
        }

        public void DisplayChoices()
        {
            dialogueChoiceHolder.SetActive(true);
            SetFirstItem();
            NavigateChoices();
        }
        
        public void DisplayChoices(List<string> choices)
        {
           

            if (choices.Count > 0)
            {
                int index = 0;
                foreach (string choice in choices)
                {
                    DialogueChoiceButton dialogueChoiceButton = Instantiate(dialogueChoiceButtonPF, dialogueChoiceHolder.transform);
                    dialogueChoiceButton.dialogueText.text = choice;
                    dialogueChoiceButton.choiceIndex = index;
                    dialogueChoiceButtons.Add(dialogueChoiceButton);
                    
                    index++;
                }
                DisplayChoices();
            }
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
    }
}