using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    public class AreaExit : SerializedMonoBehaviour
    {
        public SceneField sceneToLoad;
        public string sceneTransitionName;

        private float waitToLoadTime = 1f;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<CharacterGroundCollider>() && other.gameObject.GetComponentInParent<ComponentTagManager>().HasTag(ComponentTag.Player))
            {
                GameSceneManager.Instance.isTransitioningScenes = true;
                GameSceneManager.Instance.SetTransitionName(sceneTransitionName);
                
                
                CanvasManager.Instance.uiFade.FadeToBlack();
                StartCoroutine(LoadSceneRoutine());

            }
        }


        IEnumerator LoadSceneRoutine()
        {
            // while (waitToLoadTime >= 0)
            // {
            //     waitToLoadTime -= Time.deltaTime;
            // }
            
            yield return new WaitForSeconds(waitToLoadTime);
            SceneManager.LoadScene(sceneToLoad);
        }
        
    }
}