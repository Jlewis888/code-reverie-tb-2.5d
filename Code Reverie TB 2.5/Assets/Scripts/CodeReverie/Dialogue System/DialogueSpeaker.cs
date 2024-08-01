using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class DialogueSpeaker : SerializedMonoBehaviour
    {
        public DialogueBubble dialogueBubble;
        public bool canSpeak;
        public bool hasDialogue;
        public string characterName;
        public float typingSpeed = 0.04f;

        public TextAsset dialogueTextAsset;
        public Action dialogueComplete;

        private void Awake()
        {
            //dialogueBubble.nameText.text = characterName;
        }

        private void Start()
        {
            //StartCoroutine(DisplayDialogue(dialogueBubble.dialogueText.text));
        }


        public IEnumerator DisplayDialogue(string line)
        {
           
            dialogueBubble.dialogueText.text = "";

            foreach (char letter in line)
            {
                //dialogueBubble.dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            dialogueComplete?.Invoke();
        }
    }
}