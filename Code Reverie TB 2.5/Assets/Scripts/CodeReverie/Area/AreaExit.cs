using System;
using System.Collections;
using Sirenix.OdinInspector;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    public class AreaExit : SerializedMonoBehaviour
    {
        public SceneField sceneToLoad;
        public string sceneTransitionName;

        private float waitToLoadTime = 1f;
        
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.GetComponentInParent<ComponentTagManager>().HasTag(ComponentTag.Player) && other.gameObject == PlayerManager.Instance.currentParty[0].characterController.gameObject)
            {
                
                
                if (!sceneToLoad.IsNullOrEmpty)
                {
                    
                    other.GetComponent<PlayerMovementController>().enabled = false;
                    
                    
                    GameSceneManager.Instance.isTransitioningScenes = true;
                    GameSceneManager.Instance.SetTransitionName(sceneTransitionName);
                
                    // CanvasManager.Instance.uiFade.FadeToBlack();
                    // StartCoroutine(LoadSceneRoutine());
                    
                    TransitionAnimator.Start(
                        TransitionType.Fade, // transition type
                        duration: 1f,
                        sceneNameToLoad: sceneToLoad
                    );
                    
                }
                
                Debug.Log("Load New Area");

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