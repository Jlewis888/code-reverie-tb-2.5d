using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    [RequireComponent(typeof(ScrollRect))]
    public class CommandMenuUIScrollToSelection : SerializedMonoBehaviour
    {
        private void Awake()
        {
            TargetScrollRect = GetComponent<ScrollRect>();
            ScrollWindow = TargetScrollRect.GetComponent<RectTransform>();
        }


        private void Update()
        {
            if (GameManager.Instance.playerInput.GetButtonDown("Navigate Up"))
            {
                Move(MoveDirection.Up);
                UpdateVerticalScrollPosition(MoveDirection.Up);
              
            }
            if (GameManager.Instance.playerInput.GetButtonDown("Navigate Down"))
            {
               
                Move(MoveDirection.Down);
                UpdateVerticalScrollPosition(MoveDirection.Down);
            }
        }


        protected RectTransform ScrollWindow { get; set; }
        protected ScrollRect TargetScrollRect { get; set; }
        
        protected RectTransform CurrentTargetRectTransform { get; set; }
        protected RectTransform LayoutListGroup
        {
            get { return TargetScrollRect != null ? TargetScrollRect.content : null; }
        }
        
        
        public void UpdateVerticalScrollPosition(MoveDirection direction)
        {

            RectTransform selection = EventSystem.current.currentSelectedGameObject.transform.GetComponent<RectTransform>();
            
            // move the current scroll rect to correct position
            float selectionPosition = -selection.anchoredPosition.y - (selection.rect.height * (1 - selection.pivot.y));
            
            
            
            
            float elementHeight = selection.rect.height;
            float maskHeight = ScrollWindow.rect.height;
            float listAnchorPosition = LayoutListGroup.anchoredPosition.y;

            // get the element offset value depending on the cursor move direction
            float offlimitsValue = GetScrollOffset(selectionPosition, listAnchorPosition, elementHeight, maskHeight);

            // move the target scroll rect


            if (direction == MoveDirection.Up)
            {
                TargetScrollRect.verticalNormalizedPosition -=
                    (offlimitsValue / LayoutListGroup.rect.height);
            }

            if (direction == MoveDirection.Down)
            {
                TargetScrollRect.verticalNormalizedPosition +=
                    (offlimitsValue / LayoutListGroup.rect.height);
            }
            
            
        }
        
        private float GetScrollOffset(float position, float listAnchorPosition, float targetLength, float maskLength)
        {
            if (position < listAnchorPosition + (targetLength / 2))
            {
                return (listAnchorPosition + maskLength) - (position - targetLength);
            }
            else if (position + targetLength > listAnchorPosition + maskLength)
            {
                return (listAnchorPosition + maskLength) - (position + targetLength);
            }

            return 0;
        }
        
        public void Move(MoveDirection direction)
        {
            AxisEventData data = new AxisEventData(EventSystem.current);
 
            data.moveDir = direction;
 
            data.selectedObject = EventSystem.current.currentSelectedGameObject;
 
            ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
        }
    }
}