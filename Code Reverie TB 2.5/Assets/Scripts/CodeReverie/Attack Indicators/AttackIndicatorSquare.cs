using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    //[ExecuteAlways]
    public class AttackIndicatorSquare : AttackIndicator
    {
        public Transform growEffect;
        public Collider2D collider;
        [PropertyRange(0, "Max"), PropertyOrder(3)] public float size;
        
        [PropertyOrder(4)]
        public float Max = 100;

        public GameObject target;

        private void Update()
        {
            Max = collider.transform.localScale.y;

            if (growEffect.localScale.x != collider.transform.localScale.x)
            {
                growEffect.localScale = new Vector3(collider.transform.localScale.x, growEffect.localScale.y,
                    growEffect.localScale.x);
            }

            growEffect.localPosition = collider.transform.localPosition;
            
            growEffect.localScale = new Vector3(growEffect.localScale.x, size, 0);


            if (target != null)
            {
                Vector3 rotation = (target.transform.position - transform.position).normalized;
                //Quaternion rotation = Quaternion.LookRotation( Vector2.right, direction);
                
                var angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                transform.localRotation = Quaternion.Euler(0,0,angle);
                //transform.rotation = rotation;
            }
            
        }
    }
}