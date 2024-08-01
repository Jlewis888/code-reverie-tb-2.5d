using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CharacterMovementController : SerializedMonoBehaviour
    {
        public float moveSpeed = 1f;
        public float dashSpeed = 5f;
        public float activeMoveSpeed = 1f;
        public Vector2 moveInput;
        public Rigidbody2D rb;
        
        
        
        
        
        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveInput * (activeMoveSpeed * Time.fixedDeltaTime));
        }
        
    }
}