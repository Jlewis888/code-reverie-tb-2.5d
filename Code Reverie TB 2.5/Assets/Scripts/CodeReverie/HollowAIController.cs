using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class HollowAIController : SerializedMonoBehaviour
    {
        public Animator animator;
        public GameObject target;
        public HollowControllerState hollowControllerState;


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}