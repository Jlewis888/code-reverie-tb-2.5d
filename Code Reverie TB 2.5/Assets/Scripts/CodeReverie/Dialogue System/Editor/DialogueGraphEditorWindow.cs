using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CodeReverie
{
    public class DialogueGraphEditorWindow : EditorWindow
    {
        [SerializeField]
        private DialogueGraphAsset _dialogueGraphAsset;
        public DialogueGraphAsset currentDialogueGraphAsset => _dialogueGraphAsset;

        [SerializeField]
        private DialogueGraphView _dialogueGraphView;
        private SerializedObject _serializedObject;
        


        public static void Open(DialogueGraphAsset targetAsset)
        {
            DialogueGraphEditorWindow[] windows = Resources.FindObjectsOfTypeAll<DialogueGraphEditorWindow>();

            foreach (var window in windows)
            {
                if (window.currentDialogueGraphAsset == targetAsset)
                {
                    window.Focus();
                    return;
                }
            }

            DialogueGraphEditorWindow dialogueGraphEditorWindow =
                CreateWindow<DialogueGraphEditorWindow>(typeof(DialogueGraphEditorWindow), typeof(SceneView));

            
            var icon = (Texture2D)EditorGUIUtility.ObjectContent(targetAsset, targetAsset.GetType()).image;

            // // Change the blank file icon to a Unity-logo file icon
            // if (icon == EditorGUIUtility.FindTexture("d_DefaultAsset Icon"))
            // {
            //     
            //     icon = EditorGUIUtility.FindTexture("ScriptableObject Icon");
            // }
            // Debug.Log(icon);
            // Debug.Log(targetAsset.GetType());

            dialogueGraphEditorWindow.titleContent = new GUIContent($"{targetAsset.name}", icon);
            dialogueGraphEditorWindow.Load(targetAsset);

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