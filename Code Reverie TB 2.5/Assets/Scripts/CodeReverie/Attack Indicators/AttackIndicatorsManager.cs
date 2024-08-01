using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class AttackIndicatorsManager : SerializedMonoBehaviour
    {
        public AttackIndicatorSquare attackIndicatorSquare;
        public AttackIndicatorLine attackIndicatorLine;


        public void Start()
        {
            CloseAllIndicators();
        }


        public void CloseAllIndicators()
        {
            

            List<AttackIndicator> attackIndicators = GetComponentsInChildren<AttackIndicator>().ToList();
            
            foreach (AttackIndicator attackIndicator in attackIndicators)
            {
                attackIndicator.gameObject.SetActive(false);
            }

        }
        
    }
}