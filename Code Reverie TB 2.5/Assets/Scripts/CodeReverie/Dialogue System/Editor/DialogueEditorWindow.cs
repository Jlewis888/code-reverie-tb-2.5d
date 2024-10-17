using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeReverie
{
    public class DialogueEditorWindow : EditorWindow
    {
        [SerializeField]
        private DialogueGraphAsset _dialogueGraphAsset;
        public DialogueGraphAsset currentDialogueGraphAsset => _dialogueGraphAsset;

        [SerializeField]
        private DialogueGraphView _dialogueGraphView;
        private SerializedObject _serializedObject;
        


        public static void Open(DialogueGraphAsset targetAsset)
        {
            DialogueEditorWindow[] windows = Resources.FindObjectsOfTypeAll<DialogueEditorWindow>();

            foreach (var window in windows)
            {
                if (window.currentDialogueGraphAsset == targetAsset)
                {
                    window.Focus();
                    return;
                }
            }

            DialogueEditorWindow dialogueEditorWindow =
                CreateWindow<DialogueEditorWindow>(typeof(DialogueEditorWindow), typeof(SceneView));


            dialogueEditorWindow.titleContent = new GUIContent($"{targetAsset.name}", EditorGUIUtility.ObjectContent(null, typeof(DialogueGraphAsset)).image);
            dialogueEditorWindow.Load(targetAsset);

        }

        private void OnEnable()
        {
            if (_dialogueGraphAsset != null)
            {
                DrawGraph();
            }
        }

        private void OnGUI()
        {
            if (_dialogueGraphAsset != null)
            {

                if (EditorUtility.IsDirty(_dialogueGraphAsset))
                {
                    hasUnsavedChanges = true;
                }
                else
                {
                    hasUnsavedChanges = false;
                }
                
                
            }
        }

        private void Load(DialogueGraphAsset targetAsset)
        {
            _dialogueGraphAsset = targetAsset;
            DrawGraph();
        }

        private void DrawGraph()
        {
            _serializedObject = new SerializedObject(_dialogueGraphAsset);
            _dialogueGraphView = new DialogueGraphView(_serializedObject, this);
            _dialogueGraphView.graphViewChanged += OnChange;
            rootVisualElement.Add(_dialogueGraphView);
        }

        private GraphViewChange OnChange(GraphViewChange graphviewchange)
        {
            hasUnsavedChanges = true;
            EditorUtility.SetDirty(_dialogueGraphAsset);
            return graphviewchange;
        }

       
        
    }
}