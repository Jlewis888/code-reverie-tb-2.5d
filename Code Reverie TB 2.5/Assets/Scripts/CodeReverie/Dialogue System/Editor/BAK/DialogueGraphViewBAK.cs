using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
//using Subtegral.DialogueSystem.DataContainers;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

namespace CodeReverie
{
    public class DialogueGraphViewBAK : GraphView
    {
        public readonly Vector2 DefaultNodeSize = new Vector2(200, 150);
        public readonly Vector2 DefaultCommentBlockSize = new Vector2(300, 200);
        public DialogueNodeBAK entryPointNodeBak;
        public Blackboard Blackboard = new Blackboard();
        
        public List<ExposedProperty> ExposedProperties { get; private set; } = new List<ExposedProperty>();
        private NodeSearchWindowBAK _searchWindowBak;
        private DialogueContainer _dialogueContainer;
        
        public DialogueGraphViewBAK(DialogueEditorWindowBAK editorWindowBak)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("NarrativeGraph"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());
            //Not here originally
            this.AddManipulator(new ClickSelector());
            
            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
            
            AddElement(GetEntryPointNodeInstance());

            AddSearchWindow(editorWindowBak);
            _dialogueContainer = editorWindowBak.GetDialogueContainer();
        }
        
        private void AddSearchWindow(DialogueEditorWindowBAK editorWindowBak)
        {
            _searchWindowBak = ScriptableObject.CreateInstance<NodeSearchWindowBAK>();
            _searchWindowBak.Configure(editorWindowBak, this);
            nodeCreationRequest = context =>
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindowBak);
        }
        
        public void ClearBlackBoardAndExposedProperties()
        {
            ExposedProperties.Clear();
            Blackboard.Clear();
        }
        
        public Group CreateCommentBlock(Rect rect, CommentBlockData commentBlockData = null)
        {
            
            //Undo.RegisterCompleteObjectUndo(_dialogueContainer, "Create Comment Block Node");
            
            if(commentBlockData==null)
                commentBlockData = new CommentBlockData();
            var group = new Group
            {
                autoUpdateGeometry = true,
                title = commentBlockData.Title
            };
            AddElement(group);
            group.SetPosition(rect);
            return group;
        }
        
        public void AddPropertyToBlackBoard(ExposedProperty property, bool loadMode = false)
        {
            var localPropertyName = property.PropertyName;
            var localPropertyValue = property.PropertyValue;
            if (!loadMode)
            {
                while (ExposedProperties.Any(x => x.PropertyName == localPropertyName))
                    localPropertyName = $"{localPropertyName}(1)";
            }

            var item = ExposedProperty.CreateInstance();
            item.PropertyName = localPropertyName;
            item.PropertyValue = localPropertyValue;
            ExposedProperties.Add(item);

            var container = new VisualElement();
            var field = new BlackboardField {text = localPropertyName, typeText = "string"};
            container.Add(field);

            var propertyValueTextField = new TextField("Value:")
            {
                value = localPropertyValue
            };
            propertyValueTextField.RegisterValueChangedCallback(evt =>
            {
                var index = ExposedProperties.FindIndex(x => x.PropertyName == item.PropertyName);
                ExposedProperties[index].PropertyValue = evt.newValue;
            });
            var sa = new BlackboardRow(field, propertyValueTextField);
            container.Add(sa);
            Blackboard.Add(container);
        }
        
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            var startPortView = startPort;

            ports.ForEach((port) =>
            {
                var portView = port;
                if (startPortView != portView && startPortView.node != portView.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }
        
        public void CreateNewDialogueNode(string nodeName, Vector2 position)
        {
            AddElement(CreateNode(nodeName, position));
        }
        
        public DialogueNodeBAK CreateNode(string nodeName, Vector2 position)
        {
            var tempDialogueNode = new DialogueNodeBAK()
            {
                title = "fhasfkjlasdhjfdasjhkfjlaks",
                DialogueText = nodeName,
                GUID = Guid.NewGuid().ToString()
            };
            tempDialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));
            var inputPort = GetPortInstance(tempDialogueNode, UnityEditor.Experimental.GraphView.Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            tempDialogueNode.inputContainer.Add(inputPort);
            tempDialogueNode.RefreshExpandedState();
            tempDialogueNode.RefreshPorts();
            tempDialogueNode.SetPosition(new Rect(position,
                DefaultNodeSize)); //To-Do: implement screen center instantiation positioning

            var textField = new TextField("Test");
            textField.AddToClassList("TextBox");
            textField.RegisterValueChangedCallback(evt =>
            {
                tempDialogueNode.DialogueText = evt.newValue;
                //tempDialogueNode.title = evt.newValue;
            });
            //textField.SetValueWithoutNotify(tempDialogueNode.title);
            tempDialogueNode.mainContainer.Add(textField);
            
            var choiceTextField = new TextField("Choice Text");
            tempDialogueNode.mainContainer.Add(choiceTextField);
            
            var responseTextField = new TextField("Response");
            tempDialogueNode.mainContainer.Add(responseTextField);
            
            
            

            var button = new Button(() => { AddChoicePort(tempDialogueNode); })
            {
                text = "Add Choice"
            };
            tempDialogueNode.titleButtonContainer.Add(button);
            return tempDialogueNode;
        }
        
        public void AddChoicePort(DialogueNodeBAK nodeBakCache, string overriddenPortName = "")
        {
            // var generatedPort = GetPortInstance(nodeCache, UnityEditor.Experimental.GraphView.Direction.Output);
            // var portLabel = generatedPort.contentContainer.Q<Label>("type");
            // generatedPort.contentContainer.Remove(portLabel);
            //
            // var outputPortCount = nodeCache.outputContainer.Query("connector").ToList().Count;
            // var outputPortName = string.IsNullOrEmpty(overriddenPortName)
            //     ? $"Option {outputPortCount + 1}"
            //     : overriddenPortName;
            //
            //
            // var textField = new TextField()
            // {
            //     name = string.Empty,
            //     value = outputPortName
            // };
            //  textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
            //  generatedPort.contentContainer.Add(new Label("  "));
            // generatedPort.contentContainer.Add(textField);
            // var deleteButton = new Button(() => RemovePort(nodeCache, generatedPort))
            // {
            //     text = "X"
            // };
            // //generatedPort.contentContainer.Add(deleteButton);
            // generatedPort.portName = $"Choice {outputPortName}";
            // nodeCache.outputContainer.Add(generatedPort);
            // nodeCache.RefreshPorts();
            // nodeCache.RefreshExpandedState();
            
            
            var generatedPort = GetPortInstance(nodeBakCache, UnityEditor.Experimental.GraphView.Direction.Output);
            var outputPortCount = nodeBakCache.outputContainer.Query("connector").ToList().Count;
            generatedPort.portName = $"Choice {outputPortCount}";
            
            nodeBakCache.outputContainer.Add(generatedPort);
            nodeBakCache.RefreshPorts();
            nodeBakCache.RefreshExpandedState();
        }
        
        private void RemovePort(Node node, Port socket)
        {
            var targetEdge = edges.ToList()
                .Where(x => x.output.portName == socket.portName && x.output.node == socket.node);
            if (targetEdge.Any())
            {
                var edge = targetEdge.First();
                edge.input.Disconnect(edge);
                RemoveElement(targetEdge.First());
            }

            node.outputContainer.Remove(socket);
            node.RefreshPorts();
            node.RefreshExpandedState();
        }
        
        private Port GetPortInstance(DialogueNodeBAK nodeBak, UnityEditor.Experimental.GraphView.Direction nodeDirection,
            Port.Capacity capacity = Port.Capacity.Single)
        {
            return nodeBak.InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }
        
        private DialogueNodeBAK GetEntryPointNodeInstance()
        {
            var nodeCache = new DialogueNodeBAK()
            {
                title = "START",
                GUID = Guid.NewGuid().ToString(),
                DialogueText = "ENTRYPOINT",
                EntyPoint = true
            };

            var generatedPort = GetPortInstance(nodeCache, UnityEditor.Experimental.GraphView.Direction.Output);
            generatedPort.portName = "Next";
            nodeCache.outputContainer.Add(generatedPort);

            //nodeCache.capabilities &= ~Capabilities.Movable;
            nodeCache.capabilities &= ~Capabilities.Deletable;

            nodeCache.RefreshExpandedState();
            nodeCache.RefreshPorts();
            nodeCache.SetPosition(new Rect(100, 200, 100, 150));
            return nodeCache;
        }
        
        // Override OnMouseUp to handle the drop event.
        // protected override void OnMouseUp(MouseUpEvent evt)
        // {
        //     base.OnMouseUp(evt);
        //
        //     // Check if something was dragged onto the graph.
        //     if (DragAndDrop.GetGenericData("draggedBlackboardField") is BlackboardField draggedField)
        //     {
        //         // Get the mouse position to place the node in the graph.
        //         Vector2 mousePosition = this.ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);
        //
        //         // Create a new node based on the dragged blackboard field.
        //         CreateNodeFromBlackboardField(draggedField, mousePosition);
        //     }
        // }
    }
}