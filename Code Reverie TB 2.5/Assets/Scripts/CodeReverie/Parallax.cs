using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class Parallax : SerializedMonoBehaviour
    {
        public float parallaxOffset = -0.15f;
        private Camera cam;
        private Vector2 startPos;
        private Vector2 travel => (Vector2)cam.transform.position - startPos;


        private void Awake()
        {
            cam = Camera.main;
        }


        private void Start()
        {
            startPos = transform.position;
        }

        private void FixedUpdate()
        {
            transform.position = startPos + travel * parallaxOffset;
        }
    }
}