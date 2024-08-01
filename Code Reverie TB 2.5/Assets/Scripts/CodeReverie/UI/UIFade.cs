using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class UIFade : SerializedMonoBehaviour
    {
        public Image fadeScreen;
        public float fadeSpeed;

        private IEnumerator fadeRoutine;


        private void Awake()
        {
            fadeScreen = GetComponent<Image>();
            fadeSpeed = 1f;
        }

        public void FadeToBlack()
        {
            if (fadeRoutine != null)
            {
                StartCoroutine(fadeRoutine);
            }

            fadeRoutine = FadeRoutine(1);
            StartCoroutine(fadeRoutine);

        }


        public void FadeToClear()
        {
            if (fadeRoutine != null)
            {
                StartCoroutine(fadeRoutine);
            }

            fadeRoutine = FadeRoutine(0);
            StartCoroutine(fadeRoutine);
        }
        

        IEnumerator FadeRoutine(float targetAlpha)
        {
            while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
            {
                float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
                yield return null;
            }
        }
        
    }
}