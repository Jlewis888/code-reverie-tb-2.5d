using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CodeReverie
{
    public class MapBaseTileMap : SerializedMonoBehaviour
    {
        private void Awake()
        {
            if (ColorUtility.TryParseHtmlString("#C0B09D", out Color mapColor))
            {
                GetComponent<Tilemap>().color = mapColor;
            }

            GetComponent<TilemapRenderer>().sortingOrder = -3;
        }
    }
}