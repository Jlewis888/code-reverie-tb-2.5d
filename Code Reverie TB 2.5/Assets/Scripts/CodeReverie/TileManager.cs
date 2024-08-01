using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class TileManager : SerializedMonoBehaviour
    {
        public GameObject tilePF;

        public Grid grid;


        private void Start()
        {
            var worldPosition = grid.GetCellCenterWorld(new Vector3Int());

            Instantiate(tilePF, worldPosition, Quaternion.identity);
        }
    }
}