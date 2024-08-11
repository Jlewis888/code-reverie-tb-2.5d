using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class Rotator : SerializedMonoBehaviour
    {
        public Vector2 lookAtRotation;
        public Vector3 aimInput;
        public float angle;
        public float rotationSpeed;


        private void Update()
        {
            switch (GameManager.Instance.currentControlScheme)
            {
                case ControlSchemeType.KeyboardMouse:
                    //KeyboardMouseAim();
                    KeyboardMouseAim2();
                    break;
                case ControlSchemeType.Gamepad:
                    GamepadAim();
                    break;
            }
        }

        private void LateUpdate()
        {
            switch (GameManager.Instance.currentControlScheme)
            {
                case ControlSchemeType.KeyboardMouse:

                    // if (GetComponentInParent<CharacterUnitController>().characterUnit.spriteRenderer.flipX)
                    // {
                    //     //transform.rotation = Quaternion.Euler(180,0,angle);
                    //     transform.localScale = new Vector3(1, -1, 1);
                    // }
                    // else
                    // {
                    //     //transform.rotation = Quaternion.Euler(0,0,angle);
                    //     transform.localScale = new Vector3(1, 1, 1);
                    // }
                    transform.rotation = Quaternion.Euler(0,0,angle);
                        
                    break;
                case ControlSchemeType.Gamepad:
                    
                    if (lookAtRotation.x != 0 || lookAtRotation.y != 0)
                    {
                            
                        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        transform.rotation =
                            (Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed));
                            
                    }
                    
                    break;
            }
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