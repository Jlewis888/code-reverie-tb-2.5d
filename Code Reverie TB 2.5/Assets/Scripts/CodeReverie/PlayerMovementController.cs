using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeReverie
{
    public class PlayerMovementController : MonoBehaviour
    {
        public CharacterController characterController;
        //public Rigidbody rb;
        private Vector3 moveInput, direction;
        public float moveSpeed;
        public float speedClamp;
        public float activeMoveSpeed;
        public CharacterDirection characterDirection;
        public CharacterMovementState characterMovementState;
        private Vector3 playerVelocity;
        [SerializeField]private bool groundedPlayer;
        private float gravityValue = -9.81f;
        
        private void Awake()
        {
            //rb = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();
            moveSpeed = 5f;

            if (CombatManager.Instance == null)
            {
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
            groundedPlayer = characterController.isGrounded;
            
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            
            GamepadMovementControls();
            // moveInput.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
            // moveInput.z = GameManager.Instance.playerInput.GetAxis("Move Vertical");
            direction = moveInput.normalized;
            
            activeMoveSpeed = moveSpeed * speedClamp;
            
            playerVelocity.y += gravityValue * Time.deltaTime;
            
            // if (direction.magnitude > 0)
            // {
            //     rb.MovePosition(rb.position + moveInput * moveSpeed * Time.deltaTime);
            // }
            
            
            
        }

        private void FixedUpdate()
        {
            MoveNavMesh();
            //  if (PlayerManager.Instance.currentParty != null)
            // {
            //     switch (characterMovementState)
            //     {
            //         case CharacterMovementState.Idle: 
            //             if (direction.magnitude != 0)
            //             {
            //                 characterMovementState = CharacterMovementState.Moving;
            //             }
            //         
            //             activeMoveSpeed = 0;
            //         
            //             PlayerManager.Instance.currentParty[0].characterController.GetComponent<AnimationManager>().ChangeAnimationState("idle");
            //             //characterController.Move(playerVelocity * Time.deltaTime);
            //             break;
            //     
            //         case CharacterMovementState.Moving:
            //
            //             activeMoveSpeed = moveSpeed;
            //         
            //             if (direction.magnitude != 0)
            //             {
            //                 //rb.MovePosition(rb.position + moveInput * (activeMoveSpeed * Time.fixedDeltaTime));
            //                 Move(moveInput);
            //
            //                 if (MathF.Abs(moveInput.z) > MathF.Abs(moveInput.x))
            //                 {
            //                     if (moveInput.z > 0)
            //                     {
            //                 
            //                         characterDirection = CharacterDirection.Up;
            //                         //characterUnit.animationManager.ChangeAnimationState("fullbody_back_run");
            //                         
            //                     
            //                         //Debug.Log("Direction is up");
            //                     } else if (moveInput.z < 0)
            //                     {
            //                         characterDirection = CharacterDirection.Down;
            //                         //characterUnit.animationManager.ChangeAnimationState("fullbody_front_run");
            //                         //PlayerManager.Instance.currentParty[0].characterController.GetComponent<AnimationManager>().ChangeAnimationState("run_down");
            //
            //                     }
            //                 }
            //                 else if (MathF.Abs(moveInput.z) < MathF.Abs(moveInput.x))
            //                 {
            //                     if (moveInput.x < 0)
            //                     {
            //                         //todo update
            //                         GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
            //
            //                     }
            //                     else if (moveInput.x > 0)
            //                     {
            //                         //todo update
            //                         GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
            //                     }
            //
            //                     
            //                     characterDirection = CharacterDirection.Side;
            //                 }
            //
            //                 switch (characterDirection)
            //                 {
            //                     case CharacterDirection.Up:
            //                         //PlayerManager.Instance.currentParty[0].characterController.GetComponent<AnimationManager>().ChangeAnimationState("run_up");
            //                         GetComponent<AnimationManager>().ChangeAnimationState("run_up");
            //                         break;
            //                     case CharacterDirection.Down:
            //                         //PlayerManager.Instance.currentParty[0].characterController.GetComponent<AnimationManager>().ChangeAnimationState("run_down");
            //                         GetComponent<AnimationManager>().ChangeAnimationState("run_down");
            //                         break;
            //                     case CharacterDirection.Side:
            //                         //PlayerManager.Instance.currentParty[0].characterController.GetComponent<AnimationManager>().ChangeAnimationState("run");
            //                         GetComponent<AnimationManager>().ChangeAnimationState("run");
            //                         break;
            //                 }
            //                 
            //                 
            //             }
            //             else
            //             {
            //                 characterMovementState = CharacterMovementState.Idle;
            //             } 
            //             break;
            //     }
            // }
        }
        
        public void Move(Vector3 input)
        {

            Vector3 movement = Vector3.ClampMagnitude(input, 1);
            
            //rb.MovePosition(rb.position + movement * (activeMoveSpeed * Time.fixedDeltaTime));
            characterController.Move(movement * (activeMoveSpeed * Time.fixedDeltaTime));
            characterController.Move(playerVelocity * Time.deltaTime);
        }

        public void MoveNavMeshAgent()
        {
            
        }
        

        public void MoveNavMesh()
        {
            if (direction.sqrMagnitude >= 0.01f)
            {
                //todo Add Active move speed again
                //Vector3 newPosition = transform.position + moveInput * (activeMoveSpeed * Time.fixedDeltaTime);
                Vector3 newPosition = transform.position + moveInput * (moveSpeed * Time.fixedDeltaTime);

                NavMeshHit hit;

                bool isValid = NavMesh.SamplePosition(newPosition, out hit, .3f, NavMesh.AllAreas);

                if (isValid)
                {
                    if ((transform.position - hit.position).magnitude >= .02f)
                    {
                        transform.position = hit.position;
                    }
                }
                
            }
        }


        public void GamepadMovementControls()
        {
            moveInput.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
            moveInput.z = GameManager.Instance.playerInput.GetAxis("Move Vertical");

            
            // aimInput.x = GameManager.Instance.playerInput.GetAxis("Aim Horizontal");
            // aimInput.y = GameManager.Instance.playerInput.GetAxis("Aim Vertical");
            //
            //
            SwapCharacterDirection();
            
        }
        
        public void KeyboardMouseMovementControls()
        {
            if (!GameManager.Instance.playerInput.GetButton("Move Vertical") && !GameManager.Instance.playerInput.GetNegativeButton("Move Vertical"))
            {
                moveInput.y = 0;
            }
            else if(GameManager.Instance.playerInput.GetButton("Move Vertical"))
            {
                moveInput.y = 1;
            }
            else if (GameManager.Instance.playerInput.GetNegativeButton("Move Vertical"))
            {
                moveInput.y = -1;
            }
            
            if (!GameManager.Instance.playerInput.GetButton("Move Horizontal") && !GameManager.Instance.playerInput.GetNegativeButton("Move Horizontal"))
            {
                moveInput.x = 0;
            }
            else if(GameManager.Instance.playerInput.GetButton("Move Horizontal"))
            {
                moveInput.x = 1;
                    
                //lastInputDirection.x = 1;
            }
            else if (GameManager.Instance.playerInput.GetNegativeButton("Move Horizontal"))
            {
                moveInput.x = -1;
                //lastInputDirection.x = -1;
            }
            
            // SwapCharacterDirection();

        }
        
        public void SwapCharacterDirection()
        {
            if (moveInput.x < 0)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
                //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1f, transform.localScale.y, transform.localScale.z);
            }
            else if (moveInput.x > 0)
            {
                GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
                //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1f, transform.localScale.y, transform.localScale.z);
            }
        }
        
    }
}
