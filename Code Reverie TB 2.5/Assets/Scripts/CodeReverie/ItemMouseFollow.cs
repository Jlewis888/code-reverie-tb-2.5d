using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ItemMouseFollow : SerializedMonoBehaviour
    {
        public Canvas canvas;
        public RectTransform canvasRect;
        public Vector2 position;
        public Image itemImage;

        private void Awake()
        {
            canvasRect = canvas.GetComponent<RectTransform>();
        }

        private void Update()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition,
                canvas.worldCamera, out position);


            transform.position = canvas.transform.TransformPoint(position);
            
            
            if (!Input.GetMouseButton(0))
            {
                itemImage.gameObject.SetActive(false);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
               // gameObject.SetActive(false);
            }

        }
    }
}