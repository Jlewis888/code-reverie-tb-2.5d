using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;



namespace CodeReverie
{
    public class DialogueEditorWindowBAK : EditorWindow
    {
        private string _fileName = "New Dialogue";
        private DialogueGraphViewBAK _graphViewBak;
        private DialogueContainer _dialogueContainer;
        private Blackboard blackboard;
        
        
        // Right-hand display pannel vars
        private float panelWidth;
        private Rect panelRect;
        private GUIStyle panelStyle;
        private GUIStyle panelTitleStyle;
        private GUIStyle panelPropertyStyle;
        private Rect panelResizerRect;
        private GUIStyle resizerStyle;
        //private SelectableUI m_cachedSelectedObject;
        
        
        
        [MenuItem("Window/Dialogue Editor Graph")]
        public static void CreateGraphViewWindow()
        {
            var window = GetWindow<DialogueEditorWindowBAK>();
            window.titleContent = new GUIContent("Dialogue Editor Graph");
            //window.Show();
        }
        
        private void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
            GenerateMiniMap();
            GenerateBlackBoard();
            
        }

        // private void OnGUI()
        // {
        //     //DrawPanel();
        //     if (GUI.changed)
        //         Repaint();
        // }

        private void ConstructGraphView()
        {
            _graphViewBak = new DialogueGraphViewBAK(this)
            {
                name = "Narrative Graph",
            };
            _graphViewBak.StretchToParentSize();
            rootVisualElement.Add(_graphViewBak);
        }
        
        private void GenerateToolbar()
        {
            var toolbar = new Toolbar();

            var fileNameTextField = new TextField("File Name:");
            fileNameTextField.SetValueWithoutNotify(_fileName);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
            toolbar.Add(fileNameTextField);

            toolbar.Add(new Button(() => RequestDataOperation(true)) {text = "Save Data"});

            toolbar.Add(new Button(() => RequestDataOperation(false)) {text = "Load Data"});
            // toolbar.Add(new Button(() => _graphView.CreateNewDialogueNode("Dialogue Node")) {text = "New Node",});
            rootVisualElement.Add(toolbar);
        }
        
        private void RequestDataOperation(bool save)
        {
            if (!string.IsNullOrEmpty(_fileName))
            {
                var saveUtility = GraphSaveUtilityBAK.GetInstance(_graphViewBak);
                if (save)
                    saveUtility.SaveGraph(_fileName);
                else
                    saveUtility.LoadNarrative(_fileName);
            }
            else
            {
                EditorUtility.DisplayDialog("Invalid File name", "Please Enter a valid filename", "OK");
            }
        }
        
        private void GenerateMiniMap()
        {
            var miniMap = new MiniMap
            {
                anchored = false
            };
            var cords = _graphViewBak.contentViewContainer.WorldToLocal(new Vector2(this.maxSize.x - 10, 30));
            // // Set MiniMap position
            // miniMap.style.right = 10;  // Distance from the right edge of the GraphView
            // miniMap.style.top = 10;    // Distance from the top of the GraphView
            //miniMap.SetPosition(new Rect(cords.x, cords.y, 200, 140));
            
            miniMap.style.position = Position.Absolute;
            
            miniMap.style.top = 30;
            miniMap.style.right = 10; // 10 units from the right
            miniMap.style.height = 150;
            miniMap.style.width = 200;
            
            _graphViewBak.Add(miniMap);

            // Update and repaint if necessary
            miniMap.MarkDirtyRepaint();
            
        }
        
        private void GenerateBlackBoard()
        {
            blackboard = new Blackboard(_graphViewBak);
            blackboard.Add(new BlackboardSection());
            // DropdownField dropdown = CreateBlackboardDropdown();
            // blackboard.Add(dropdown);

            blackboard.addItemRequested += BlackboardAddItemRequested;
            // blackboard.addItemRequested = _blackboard =>
            // {
            //     Debug.Log("tet test tes");
            //     //_graphView.AddPropertyToBlackBoard(ExposedProperty.CreateInstance(), false);
            // };
            blackboard.editTextRequested = (_blackboard, element, newValue) =>
            {
                var oldPropertyName = ((BlackboardField) element).text;
                if (_graphViewBak.ExposedProperties.Any(x => x.PropertyName == newValue))
                {
                    EditorUtility.DisplayDialog("Error", "This property name already exists, please chose another one.",
                        "OK");
                    return;
                }

                var targetIndex = _graphViewBak.ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
                _graphViewBak.ExposedProperties[targetIndex].PropertyName = newValue;
                ((BlackboardField) element).text = newValue;
            };
            blackboard.SetPosition(new Rect(10,30,200,300));
            _graphViewBak.Add(blackboard);
            _graphViewBak.Blackboard = blackboard;
        }
        
        private DropdownField CreateBlackboardDropdown()
        {
            // Create the dropdown field with a list of options.
            var dropdownOptions = new List<string> { "Option 1", "Option 2", "Option 3" };
            DropdownField dropdown = new DropdownField("Select Property Type", dropdownOptions, 0);

            // Customize the dropdown's appearance and behavior.
            dropdown.RegisterValueChangedCallback(evt =>
            {
                Debug.Log($"Selected option: {evt.newValue}");
            });

            return dropdown;
        }
        
        private void BlackboardAddItemRequested(Blackboard blackboard)
        {
            // Show the dropdown when the "Add" button in the Blackboard is clicked.
            ShowDropdownMenu();
        }
        
        private void ShowDropdownMenu()
        {
            // Create a dropdown menu with various property types.
            GenericMenu dropdownMenu = new GenericMenu();
        
            dropdownMenu.AddItem(new GUIContent("Int"), false, () => AddBlackboardProperty("Int"));
            dropdownMenu.AddItem(new GUIContent("Float"), false, () => AddBlackboardProperty("Float"));
            dropdownMenu.AddItem(new GUIContent("String"), false, () => AddBlackboardProperty("String"));
            dropdownMenu.AddItem(new GUIContent("Bool"), false, () => AddBlackboardProperty("Bool"));
        
            // Show the dropdown menu at the mouse position.
            dropdownMenu.ShowAsContext();
        }
        
        private void AddBlackboardProperty(string propertyType)
        {
            // Create a new BlackboardField based on the selected dropdown option.
            var field = new DialogueBlackboardFieldBAK
            {
                text = $"New {propertyType} Property",
                typeText = propertyType,
                userData = propertyType // Store the property type as user data.
            };

            // Create a property view for the BlackboardRow.
            var propertyView = new BlackboardFieldPropertyViewBAK(propertyType);
            
            
            // Add drag support to the blackboard field.
            // field.RegisterCallback<MouseDownEvent>(evt =>
            // {
            //     if (evt.button == 0) // Left-click to start dragging.
            //     {
            //         Debug.Log("This is working here");
            //         DragAndDrop.PrepareStartDrag();
            //         DragAndDrop.SetGenericData("draggedBlackboardField", field);
            //         DragAndDrop.StartDrag(field.text);
            //     }
            // });

            // Add the new BlackboardField as a BlackboardRow.
            var row = new BlackboardRow(field, propertyView);
            blackboard.Add(row);
        }
        
        private void OnDisable()
        {
            rootVisualElement.Remove(_graphViewBak);
        }

        public DialogueContainer GetDialogueContainer()
        {
            return _dialogueContainer;
        }
        

        private Vector2 panelVerticalScroll;
        
        void DrawPanel()
        {
            const int VERTICAL_GAP = 20;
            const int VERTICAL_PADDING = 10;

            panelRect = new Rect(position.width - panelWidth, 17, panelWidth, position.height - 17);
            //Debug.Log(panelRect);
            
            // if (panelStyle.normal.background == null)
            //     InitGUIStyles();
            GUILayout.BeginArea(panelRect, panelStyle);
            // GUILayout.BeginVertical();
            // panelVerticalScroll = GUILayout.BeginScrollView(panelVerticalScroll);
            //
            // GUI.SetNextControlName("CONTROL_TITLE");
            //
            // GUILayout.Space(10);
            //
            // GUILayout.Label("Conversation: " , panelTitleStyle);
            // GUILayout.Space(VERTICAL_GAP);
           
            // GUILayout.BeginHorizontal();
            // if (GUILayout.Button("Add bool"))
            // {
            //     //string newname = GetValidParamName("New bool");
            //     //CurrentAsset.ParameterList.Add(new EditableBoolParameter(newname));
            // }
            // if (GUILayout.Button("Add int"))
            // {
            //     //string newname = GetValidParamName("New int");
            //    // CurrentAsset.ParameterList.Add(new EditableIntParameter(newname));
            // }
            // GUILayout.EndHorizontal();
            
            // GUILayout.EndScrollView();
            // GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        
        private void DrawGrid(int gridSpacing, Color gridColor)
        {
            // Begin a clipping region for the grid (ensures drawing is only within this area)
            Handles.BeginGUI();
            Handles.color = gridColor;

            // Draw vertical lines
            for (float x = 0; x < position.width; x += gridSpacing)
            {
                Handles.DrawLine(new Vector3(x, 0, 0), new Vector3(x, position.height, 0));
            }

            // Draw horizontal lines
            for (float y = 0; y < position.height; y += gridSpacing)
            {
                Handles.DrawLine(new Vector3(0, y, 0), new Vector3(position.width, y, 0));
            }

            Handles.color = Color.white; // Reset color back to white
            Handles.EndGUI();
        }
        
    }
}