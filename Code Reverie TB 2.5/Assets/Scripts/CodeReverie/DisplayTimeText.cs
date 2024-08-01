using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class DisplayTimeText : SerializedMonoBehaviour
    {
        public TMP_Text timeText;

        private void Awake()
        {
            timeText = GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {
            timeText.text = GameManager.Instance.GetComponent<DisplayTime>().timeText;
        }
        
    }
}