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
        
        public string pausedAnimation;
        public float pausedAnimationSpeed;
        public bool animationsPaused;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        
        public void ChangeAnimationState(string newAnimation)
        {

            // if (animationsPaused)
            // {
            //     return;
            // }
            
            if (currentAnimation == newAnimation)
            {
                return;
            }
           
            
            
            //Debug.Log($"{name}: Animation Changed to {newAnimation}");
            //animator.StopPlayback();
            animator.Play(newAnimation);

            
            currentAnimation = newAnimation;
            currentAnimationSpeed = animator.speed;
            
            
        }

        public void PauseAnimation()
        {
            animator.speed = 0;
            // pausedAnimation = currentAnimation;
            // pausedAnimationSpeed = currentAnimationSpeed;
            animationsPaused = true;
        }

        public void ResumeAnimation()
        {
            animationsPaused = false;
            animator.speed = 1;
        }
        
    }
}