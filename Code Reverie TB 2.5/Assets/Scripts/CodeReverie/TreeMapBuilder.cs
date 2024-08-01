using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [ExecuteInEditMode]
    public class TreeMapBuilder : SerializedMonoBehaviour
    {
        public GameObject treePF;
        [Range(0, 1000)] public int count;
        
        [Button("Spawn Trees")]
        public void SpawnTrees()
        {
            
            Clear();
            
            Collider2D collider2D = GetComponent<Collider2D>();
            print(collider2D);

            for (int i = 0; i < count; i++)
            {
                GameObject tree = Instantiate(treePF, transform);

                float randomX = Random.Range(-collider2D.bounds.extents.x, collider2D.bounds.extents.x);
                float randomY = Random.Range(-collider2D.bounds.extents.y, collider2D.bounds.extents.y);

                tree.transform.position = collider2D.bounds.center + new Vector3(randomX, randomY);
            }
            
            
            
        }

        [Button("Clear")]
        public void Clear()
        {
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }

            if (transform.childCount > 0)
            {
                Clear();
            }
        }
        
        
    }
}