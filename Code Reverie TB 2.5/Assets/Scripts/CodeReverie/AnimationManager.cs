using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class AnimationManager : SerializedMonoBehaviour
    {
        public Animator animator;
        public string currentAnimation;
        public float currentAnimationSpeed;


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
            currentAnimationSpeed = animator.speed;
        }

        public void PauseAnimation()
        {
            animator.speed = 0;
        }

        public void ResumeAnimation()
        {
            animator.speed = currentAnimationSpeed;
        }
        
    }
}