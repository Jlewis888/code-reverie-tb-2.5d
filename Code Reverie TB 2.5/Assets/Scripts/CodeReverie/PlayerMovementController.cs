using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    public class PlayerMovementController : MonoBehaviour
    {
        
        public Rigidbody rb;
        private Vector3 moveInput, direction;
        public float moveSpeed;
        public float speedClamp;
        public float activeMoveSpeed;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            moveInput.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
            moveInput.z = GameManager.Instance.playerInput.GetAxis("Move Vertical");
            direction = moveInput.normalized;

            activeMoveSpeed = moveSpeed * speedClamp;
            
            GamepadMovementControls();

            if (direction.magnitude > 0)
            {
                rb.MovePosition(rb.position + moveInput * moveSpeed * Time.deltaTime);
            }
            
        }
        
        
        public void GamepadMovementControls()
        {
            moveInput.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
            moveInput.y = GameManager.Instance.playerInput.GetAxis("Move Vertical");

            
            // aimInput.x = GameManager.Instance.playerInput.GetAxis("Aim Horizontal");
            // aimInput.y = GameManager.Instance.playerInput.GetAxis("Aim Vertical");
            //
            //
            // SwapCharacterDirection();
            
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
        
    }
}
