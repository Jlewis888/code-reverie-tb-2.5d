using System.Diagnostics;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace CodeReverie
{
    public class DialogueBlackboardFieldBAK : BlackboardField
    {
        static readonly Texture2D k_ExposedIcon = Resources.Load<Texture2D>("GraphView/Nodes/BlackboardFieldExposed");
        static readonly string k_UxmlTemplatePath = "UXML/Blackboard/SGBlackboardField";
        static readonly string k_StyleSheetPath = "Styles/SGBlackboard";
        
        
        private bool isDragging = false;
        private VisualElement dragGhost;
        
        Pill m_Pill;
        
        public Texture icon
        {
            get { return m_Pill.icon; }
            set { m_Pill.icon = value; }
        }
        
        public DialogueBlackboardFieldBAK() 
        {
            VisualElement visualElement = (VisualElement) (EditorGUIUtility.Load("UXML/GraphView/BlackboardField.uxml") as VisualTreeAsset).Instantiate();
            m_Pill = visualElement.Q<Pill>("pill", (string) null);
            Assert.IsTrue(this.m_Pill != null);
            RegisterCallback<MouseDownEvent>(OnMouseDown);
            //RegisterCallback<MouseMoveEvent>(OnMouseMove);
            //RegisterCallback<MouseUpEvent>(OnMouseUp);
            RegisterCallback<DragUpdatedEvent>(OnDragUpdated);
            RegisterCallback<DragExitedEvent>(OnDragExited);
            RegisterCallback<DragPerformEvent>(OnDragPerformEvent);
            UpdateFromViewModel();
            Debug.Log("I am here");
        }
        
        internal void UpdateFromViewModel()
        {
            //this.text = ViewModel.inputName;
            Debug.Log(k_ExposedIcon);
            icon = k_ExposedIcon;
            //this.typeText = ViewModel.inputTypeName;
        }
        
        private void OnMouseDown(MouseDownEvent evt)
        {
            if (evt.currentTarget == this && evt.eventTypeId == MouseDownEvent.TypeId())
            {
                
            }

            Debug.Log("This is working here too");
            DragAndDrop.PrepareStartDrag();
            DragAndDrop.SetGenericData("BlackboardVariable", this);
            DragAndDrop.objectReferences = new Object[] { };
            Debug.Log(Event.current.type);
            if (Event.current.type == EventType.MouseDown)
            {
                Debug.Log("Start drag");
                DragAndDrop.StartDrag("Dragging Blackboard Variable");
            }
                
                
            evt.StopPropagation();
        }

        private void OnMouseMove(MouseMoveEvent evt)
        {
            Debug.Log("mosue is moving");
            evt.StopPropagation();
        }

        private void OnMouseUp(MouseUpEvent evt)
        {
            // Debug.Log(evt.currentTarget);
            // Debug.Log("Mouse up bitch");
            // // DragAndDrop.AcceptDrag();
            // // DragAndDrop.SetGenericData("BlackboardVariable", null);
            // evt.StopPropagation();
            
        }

        private void OnDragUpdated(DragUpdatedEvent e)
        {
            e.StopPropagation();
            Debug.Log("fhadsfjsahljfasjjk");
            
            //DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
            DragAndDrop.visualMode = DragAndDropVisualMode.Move;
        }


        void OnDragPerformEvent(DragPerformEvent evt)
        {
            Debug.Log("What is this doing");
        }
        
        private void OnDragExited(DragExitedEvent e)
        {
            Debug.Log("Testing this");
            Debug.Log(e.currentTarget);
            DragAndDrop.AcceptDrag();
            if (Event.current.type == EventType.MouseDown)
            {
                DragAndDrop.AcceptDrag();
            }
        }
        
    }
}