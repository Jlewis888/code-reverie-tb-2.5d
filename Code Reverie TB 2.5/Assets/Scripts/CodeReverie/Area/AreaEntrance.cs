using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class AreaEntrance : SerializedMonoBehaviour
    {
        public string transitionName;


        private void Start()
        {
            if (transitionName == GameSceneManager.Instance.SceneTransitionName)
            {

                foreach (Character character in PlayerManager.Instance.currentParty)
                {
                    if (character.characterController != null)
                    {
                        character.characterController.transform.position = transform.position;
                    }
                }
                
                //PlayerController.Instance.transform.position = transform.position;
                CanvasManager.Instance.uiFade.FadeToClear();
            }
        }
    }
}