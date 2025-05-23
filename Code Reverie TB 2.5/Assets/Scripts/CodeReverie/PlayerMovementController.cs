using System;
using System.Collections;
using System.Collections.Generic;
using ProjectDawn.Navigation.Hybrid;
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
        [SerializeField] private bool groundedPlayer;
        private float gravityValue = -9.81f;
        public string areaMask;

        private void Awake()
        {
            //rb = GetComponent<Rigidbody>();
            characterController = GetComponent<CharacterController>();
            moveSpeed = 5f;

            if (CombatManager.Instance == null)
            {
                GetComponent<AgentAuthoring>().enabled = false;
                GetComponent<AgentNavMeshAuthoring>().enabled = false;
                GetComponent<AgentCylinderShapeAuthoring>().enabled = false;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            areaMask = "Walkable";
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
            //MoveNavMesh();
            //MoveNavMesh2();
            MoveNavMesh3();
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
                Vector3 newPosition = transform.position + moveInput.normalized * (moveSpeed * Time.fixedDeltaTime);

                NavMeshHit hit;

                // int areaMask << NavMesh.GetAreaFromName("Jump");
                // areaMask += 1 << NavMesh.GetAreaFromName("Everything");//turn on all
                // areaMask -= 1 << NavMesh.GetAreaFromName("Jump");
                // nma.areaMask=areaMask;

                int areaIndex = NavMesh.GetAreaFromName(areaMask); // e.g., "Walkable"
                int _areaMask = 1 << areaIndex;

                //bool isValid = NavMesh.SamplePosition(newPosition, out hit, .3f, NavMesh.AllAreas);
                bool isValid = NavMesh.SamplePosition(newPosition, out hit, .3f, _areaMask);

                // Debug.Log(isValid);
                // Debug.Log(hit.mask);
                //
                if (isValid)
                {
                    // if ((transform.position - hit.position).magnitude >= .02f)
                    // {
                    //     transform.position = hit.position;
                    // }

                    // Raycast downward from above the target point to find terrain
                    RaycastHit raycastHit;
                    Vector3 checkPosition = hit.position + Vector3.up * 2f;
                    if (Physics.Raycast(checkPosition, Vector3.down, out raycastHit, 5f,
                            LayerMask.GetMask("Default", "Terrain")))
                    {
                        Vector3 adjustedPosition = raycastHit.point;

                        if ((transform.position - adjustedPosition).magnitude >= 0.02f)
                        {
                            transform.position = adjustedPosition;
                        }
                    }
                    else
                    {
                        // Fallback to flat navmesh position if raycast fails
                        transform.position = hit.position;
                    }
                }

                // Directional Animation
                if (Mathf.Abs(moveInput.z) > Mathf.Abs(moveInput.x))
                {
                    if (moveInput.z > 0)
                    {
                        characterDirection = CharacterDirection.Up;
                        GetComponent<AnimationManager>().ChangeAnimationState("run_up");
                    }
                    else
                    {
                        characterDirection = CharacterDirection.Down;
                        GetComponent<AnimationManager>().ChangeAnimationState("run_down");
                    }
                }
                else
                {
                    characterDirection = CharacterDirection.Side;

                    if (moveInput.x < 0)
                    {
                        GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = true;
                    }
                    else if (moveInput.x > 0)
                    {
                        GetComponentInChildren<CharacterUnit>().spriteRenderer.flipX = false;
                    }

                    GetComponent<AnimationManager>().ChangeAnimationState("run");
                }
            }
            else
            {
                // No movement, play idle
                GetComponent<AnimationManager>().ChangeAnimationState("idle");
            }
        }

        public void MoveNavMesh2()
        {
            var animationManager = GetComponent<AnimationManager>();
            var characterUnit = GetComponentInChildren<CharacterUnit>();

            if (direction.sqrMagnitude >= 0.01f)
            {
                Vector3 desiredDirection = moveInput.normalized;
                Vector3 targetPosition = transform.position + desiredDirection * (moveSpeed * Time.fixedDeltaTime);

                int areaIndex = NavMesh.GetAreaFromName(areaMask); // e.g., "Walkable"
                int _areaMask = 1 << areaIndex;

                NavMeshHit sampleHit;
                bool isSampleValid = NavMesh.SamplePosition(targetPosition, out sampleHit, 1f, _areaMask);

                if (isSampleValid)
                {
                    // Check pathability
                    if (!NavMesh.Raycast(transform.position, sampleHit.position, out NavMeshHit raycastHit, _areaMask))
                    {
                        // Raycast downward from sample point to terrain
                        RaycastHit terrainHit;
                        Vector3 raycastOrigin = sampleHit.position + Vector3.up * 2f;

                        if (Physics.Raycast(raycastOrigin, Vector3.down, out terrainHit, 5f,
                                LayerMask.GetMask("Default", "Terrain")))
                        {
                            Vector3 adjustedPosition = terrainHit.point;

                            // Use Lerp for smoother movement
                            if ((transform.position - adjustedPosition).magnitude >= 0.02f)
                            {
                                transform.position = Vector3.Lerp(transform.position, adjustedPosition,
                                    moveSpeed * Time.fixedDeltaTime);
                            }
                        }
                        else
                        {
                            // Fallback if no terrain below
                            transform.position = Vector3.Lerp(transform.position, sampleHit.position, 0.5f);
                        }
                    }
                }

                // Directional animation
                if (Mathf.Abs(moveInput.z) > Mathf.Abs(moveInput.x))
                {
                    characterDirection = moveInput.z > 0 ? CharacterDirection.Up : CharacterDirection.Down;
                    animationManager.ChangeAnimationState(characterDirection == CharacterDirection.Up
                        ? "run_up"
                        : "run_down");
                }
                else
                {
                    characterDirection = CharacterDirection.Side;
                    characterUnit.spriteRenderer.flipX = moveInput.x < 0;
                    animationManager.ChangeAnimationState("run");
                }
            }
            else
            {
                animationManager.ChangeAnimationState("idle");
            }
        }


        public void MoveNavMesh3()
        {
            var animationManager = GetComponent<AnimationManager>();
            var characterUnit = GetComponentInChildren<CharacterUnit>();

            if (direction.sqrMagnitude >= 0.01f)
            {
                Vector3 desiredDirection = moveInput.normalized;
                Vector3 targetPosition = transform.position + desiredDirection * (moveSpeed * Time.fixedDeltaTime);

                int areaIndex = NavMesh.GetAreaFromName(areaMask); // e.g., "Walkable"
                int _areaMask = 1 << areaIndex;

                NavMeshHit sampleHit;
                bool isSampleValid = NavMesh.SamplePosition(targetPosition, out sampleHit, 1f, _areaMask);

                if (isSampleValid)
                {
                    // Ensure the path is walkable
                    if (!NavMesh.Raycast(transform.position, sampleHit.position, out NavMeshHit raycastHit, _areaMask))
                    {
                        
                        transform.position = Vector3.MoveTowards(
                            transform.position,
                            sampleHit.position,
                            moveSpeed * Time.fixedDeltaTime
                        );
                        
                        // // Align with terrain using raycast
                        // RaycastHit terrainHit;
                        // Vector3 rayOrigin = sampleHit.position + Vector3.up * 2f;
                        //
                        // if (Physics.Raycast(rayOrigin, Vector3.down, out terrainHit, 5f,
                        //         LayerMask.GetMask("Default", "Terrain")))
                        // {
                        //     Vector3 adjustedPosition = terrainHit.point;
                        //
                        //     // Move directly toward the adjusted position without lerp
                        //     transform.position = Vector3.MoveTowards(
                        //         transform.position,
                        //         adjustedPosition,
                        //         moveSpeed * Time.fixedDeltaTime
                        //     );
                        // }
                        // else
                        // {
                        //     // Fallback: move toward navmesh position
                        //     transform.position = Vector3.MoveTowards(
                        //         transform.position,
                        //         sampleHit.position,
                        //         moveSpeed * Time.fixedDeltaTime
                        //     );
                        // }
                    }
                }

                // Directional animation
                if (Mathf.Abs(moveInput.z) > Mathf.Abs(moveInput.x))
                {
                    characterDirection = moveInput.z > 0 ? CharacterDirection.Up : CharacterDirection.Down;
                    animationManager.ChangeAnimationState(characterDirection == CharacterDirection.Up
                        ? "run_up"
                        : "run_down");
                }
                else
                {
                    characterDirection = CharacterDirection.Side;
                    characterUnit.spriteRenderer.flipX = moveInput.x < 0;
                    animationManager.ChangeAnimationState("run");
                }
            }
            else
            {
                animationManager.ChangeAnimationState("idle");
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
            if (!GameManager.Instance.playerInput.GetButton("Move Vertical") &&
                !GameManager.Instance.playerInput.GetNegativeButton("Move Vertical"))
            {
                moveInput.y = 0;
            }
            else if (GameManager.Instance.playerInput.GetButton("Move Vertical"))
            {
                moveInput.y = 1;
            }
            else if (GameManager.Instance.playerInput.GetNegativeButton("Move Vertical"))
            {
                moveInput.y = -1;
            }

            if (!GameManager.Instance.playerInput.GetButton("Move Horizontal") &&
                !GameManager.Instance.playerInput.GetNegativeButton("Move Horizontal"))
            {
                moveInput.x = 0;
            }
            else if (GameManager.Instance.playerInput.GetButton("Move Horizontal"))
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