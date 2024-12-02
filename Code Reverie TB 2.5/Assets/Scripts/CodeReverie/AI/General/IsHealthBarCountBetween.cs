using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CodeReverie
{
    public class IsHealthBarCountBetween : EnemyConditional
    {
        public SharedInt currentPhase;
        public int phase;
        public int max;
        public int min;
        
        
        
        public override TaskStatus OnUpdate()
        {
            //Debug.Log(health.CurrentHealthBarCount);
            return (health.CurrentHealthBarCount <= max && health.CurrentHealthBarCount > min) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}