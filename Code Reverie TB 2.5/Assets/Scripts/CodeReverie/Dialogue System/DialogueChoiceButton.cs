using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class DialogueChoiceButton : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public ChoiceNode choiceNode;
        private Button button;
        public TMP_Text dialogueText;
        public int choiceIndex;
        public bool continueButton;
        public bool endDialogueButton;
        public bool vendorButton;
        public DialogueSpeaker speaker;
        public GameObject selector;
        
        private void Awake()
        {
            button = GetComponent<Button>();
            //button.onClick.AddListener(SelectDialogueChoice);
            speaker = CanvasManager.Instance.dialogueManager.speaker;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CanvasManager.Instance.dialogueManager.currentChoiceIndex = choiceIndex;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
       
        }


        public void SelectDialogueChoice()
        {
            // if (endDialogueButton)
            // {
            //     
            //     EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.screenSpaceCanvasManager.hudManager);
            //     EventManager.Instance.playerEvents.OnDialogueEnd(speaker);
            //     
            //     CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController.transform);
            //     CameraManager.Instance.ToggleMainCamera();
            //     
            //     return;
            // }
            
            
            // if (CanvasManager.Instance.dialogueManager.CanContinue)
            // {
            //
            //     if (continueButton)
            //     {
            //         if (CanvasManager.Instance.dialogueManager.currentChoiceCount == 0)
            //         {
            //             CanvasManager.Instance.dialogueManager.ContinueDialogue();
            //         }
            //     }
            //     else
            //     {
            //         CanvasManager.Instance.dialogueManager.MakeChoice(choiceIndex);
            //         
            //     }
            //
            //    
            // }

            // if (vendorButton)
            // {
            //     CanvasManager.Instance.OpenVendorMenu();
            // }
            
            
        }
        
    }
}