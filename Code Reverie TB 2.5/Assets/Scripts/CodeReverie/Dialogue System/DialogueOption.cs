using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class DialogueOption : SerializedMonoBehaviour
    {
        public GameObject selector;
        public TMP_Text dialogueText;
        public bool isOption;
        
        private void Awake()
        {
            selector.SetActive(false);
        }
    }
}