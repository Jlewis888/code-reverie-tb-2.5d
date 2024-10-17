using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace CodeReverie
{
    [CustomEditor(typeof(DialogueGraphAsset))]
    public class DialogueGraphAssetEditor : Editor
    {
        
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int index)
        {

            Object asset = EditorUtility.InstanceIDToObject(instanceID);

            if (asset.GetType() == typeof(DialogueGraphAsset))
            {
                DialogueEditorWindow.Open((DialogueGraphAsset)asset);
                return true;
            }
            
            return false;
        }
        
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open"))
            {
                DialogueEditorWindow.Open((DialogueGraphAsset)target);
            }
        }
    }
}