using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CharacterUnit : SerializedMonoBehaviour
    {
        public Animator animator;
        public Character character;
        public SpriteRenderer spriteRenderer;


        private void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}