using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class AnimationManager : SerializedMonoBehaviour
    {
        public Animator animator;
        public string currentAnimation;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        
        public void ChangeAnimationState(string newAnimation)
        {
           
            
            if (currentAnimation == newAnimation)
            {
                return;
            }
           
            
            animator.Play(newAnimation);
            currentAnimation = newAnimation;
        }
        
    }
}