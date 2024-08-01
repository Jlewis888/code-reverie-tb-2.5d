using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class EnvironmentAnimations : SerializedMonoBehaviour
    {
        public AnimationManager animationManager;
        public float animationStartDelay = 0;
        public string startAnimation;

        private void Awake()
        {
            animationManager = GetComponent<AnimationManager>();

            //StartCoroutine(StartAnimation());
        }

        private void Start()
        {
            StartCoroutine(StartAnimation());
        }

        public IEnumerator StartAnimation()
        {
            yield return new WaitForSeconds(animationStartDelay);
            animationManager.ChangeAnimationState(startAnimation);
        }
        
        
    }
}