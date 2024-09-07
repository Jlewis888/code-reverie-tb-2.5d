using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class GraphicsSettingsMenuManager : SerializedMonoBehaviour
    {
        public FullScreenMode fullScreenMode;
        public int resolutionWidth;
        public int resolutionHeight;
        public int targetFrameRate;
        
        private void OnEnable()
        {
            //Display Modes: Windowed, Borderless Full Screen, Full Screen
            
            //Resolutions: 
            
            
            
            //Screen.SetResolution(640, 480, FullScreenMode.Windowed);
            //Application.targetFrameRate = targetFrameRate;
            //Application.SetTargetFrameRate(60);
        }
        
        private void Start()
        {
           
        }

        
        
    }
}