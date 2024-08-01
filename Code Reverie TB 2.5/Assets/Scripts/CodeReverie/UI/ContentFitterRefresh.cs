using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ContentFitterRefresh : SerializedMonoBehaviour
    {
        private void Awake()
        {
            RefreshContentFitters();
        }


        public void RefreshContentFitters()
        {
            var rectTransform = (RectTransform)transform;
            RefreshContentFitter(rectTransform);
        }
 
        private void RefreshContentFitter(RectTransform transform)
        {
            //Debug.Log("RefreshContentFitter");
            if (transform == null || !transform.gameObject.activeSelf)
            {
                return;
            }
   
            foreach (RectTransform child in transform)
            {
                RefreshContentFitter(child);
            }
 
            var layoutGroup = transform.GetComponent<LayoutGroup>();
            var contentSizeFitter = transform.GetComponent<ContentSizeFitter>();
            if (layoutGroup != null)
            {
                layoutGroup.SetLayoutHorizontal();
                layoutGroup.SetLayoutVertical();
            }
 
            if (contentSizeFitter != null)
            {
                
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
            }
        }
    }
}