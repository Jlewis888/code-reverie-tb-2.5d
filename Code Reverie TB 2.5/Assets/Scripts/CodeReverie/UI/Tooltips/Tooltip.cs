using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class Tooltip : SerializedMonoBehaviour
    {
        public Canvas canvas;
        public GameObject header;
        public GameObject body;
        public TMP_Text toolTipTitleText;
        public TMP_Text itemText;
        public TMP_Text description;
        public RectTransform backgroundRectTransform;
        public RectTransform rectTransform;
        public Direction pivotDirection;
        public float toolTipOffset;
        
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            CloseToolTip();
         

        }

        private void OnEnable()
        {
            // if (TryGetComponent(out ContentFitterRefresh contentFitterRefresh))
            // {
            //     contentFitterRefresh.RefreshContentFitters();
            //     contentFitterRefresh.RefreshContentFitters();
            // }
        }

        private void OnDisable()
        {
            description.text = "Placeholder text";
        }


        private void Update()
        {
            SetToolTipPivot();
            Vector2 anchoredPosition;

            if (GameManager.Instance.currentControlScheme == ControlSchemeType.KeyboardMouse)
            {
                //toolTipOffset = 35f;
                anchoredPosition =  Input.mousePosition / canvas.GetComponent<RectTransform>().localScale.x;
                
                if (pivotDirection == Direction.BottomLeft)
                {
                    anchoredPosition.x += toolTipOffset;
                }
                else
                {
                    anchoredPosition.x -= toolTipOffset;
                }
                
            }
            else
            {
                toolTipOffset = 70f;
                
                //anchoredPosition = CanvasManager.Instance.virtualMouseObject.transform.position / canvas.GetComponent<RectTransform>().localScale.x;
                anchoredPosition =  Input.mousePosition / canvas.GetComponent<RectTransform>().localScale.x;
                
                if (pivotDirection == Direction.BottomLeft)
                {
                    anchoredPosition.x += toolTipOffset;
                }
                else
                {
                    anchoredPosition.x -= toolTipOffset;
                }
            }
            
            
            Vector2 clamped = anchoredPosition;

            float minWidth;
            float maxWidth;

            if (rectTransform.pivot.Equals(new Vector2(0, 0)))
            {
                minWidth = -rectTransform.rect.width;
                maxWidth = backgroundRectTransform.rect.width - rectTransform.rect.width;
            }
            else
            {
                minWidth = rectTransform.rect.width;
                maxWidth = backgroundRectTransform.rect.width;
            }
            
            
            clamped.x = Mathf.Clamp(clamped.x, minWidth, maxWidth);
            clamped.y = Mathf.Clamp(clamped.y, rectTransform.rect.height / 2, backgroundRectTransform.rect.height - rectTransform.rect.height);
           
            rectTransform.anchoredPosition = clamped;
           
        }

        public void SetToolTipData(TooltipData tooltipData)
        {
            toolTipTitleText.text = tooltipData.name;
            description.text = tooltipData.description;
            description.fontSize = 24;

            if (tooltipData.headerColor != null)
            {
                header.GetComponent<Image>().color = tooltipData.headerColor;
            }
            
        }
        
        
        public virtual void OpenToolTip()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void CloseToolTip()
        {
            gameObject.SetActive(false);
        }
        
        public void SetToolTipPivot()
        {

            Vector3 viewportCoord;

            if (GameManager.Instance.currentControlScheme == ControlSchemeType.KeyboardMouse)
            {
                viewportCoord = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            else
            {
                //viewportCoord = Camera.main.ScreenToViewportPoint(CanvasManager.Instance.virtualMouseObject.transform.position);
                viewportCoord = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }


            if (viewportCoord.x < 0.5) {
                rectTransform.pivot = new Vector2(0, 0);
                pivotDirection = Direction.BottomLeft;
            }
            if (viewportCoord.x > 0.5) {
                rectTransform.pivot = new Vector2(1, 0);
                pivotDirection = Direction.BottomRight;
            }
        }
        
    }
}