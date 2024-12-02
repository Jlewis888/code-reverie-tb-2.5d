using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class MovePlayerObject : SerializedMonoBehaviour
    {
        public CharacterBattleManager characterBattleManager;
        public Rigidbody rb;
        private Vector3 moveInput, direction;
        public float moveSpeed;
        public float maxDistance;
        public GameObject centerObject;


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            maxDistance = 3f;
            moveSpeed = 3f;
        }

        private void OnDisable()
        {
            GetComponent<AnimationManager>().currentAnimation = "";
        }

        private void Update()
        {
            GamepadMovementControls();
            direction = moveInput.normalized;
            
            
            if (direction.magnitude != 0)
            {
                
                if (centerObject != null)
                {
                    float dist = Vector3.Distance(transform.position, centerObject.transform.position);
                    
                    if (dist > maxDistance)
                    {
                        Vector3 vect =  transform.position - centerObject.transform.position;
                        vect *= maxDistance/dist;
                        transform.position = centerObject.transform.position + vect;
                    }
                    
                    Move(moveInput);
                    

                    // if (dist > maxDistance)
                    // {
                    //     Debug.Log("'dfhasfhdsakjfsakl'");
                    //     Vector3 vect = (other.transform.position - transform.position).normalized;
                    //     vect *= (dist-maxDistance);
                    //     transform.position += vect;
                    //     //transform.position = other.transform.position + vect * maxDistance;
                    //     //transform.position = transform.position.normalized * maxDistance;
                    // }
                    // else
                    // {
                    //     Move(moveInput);
                    // }
                }
            }
        }

        public void Init()
        {

            if (characterBattleManager != null)
            {
                // GetComponent<Animator>().runtimeAnimatorController = characterBattleManager.GetComponent<Animator>().runtimeAnimatorController;
                // GetComponent<AnimationManager>().animator = GetComponent<Animator>();
                
                transform.position = characterBattleManager.transform.position + characterBattleManager.transform.right * 1.5f;
                centerObject = characterBattleManager.gameObject;
            }
            
            
        }
        
        
        public void GamepadMovementControls()
        {
            moveInput.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
            moveInput.z = GameManager.Instance.playerInput.GetAxis("Move Vertical");
            
        }
        
        public void Move(Vector3 input)
        {

            Vector3 movement = Vector3.ClampMagnitude(input, 1);
            
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        }
        
        
        
    }
}