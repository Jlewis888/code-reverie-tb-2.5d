using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class LevelUpPopUp : SerializedMonoBehaviour
    {

        public TMP_Text levelUpText;
        public TMP_Text skillPointText;
        public float timer;
        
        private void OnEnable()
        {
            levelUpText.text = $"Level {PlayerManager.Instance.Level}";

            timer = 5f;


        }

        private void Update()
        {
            levelUpText.text = $"Level {PlayerManager.Instance.Level}";
            
            if (timer < 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                timer -= Time.deltaTime;
            }

        }
    }
}