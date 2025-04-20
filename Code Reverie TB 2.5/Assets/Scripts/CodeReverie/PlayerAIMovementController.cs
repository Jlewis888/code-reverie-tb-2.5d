using System;
using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class PlayerAIMovementController : SerializedMonoBehaviour
    {
        public GameObject followTarget;
        public bool isMoving;

        private void Update()
        {
            
            
        }

        private void FixedUpdate()
        {
            Move2();
        }

        public void Move()
        {
            float moveSpeed = followTarget.GetComponent<PlayerMovementController>().moveSpeed;
            Vector3 targetDirection = followTarget.transform.position - transform.position;
            FaceFollowTarget();
            
            var animationManager = GetComponent<AnimationManager>();
            var characterUnit = GetComponentInChildren<CharacterUnit>();
            
            if (Vector3.Distance(transform.position, followTarget.transform.position) >= 1)
            {
                
                GetComponent<AnimationManager>().ChangeAnimationState("run");

                isMoving = true;
                
                transform.position = Vector3.MoveTowards(transform.position,
                    followTarget.transform.position,
                    moveSpeed * Time.deltaTime);
                
                
                // Determine animation direction
                if (Mathf.Abs(targetDirection.z) > Mathf.Abs(targetDirection.x))
                {
                    if (targetDirection.z > 0)
                    {
                        animationManager.ChangeAnimationState("run_up");
                    }
                    else
                    {
                        animationManager.ChangeAnimationState("run_down");
                    }
                }
                else
                {
                    animationManager.ChangeAnimationState("run");
                    characterUnit.spriteRenderer.flipX = targetDirection.x < 0;
                }
                
            }
            else
            {
                
                if (isMoving & Vector3.Distance(transform.position, followTarget.transform.position) >= .75f)
                {
                
                    //GetComponent<AnimationManager>().ChangeAnimationState("run");
                    
                    transform.position = Vector3.MoveTowards(transform.position,
                        followTarget.transform.position,
                        moveSpeed * Time.deltaTime);
                    
                    
                    // Determine animation direction
                    if (Mathf.Abs(targetDirection.z) > Mathf.Abs(targetDirection.x))
                    {
                        if (targetDirection.z > 0)
                        {
                            animationManager.ChangeAnimationState("run_up");
                        }
                        else
                        {
                            animationManager.ChangeAnimationState("run_down");
                        }
                    }
                    else
                    {
                        animationManager.ChangeAnimationState("run");
                        characterUnit.spriteRenderer.flipX = targetDirection.x < 0;
                    }
                    
                }
                else
                {
                    isMoving = false;
                    GetComponent<AnimationManager>().ChangeAnimationState("idle");
                }
                
                
            }
        }

        public void Move2()
        {
            float moveSpeed = followTarget.GetComponent<PlayerMovementController>().moveSpeed;
            Vector3 targetDirection = followTarget.transform.position - transform.position;

            FaceFollowTarget();

            var animationManager = GetComponent<AnimationManager>();
            var characterUnit = GetComponentInChildren<CharacterUnit>();

            float distance = Vector3.Distance(transform.position, followTarget.transform.position);

            if (distance >= 1f || (isMoving && distance >= 0.75f))
            {
                isMoving = true;

                // Move toward the follow target
                transform.position = Vector3.MoveTowards(transform.position,
                    followTarget.transform.position,
                    moveSpeed * Time.deltaTime);

                // Determine animation direction
                if (Mathf.Abs(targetDirection.z) > Mathf.Abs(targetDirection.x))
                {
                    if (targetDirection.z > 0)
                    {
                        animationManager.ChangeAnimationState("run_up");
                    }
                    else
                    {
                        animationManager.ChangeAnimationState("run_down");
                    }
                }
                else
                {
                    animationManager.ChangeAnimationState("run");
                    characterUnit.spriteRenderer.flipX = targetDirection.x < 0;
                }
            }
            else
            {
                if (isMoving)
                {
                    isMoving = false;
                    animationManager.ChangeAnimationState("idle");
                }
            }
        }
        

        public void FaceFollowTarget()
        {
            if (transform.position.x > followTarget.transform.position.x)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
                //playerCharacterUnit.activeUnitSprite.transform.localScale =  new Vector3(-1,1,1);
            }
            else
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
                //playerCharacterUnit.activeUnitSprite.transform.localScale = new Vector3(1,1,1);
            }
        }
        
    }
}
