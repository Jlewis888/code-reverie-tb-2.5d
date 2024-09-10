using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class MaterialsManager : SerializedMonoBehaviour
    {
        public static MaterialsManager Instance;
        public Material dissolveMaterial;


        private void Awake()
        {
            Instance = this;
        }
    }
}