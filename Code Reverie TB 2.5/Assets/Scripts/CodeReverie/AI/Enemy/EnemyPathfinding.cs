using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class EnemyPathfinding : SerializedMonoBehaviour
    {
        public float moveSpeed = 1f;
        public float activeMoveSpeed = 1f;
        public Vector2 moveInput;
        public Rigidbody2D rb;
        public Vector2 targetPosition;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            //rb.MovePosition(rb.position + moveInput * (activeMoveSpeed * Time.fixedDeltaTime));

            //transform.position = Vector2.Lerp(transform.position, targetPosition, activeMoveSpeed * Time.deltaTime);
        }

        public void MoveTo(Vector2 position)
        {
            targetPosition = new Vector2(transform.position.x + position.x, transform.position.y + position.y);
            moveInput = targetPosition;
        }

        public void SetTargetPosition(Vector2 position)
        {
            targetPosition = position;
        }
        
    }
}