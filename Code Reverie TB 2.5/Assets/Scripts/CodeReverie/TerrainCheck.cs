using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class TerrainCheck : SerializedMonoBehaviour
    {
        public float groundDistance;
        public LayerMask currentLayer;
        public LayerMask terrainLayer;
        public LayerMask waterLayer;

        private void Update()
        {
            CheckTerrain();
        }

        public void CheckTerrain()
        {
            RaycastHit hit;

            Vector3 castPos = transform.position;
            castPos.y += 1;

            if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.transform);
                    Vector3 movePos = transform.position;
                    movePos.y = hit.point.y + groundDistance;
                    transform.position = movePos;
                }
            }

        }
        
    }
}