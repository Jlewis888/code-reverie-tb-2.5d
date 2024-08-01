using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    
    
    
    
 
    [CustomEditor(typeof(EditorScreenShots))]
    public class ScreenshotEditor : Editor
    {
        public string textureName = "Minimap_";
        public string path = "Assets/Textures/Minimap/";
        static int counter;
 
        public override void OnInspectorGUI()
        {
            textureName = EditorGUILayout.TextField("Name:", textureName);
            path = EditorGUILayout.TextField("Path:", path);
            if(GUILayout.Button("Capture"))
            {
                Screenshot();
            }
        }
 
        //[MenuItem("Screenshot/Take screenshot")]
        void Screenshot()
        {
            ScreenCapture.CaptureScreenshot(path + textureName + counter+ ".png");
            counter++;
        }
    }
}