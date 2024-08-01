using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "PlayerExperienceMap", menuName = "Scriptable Objects/Player Progress/PlayerExperienceMap")]
    public class PlayerExperienceMap : SerializedScriptableObject
    {
        public float startNum;
        public Dictionary<int, float> experienceMap = new Dictionary<int,float>();
       

        private void OnValidate()
        {
#if UNITY_EDITOR

            if (!experienceMap.ContainsKey(1))
            {
                experienceMap.Add(1, startNum);
            }
            else
            {
                experienceMap[1] = startNum;
            }
            
            for (int i = 1; i < 99; i++)
            {
                if (!experienceMap.ContainsKey(i+1))
                {
                    experienceMap.Add(i + 1, (experienceMap[i] * 1.5f));
                }
                else
                {
                    experienceMap[i + 1] = experienceMap[i] * 1.5f;
                }
                
            }
#endif
        }
    }
}