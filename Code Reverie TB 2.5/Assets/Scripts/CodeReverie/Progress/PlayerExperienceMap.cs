using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "PlayerExperienceMap", menuName = "Scriptable Objects/Player Progress/PlayerExperienceMap")]
    public class PlayerExperienceMap : SerializedScriptableObject
    {
        public int startingXP = 10;
        public Dictionary<int, int> experienceMap = new Dictionary<int,int>();
       
       
        
        

        private void OnValidate()
        {
#if UNITY_EDITOR
            Dictionary<int, int> experienceMap1 = new Dictionary<int, int>(); 
            
            //Optimized values: a = 1.64713299999985, b = 1.74713299999985
            //experienceMap = new ExponentialExperienceCalculator(1.64713299999985, 1.74713299999985, 4).GetXPDictionary(1, 9);
            
            
            //experienceMap.
            
            if (!experienceMap.ContainsKey(1))
            {
                experienceMap.Add(1, startingXP);
            }
            else
            {
                experienceMap[1] = startingXP;
            }
            
            for (int i = 1; i < 99; i++)
            {
                if (!experienceMap.ContainsKey(i+1))
                {
                    experienceMap.Add(i + 1, (int)(experienceMap[i] * 1.5f));
                }
                else
                {
                    experienceMap[i + 1] = (int)(experienceMap[i] * 1.5f);
                }
                
            }
#endif
        }
    }
}