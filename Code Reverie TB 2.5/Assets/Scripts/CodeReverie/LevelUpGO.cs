using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class LevelUpGO : SerializedMonoBehaviour
    {
        private void Awake()
        {
            
        }

        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
               
                Destroy(gameObject);
            }
        }
    }
}