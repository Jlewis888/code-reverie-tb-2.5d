using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectExtension : SerializedMonoBehaviour
    {

        public ScrollRect targetScrollRect;
        public RectTransform scrollWindow;
        public RectTransform contentPanel;
        protected ScrollRect TargetScrollRect { get; set; }
        protected RectTransform ScrollWindow { get; set; }
        
        private void Awake()
        {
            
            
            TargetScrollRect = GetComponent<ScrollRect>();
            targetScrollRect = GetComponent<ScrollRect>();

            contentPanel = targetScrollRect.content;
            
            ScrollWindow = TargetScrollRect.GetComponent<RectTransform>();
            scrollWindow = TargetScrollRect.GetComponent<RectTransform>();
        }
        
        
        // private void Update()
        // {
        //     TargetScrollRect.verticalNormalizedPosition
        // }
        
        public void SnapTo(RectTransform target)
        {
            Canvas.ForceUpdateCanvases();

            if (contentPanel != null)
            {
                contentPanel.anchoredPosition =
                    (Vector2)TargetScrollRect.transform.InverseTransformPoint(contentPanel.position)
                    - (Vector2)TargetScrollRect.transform.InverseTransformPoint(target.position);

                contentPanel.anchoredPosition = new Vector2(0, contentPanel.anchoredPosition.y);
            }
            
            
        }
    }
}