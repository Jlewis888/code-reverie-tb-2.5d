using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class DraggableItem: SerializedMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image image;
        public Transform parentAfterDrag;

        private void Awake()
        {
            image = GetComponent<Image>();
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            //CanvasManager.Instance.itemMouseFollow.itemImage.gameObject.SetActive(true);
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;

        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            transform.SetParent(CanvasManager.Instance.transform);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            
            GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }
}