using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeReverie
{
    public class DialogueGraphView : GraphView
    {

        private SerializedObject _serializedObject;
        private DialogueGraphEditorWindow _dialogueGraphEditorWindow;
        private DialogueGraphAsset _dialogueGraphAsset;
        public DialogueGraphEditorWindow dialogueGraphEditorWindow => _dialogueGraphEditorWindow;
        public List<DialogueGraphEditorNode> dialogueEditorNodes;
        public Dictionary<string, DialogueGraphEditorNode> nodeDictionary;
        public Dictionary<Edge, DialogueGraphConnection> connectionDictionary;
        
        
        private NodeSearchWindow _nodeSearchWindow;
        
        public DialogueGraphView(SerializedObject serializedObject, DialogueGraphEditorWindow dialogueGraphEditorWindow)
        {
            _serializedObject = serializedObject;
            _dialogueGraphAsset = (DialogueGraphAsset)serializedObject.targetObject;
            connectionDictionary = new Dictionary<Edge, DialogueGraphConnection>();
            
            _dialogueGraphEditorWindow = dialogueGraphEditorWindow;

            dialogueEditorNodes = new List<DialogueGraphEditorNode>();
            nodeDictionary = new Dictionary<string, DialogueGraphEditorNode>();
            _nodeSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
            _nodeSearchWindow.dialogueGraph = this;

            this.nodeCreationRequest = ShowSearchWindow;
            
            styleSheets.Add(Resources.Load<StyleSheet>("NarrativeGraph"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());
            //Not here originally
            this.AddManipulator(new ClickSelector());
            
            var grid = new GridBackground();
            grid.name = "Grid";
            Insert(0, grid);
            grid.StretchToParentSize();
            
            Add(grid);
            grid.SendToBack();
            //AddSearchWindow(dialogueEditorWindow);

            DrawNodes();
            DrawConnections();

            graphViewChanged += OnGraphViewChangedEvent;

        }

        

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            var startPortView = startPort;
            ports.ForEach(port =>
            {
                var portView = port;
                if (startPortView != portView && startPortView.node != portView.node)
                    compatiblePorts.Add(port);
            });
            
            return compatiblePorts;
        }


        private GraphViewChange OnGraphViewChangedEvent(GraphViewChange graphviewchange)
        {

            if (graphviewchange.movedElements != null)
            {
                
                Undo.RecordObject(_serializedObject.targetObject, "Moved Elements");
                
                foreach (DialogueGraphEditorNode dialogueEditorNode in graphviewchange.movedElements.OfType<DialogueGraphEditorNode>().ToList())
                {
                    dialogueEditorNode.SavePosition();
                }
                
                
            }
            
            if (graphviewchange.elementsToRemove != null)
            {
                
                Undo.RecordObject(_serializedObject.targetObject, "Removed Stuff");
                List<DialogueGraphEditorNode> nodes = graphviewchange.elementsToRemove.OfType<DialogueGraphEditorNode>().ToList();


                if (nodes.Count > 0)
                {
                    //Undo.RecordObject(_serializedObject.targetObject, "Removed Node");
                    
                    for (int i = nodes.Count - 1; i >= 0; i--)
                    {
                        RemoveNode(nodes[i]);
                    }
                }

                foreach (Edge edge in graphviewchange.elementsToRemove.OfType<Edge>())
                {
                    RemoveConnection(edge);
                }
                
            }

            if (graphviewchange.edgesToCreate != null)
            {
                Undo.RecordObject(_serializedObject.targetObject, "Add Connection");
                foreach (Edge edge in graphviewchange.edgesToCreate)
                {
                    CreateEdge(edge);
                }
            }


            return graphviewchange;
        }

        private void CreateEdge(Edge edge)
        {
            DialogueGraphEditorNode dialogueGraphEditorNodeInput = (DialogueGraphEditorNode)edge.input.node;

            int inputIndex = dialogueGraphEditorNodeInput.Ports.IndexOf(edge.input);
            
            DialogueGraphEditorNode dialogueGraphEditorNodeOutput = (DialogueGraphEditorNode)edge.output.node;
            
            int outputIndex = dialogueGraphEditorNodeOutput.Ports.IndexOf(edge.output);

            DialogueGraphConnection graphConnection = new DialogueGraphConnection(dialogueGraphEditorNodeInput.graphNode.id, inputIndex,
                dialogueGraphEditorNodeOutput.graphNode.id, outputIndex);
            
            _dialogueGraphAsset.Connections.Add(graphConnection);
            
        }

        private void RemoveNode(DialogueGraphEditorNode node)
        {
            //Undo.RecordObject(_serializedObject.targetObject, "Removed Node");
            _dialogueGraphAsset.DialogueNodes.Remove(node.graphNode);
            nodeDictionary.Remove(node.graphNode.id);
            dialogueEditorNodes.Remove(node);
            _serializedObject.Update();

        }

        private void DrawNodes()
        {
            foreach (DialogueGraphNode dialogueNode in _dialogueGraphAsset.DialogueNodes)
            {
                AddNodeToGraph(dialogueNode);
            }
            
            Bind();
        }
        
        private void DrawConnections()
        {
            if (_dialogueGraphAsset.Connections == null)
            {
                return;
            }

            foreach (DialogueGraphConnection connection in _dialogueGraphAsset.Connections)
            {
                DrawConnection(connection);
            }
        }

        private void DrawConnection(DialogueGraphConnection graphConnection)
        {
            DialogueGraphEditorNode inputNode = GetNode(graphConnection.inputPort.nodeId);
            DialogueGraphEditorNode outputNode = GetNode(graphConnection.outputPort.nodeId);
            if (inputNode == null)
            {
                return;
            }
            
            if (outputNode == null)
            {
                return;
            }

            Port inPort = inputNode.Ports[graphConnection.inputPort.portIndex];
            Port outPort = outputNode.Ports[graphConnection.outputPort.portIndex];

            Edge edge = inPort.ConnectTo(outPort);
            AddElement(edge);
            connectionDictionary.Add(edge, graphConnection);
        }

        private void RemoveConnection(Edge edge)
        {
            Debug.Log("Remove Connection");
            if (connectionDictionary.TryGetValue(edge, out DialogueGraphConnection connection))
            {
                Debug.Log("Remove Connection Here");
                
                _dialogueGraphAsset.Connections.Remove(connection);
                connectionDictionary.Remove(edge);
            }
            
        }

        private DialogueGraphEditorNode GetNode(string nodeId)
        {
            DialogueGraphEditorNode node = null;

            nodeDictionary.TryGetValue(nodeId, out node);

            return node;
        }

        private void ShowSearchWindow(NodeCreationContext obj)
        {
            _nodeSearchWindow.target = (VisualElement)focusController.focusedElement;
            SearchWindow.Open(new SearchWindowContext(obj.screenMousePosition), _nodeSearchWindow);
        }

        // private void AddSearchWindow(DialogueEditorWindow dialogueEditorWindow1)
        // {
        //     _nodeSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        //     _nodeSearchWindow.Configure(editorWindowBak, this);
        //     
        //     nodeCreationRequest = context =>
        //         SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindowBak);
        // }

        public void Add(DialogueGraphNode graphNode)
        {
            Undo.RecordObject(_serializedObject.targetObject, "Added Node");
            _dialogueGraphAsset.DialogueNodes.Add(graphNode);
            _serializedObject.Update();
            AddNodeToGraph(graphNode);
            Bind();

        }

        private void AddNodeToGraph(DialogueGraphNode graphNode)
        {
            graphNode.typeName = graphNode.GetType().AssemblyQualifiedName;
            DialogueGraphEditorNode graphEditorNode = new DialogueGraphEditorNode(graphNode, _serializedObject);
            graphEditorNode.SetPosition(graphNode.position);
            
            dialogueEditorNodes.Add(graphEditorNode);
            nodeDictionary.Add(graphNode.id, graphEditorNode);
            
            
            AddElement(graphEditorNode);

        }

        private void Bind()
        {
            _serializedObject.Update();
            this.Bind(_serializedObject);
        }
    }
}