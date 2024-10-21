using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

namespace CodeReverie
{
    public class DialogueGraphEditorNode : Node
    {

        private DialogueGraphNode _dialogueGraphNode;
        public DialogueGraphNode graphNode => _dialogueGraphNode;

        private Port _outputPort;
        private List<Port> _ports;
        public List<Port> Ports => _ports;

        private SerializedProperty serializedProperty;
        private SerializedObject serializedObject;
        
        public DialogueGraphEditorNode(DialogueGraphNode graphNode, SerializedObject dialogueGraphAsset)
        {
            AddToClassList("dialogue-graph-node");

            _dialogueGraphNode = graphNode;
            serializedObject = dialogueGraphAsset;

            Type typeInfo = graphNode.GetType();

            NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

            title = info.Name;

            _ports = new List<Port>();
            
            string[] depths = info.Category.Split('/');

            foreach (string depth in depths)
            {
                AddToClassList(depth.ToLower().Replace(' ', '-'));
            }

            name = typeInfo.Name;
            
            if (info.HasFlowOutput)
            {

                Port.Capacity capacity = info.HasMultiOutput ? Port.Capacity.Multi : Port.Capacity.Single;
                
                CreateOutputPort(capacity);
                
            }

            if (info.HasFlowInput)
            {
                CreateInputPort();
            }

            foreach (FieldInfo property in typeInfo.GetFields())
            {
                if (property.GetCustomAttribute<ExposedPropertyAttribute>() is ExposedPropertyAttribute exposedProperty)
                {
                    PropertyField field = DrawProperty(property.Name);
                    
                    //field.RegisterValueChangeCallback(OnFieldChangeCallback);
                }
            }
            
            RefreshExpandedState();
            
        }

        private void OnFieldChangeCallback(SerializedPropertyChangeEvent evt)
        {
            
        }

        private PropertyField DrawProperty(string propertyName)
        {
            if (serializedProperty == null)
            {
                FetchSerializedProperty();
            }

            SerializedProperty prop = serializedProperty.FindPropertyRelative(propertyName);
            PropertyField field = new PropertyField(prop);

            field.bindingPath = prop.propertyPath;
            
            extensionContainer.Add(field);

            return field;
        }

        private void FetchSerializedProperty()
        {
            SerializedProperty nodes = serializedObject.FindProperty("dialogueNodes");

            if (nodes.isArray)
            {
                int size = nodes.arraySize;

                for (int i = 0; i < size; i++)
                {
                    var element = nodes.GetArrayElementAtIndex(i);
                    var elementId = element.FindPropertyRelative("GUID");
                    if (elementId.stringValue == _dialogueGraphNode.id)
                    {
                        serializedProperty = element;
                    }
                }
            }
        }

        private void CreateInputPort()
        {
            Port inputPort = InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Input,
                Port.Capacity.Single, typeof(PortTypes.Port));
            inputPort.portName = "In";
            inputPort.tooltip = "The input";
            _ports.Add(inputPort);
            inputContainer.Add(inputPort);
        }
        

        private void CreateOutputPort(Port.Capacity capacity)
        {
            _outputPort = InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Output, capacity, typeof(PortTypes.Port));
            _outputPort.portName = "Out";
            _outputPort.tooltip = "The output";
            _ports.Add(_outputPort);
            outputContainer.Add(_outputPort);
        }

        

        public void SavePosition()
        {
            _dialogueGraphNode.SetPosition(GetPosition());
        }
    }
}