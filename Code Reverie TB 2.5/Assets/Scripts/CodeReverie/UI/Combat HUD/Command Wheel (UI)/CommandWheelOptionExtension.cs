using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CommandWheelOptionExtension : SerializedMonoBehaviour
    {
        public CommandWheelOption commandWheelOption;
        
        private void Awake()
        {
            commandWheelOption = GetComponent<CommandWheelOption>();
        }
    }
}