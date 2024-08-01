using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class TestShaderScript : SerializedMonoBehaviour
    {
        public bool changeColor;
        public bool changeThickness;

        private void Awake()
        {

            if (changeColor)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }

            if (changeThickness)
            {
                GetComponent<Renderer>().material.SetFloat("_Thickness", 2f);
            }
        }
    }
}