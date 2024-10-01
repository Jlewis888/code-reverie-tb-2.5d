using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeReverie
{
    public class CommandWheel : SerializedMonoBehaviour
    {
        public TMP_Text optionText;
        public List<CommandWheelOption> commandWheelOptions = new List<CommandWheelOption>();
        [FormerlySerializedAs("selectedRadialMenuOption")] public CommandWheelOption selectedCommandWheelOption;
        public Vector2 inputDirection;
        public float angleOffset = -90f; // Adjust to rotate the menu orientation (set -90 for "up" to be the first slice)
        public int currentIndex = 0;
        public CommandWheelToolTip commandWheelToolTip;
        public bool active;
        
        private void Awake()
        {
           
        }

        private void OnEnable()
        {
            currentIndex = 0;
            commandWheelOptions = GetComponentsInChildren<CommandWheelOption>().ToList();
            
            SetSelectedRadialMenuOption();
            
            
            InitExtensions();
            
        }

        private void Update()
        {

            if (active)
            {
                inputDirection.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
                inputDirection.y = GameManager.Instance.playerInput.GetAxis("Move Vertical");
            
                if (inputDirection.sqrMagnitude > 0.1f) // Ensure some input is given
                {
                    UpdateMenuSelection();
                }
            }
            
            
        }

        public void UpdateMenuSelection()
        {
            float angle = Mathf.Atan2(-inputDirection.y, inputDirection.x) * Mathf.Rad2Deg - angleOffset;
            if (angle < 0)
            {
                angle += 360f;
            }
            
            // Determine the index of the closest menu item based on the angle
            float angleStep = 360f / commandWheelOptions.Count; // Each button covers 30 degrees in a 12-slice menu
            
            
            currentIndex = Mathf.RoundToInt(angle / angleStep) % commandWheelOptions.Count;
            SetSelectedRadialMenuOption();
            // Move the selector to the currently highlighted button
            //selector.position = menuButtons[currentIndex].position;

        }

        public void SetSelectedRadialMenuOption()
        {
            
            selectedCommandWheelOption = commandWheelOptions[currentIndex];
            
            foreach (CommandWheelOption radialMenuOption in commandWheelOptions)
            {
                
                radialMenuOption.SetActive();
                
                if (radialMenuOption != selectedCommandWheelOption)
                {
                    radialMenuOption.SetInactive();
                }

                if (radialMenuOption.toolTipData == null)
                {
                    
                }
            }

            commandWheelToolTip.combatToolTipData = selectedCommandWheelOption.toolTipData;

            if (commandWheelToolTip.combatToolTipData == null)
            {
                
                commandWheelToolTip.gameObject.SetActive(false);
            }
            else
            {
                commandWheelToolTip.SetToolTipData();
                commandWheelToolTip.gameObject.SetActive(true);
            }

        }

        public void InitExtensions()
        {
            foreach (CommandWheelExtension commandWheelExtension in GetComponents<CommandWheelExtension>())
            {
                commandWheelExtension.InitExtension();
            }
        }
        
        
    }
}