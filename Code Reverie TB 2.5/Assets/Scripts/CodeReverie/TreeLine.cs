using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeReverie
{
    public class TreeLine : SerializedMonoBehaviour
    {
        public List<EnvironmentAnimations> treeAnimationGroup = new List<EnvironmentAnimations>();
        public float animationStartDelay = 0;
        public string startAnimation;
        public bool random;
        
        
        private void Awake()
        {
            treeAnimationGroup = GetComponentsInChildren<EnvironmentAnimations>().ToList();

            if (random)
            {
                animationStartDelay = Random.Range(0, 0.75f);
            }
            
            foreach (var tree in treeAnimationGroup)
            {
                tree.startAnimation = startAnimation;
                tree.animationStartDelay = animationStartDelay;
            }
        }
    }
}