using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class AimRotator : SerializedMonoBehaviour
    {
        public Vector2 lookAtRotation;
        public Vector2 lookAtRotation2D;
        public Vector3 aimInput;
        public bool canMove;
        public float angle;
        public float rotationSpeed;
        
        
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.toggleInventory += SetCanMoveInventoryListener;
        }
        
        
        private void OnDisable()
        {
            EventManager.Instance.generalEvents.toggleInventory -= SetCanMoveInventoryListener;
        }
        
        private void Update()
        {
            if (canMove)
            {

                switch (GameManager.Instance.currentControlScheme)
                {
                    case ControlSchemeType.KeyboardMouse:
                        KeyboardMouseAim();
                        break;
                    case ControlSchemeType.Gamepad:
                        GamepadAim();
                        break;
                }
                
                
                
            }
            else
            {
                lookAtRotation = Vector2.zero;
                
            }


        }
        
        private void LateUpdate()
        {


            if (canMove)
            {
                switch (GameManager.Instance.currentControlScheme)
                {
                    case ControlSchemeType.KeyboardMouse:

                        if (GetComponentInParent<SpriteRenderer>().flipX)
                        {
                            transform.rotation = Quaternion.Euler(0,-180,angle);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(0,0,angle);
                        }
                    
                        // if (!GetComponentInParent<PlayerCharacterManager>().facingRight)
                        // {
                        //     //transform.rotation = Quaternion.Euler(0,-180,angle);
                        // }
                        // else
                        // {
                        //     //transform.rotation = Quaternion.Euler(0,0,angle);
                        // }
                        // transform.rotation = Quaternion.Euler(0,0,angle);

                        break;
                    case ControlSchemeType.Gamepad:
                    
                        if (lookAtRotation.x != 0 || lookAtRotation.y != 0)
                        {
                
                            // direction = Quaternion.LookRotation( transform.right, lookAtRotation);
                            // direction = Quaternion.Euler(0,0,direction.y);


                            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                            transform.rotation =
                                (Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed));

                

                            //transform.rotation = Quaternion.LookRotation(Vector3.forward, lookAtRotation);
                        }

                    
                   
                        break;
                }
            }

            
        }
        
        
        public void SetCanMoveInventoryListener(bool canMove)
        {
            this.canMove = !canMove;

            transform.rotation = Quaternion.Euler(0, 0, 0f);
            
            

        }


        public void KeyboardMouseAim()
        {
            Vector3 mousePos = Input.mousePosition;

            //Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            
            
            
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        }
        
        
        public void KeyboardMouseAim2()
        {
            //Vector3 mousePos = Input.mousePosition;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

            Vector3 rotation = mousePos - transform.position;
            
            angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        }

        public void GamepadAim()
        {
            aimInput.x = GameManager.Instance.playerInput.GetAxis("Aim Horizontal");
            aimInput.y = GameManager.Instance.playerInput.GetAxis("Aim Vertical");

                
            angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
                
            if (aimInput.normalized.magnitude > 0)
            {
                lookAtRotation.x = GameManager.Instance.playerInput.GetAxis("Aim Horizontal");
                lookAtRotation.y = GameManager.Instance.playerInput.GetAxis("Aim Vertical");

            }
            else
            {
                lookAtRotation.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
                lookAtRotation.y = GameManager.Instance.playerInput.GetAxis("Move Vertical");
                
            }
        }
    }
}
