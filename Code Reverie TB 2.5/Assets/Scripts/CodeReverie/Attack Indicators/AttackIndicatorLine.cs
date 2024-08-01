using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    //[ExecuteAlways]
    public class AttackIndicatorLine : AttackIndicator
    {
        public LineRenderer lineRenderer;
        public Transform firePoint;
        public Transform endPoint;
        public float MaxLength;
        public LayerMask layerMask;

        private void Update()
        {
            lineRenderer.SetPosition(0, firePoint.position);
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.right, float.PositiveInfinity, layerMask);
            //Debug.DrawRay(transform.position, Vector2.right * hit2D.distance, Color.red);
            
            
            if (hit2D.collider != null)
            {
                lineRenderer.SetPosition(1, hit2D.point);
            }
            else
            {
                lineRenderer.SetPosition(1, endPoint.position);
                // print(transform.right * 100f);
            }

        }
    }
}