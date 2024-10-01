using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class CommandWheelPanel : SerializedMonoBehaviour
    {
        public GameObject commandWheelContainer;
        public List<CommandWheel> commandWheels = new List<CommandWheel>();
        public CommandWheel commandWheelPF;
        public CommandWheel activeCommandWheel;


        private void OnEnable()
        {
            activeCommandWheel = GetComponentInChildren<CommandWheel>();

            if (activeCommandWheel != null)
            {
                activeCommandWheel.active = true;
            }
            
            
            InitExtensions();
        }
        
        public void InitExtensions()
        {
            foreach (CommandWheelPanelExtension commandWheelExtension in GetComponents<CommandWheelPanelExtension>())
            {
                commandWheelExtension.InitExtension();
            }
        }
    }
}