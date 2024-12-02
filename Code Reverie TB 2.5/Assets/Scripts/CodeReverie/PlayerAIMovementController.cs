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
            float moveSpeed = followTarget.GetComponent<PlayerMovementController>().moveSpeed;
            FaceFollowTarget();
            
            if (Vector3.Distance(transform.position, followTarget.transform.position) >= 1)
            {
                
                GetComponent<AnimationManager>().ChangeAnimationState("run");

                isMoving = true;
                
                transform.position = Vector3.MoveTowards(transform.position,
                    followTarget.transform.position,
                    moveSpeed * Time.deltaTime);
            }
            else
            {
                
                if (isMoving & Vector3.Distance(transform.position, followTarget.transform.position) >= .75f)
                {
                
                    GetComponent<AnimationManager>().ChangeAnimationState("run");
                    
                    transform.position = Vector3.MoveTowards(transform.position,
                        followTarget.transform.position,
                        moveSpeed * Time.deltaTime);
                }
                else
                {
                    isMoving = false;
                    GetComponent<AnimationManager>().ChangeAnimationState("idle");
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
