using System;
using System.Collections.Generic;
using System.Timers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class StatModifierObject : SerializedMonoBehaviour, IStatModifierProvider
    {
        public string id;
        public Stat statModifier;
        public float value;
        public float time;
        public float timer;
        public int turns;


        private void Awake()
        {
            
        }


        private void Update()
        {
            if (statModifier.statAttribute != null)
            {
                
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                
            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        public IEnumerable<float> GetAdditiveStatModifiers(StatAttribute stat)
        {
            if (statModifier.statAttribute == stat)
            {
                yield return value;
            }
        }

        public IEnumerable<float> GetPercentageStatModifiers(StatAttribute stat)
        {
            throw new NotImplementedException();
        }
    }
}