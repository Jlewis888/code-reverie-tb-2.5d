using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeReverie
{
    public class DialogueGraphView : GraphView
    {

        private SerializedObject _serializedObject;
        private DialogueEditorWindow _dialogueEditorWindow;
        private DialogueGraphAsset _dialogueGraphAsset;
        public DialogueEditorWindow dialogueEditorWindow => _dialogueEditorWindow;
        public List<DialogueEditorNode> dialogueEditorNodes;
        public Dictionary<string, DialogueEditorNode> nodeDictionary;
        private NodeSearchWindow _nodeSearchWindow;
        
        public DialogueGraphView(SerializedObject serializedObject, DialogueEditorWindow dialogueEditorWindow)
        {
            _serializedObject = serializedObject;
            _dialogueGraphAsset = (DialogueGraphAsset)serializedObject.targetObject;
            
            _dialogueEditorWindow = dialogueEditorWindow;

            dialogueEditorNodes = new List<DialogueEditorNode>();
            nodeDictionary = new Dictionary<string, DialogueEditorNode>();
            _nodeSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
            _nodeSearchWindow.dialogueGraph = this;

            this.nodeCreationRequest = ShowSearchWindow;
            
            styleSheets.Add(Resources.Load<StyleSheet>("NarrativeGraph"));
            
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

            graphViewChanged += OnGraphViewChangedEvent;

        }

        private GraphViewChange OnGraphViewChangedEvent(GraphViewChange graphviewchange)
        {

            if (graphviewchange.movedElements != null)
            {
                
                Undo.RecordObject(_serializedObject.targetObject, "Moved Elements");
                
                foreach (DialogueEditorNode dialogueEditorNode in graphviewchange.movedElements.OfType<DialogueEditorNode>().ToList())
                {
                    dialogueEditorNode.SavePosition();
                }
                
                
            }
            
            if (graphviewchange.elementsToRemove != null)
            {
                
                Undo.RecordObject(_serializedObject.targetObject, "Removed Nodes");
                List<DialogueEditorNode> nodes = graphviewchange.elementsToRemove.OfType<DialogueEditorNode>().ToList();


                if (nodes.Count > 0)
                {
                    //Undo.RecordObject(_serializedObject.targetObject, "Removed Node");
                    
                    for (int i = nodes.Count - 1; i >= 0; i--)
                    {
                        RemoveNode(nodes[i]);
                    }
                }
                
                
                
            }


            return graphviewchange;
        }

        private void RemoveNode(DialogueEditorNode node)
        {
            //Undo.RecordObject(_serializedObject.targetObject, "Removed Node");
            _dialogueGraphAsset.DialogueNodes.Remove(node.Node);
            nodeDictionary.Remove(node.Node.id);
            dialogueEditorNodes.Remove(node);
            _serializedObject.Update();

        }

        private void DrawNodes()
        {
            foreach (DialogueNode dialogueNode in _dialogueGraphAsset.DialogueNodes)
            {
                AddNodeToGraph(dialogueNode);
            }
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

        public void Add(DialogueNode node)
        {
            Undo.RecordObject(_serializedObject.targetObject, "Added Node");
            _dialogueGraphAsset.DialogueNodes.Add(node);
            _serializedObject.Update();
            AddNodeToGraph(node);

        }

        private void AddNodeToGraph(DialogueNode node)
        {
            node.typeName = node.GetType().AssemblyQualifiedName;
            DialogueEditorNode editorNode = new DialogueEditorNode(node);
            editorNode.SetPosition(node.position);
            
            dialogueEditorNodes.Add(editorNode);
            nodeDictionary.Add(node.id, editorNode);
            
            
            AddElement(editorNode);

        }
    }
}