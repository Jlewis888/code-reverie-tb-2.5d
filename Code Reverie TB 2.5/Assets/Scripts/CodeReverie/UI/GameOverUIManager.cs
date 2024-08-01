using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class GameOverUIManager : SerializedMonoBehaviour
    {
        public GameObject buttonTitleHolder;
        public Button continueButton;
        public Button loadGameButton;
        public Button quitButton;
        
        
        
        public Image fadeScreen;
        public float fadeSpeed;
        private IEnumerator fadeRoutine;

        private void Awake()
        {
            fadeSpeed = 0.5f;
            
            continueButton.onClick.AddListener(Continue);
            loadGameButton.onClick.AddListener(LoadGame);
            quitButton.onClick.AddListener(Quit);
            
        }

        private void OnEnable()
        {
            buttonTitleHolder.SetActive(false);
            FadeToBlack();
        }

        private void OnDisable()
        {
            FadeToClear();
            buttonTitleHolder.SetActive(false);
        }


        public void FadeToBlack()
        {
            if (fadeRoutine != null)
            {
                StartCoroutine(fadeRoutine);
            }

            fadeRoutine = FadeRoutine(0.9f);
            StartCoroutine(fadeRoutine);

        }
        
        public void FadeToClear()
        {
            // if (fadeRoutine != null)
            // {
            //     StartCoroutine(fadeRoutine);
            // }
            //
            // fadeRoutine = FadeRoutine(0);
            // StartCoroutine(fadeRoutine);
            
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 0);
        }
        
        IEnumerator FadeRoutine(float targetAlpha)
        {
            while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
            {
                float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
                yield return null;
            }
            
            buttonTitleHolder.SetActive(true);
        }

        public void Continue()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            EventManager.Instance.generalEvents.OnGameRestart();
            gameObject.SetActive(false);
            
        }

        public void LoadGame()
        {
            
        }

        public void Quit()
        {
            
        }
        
    }
}