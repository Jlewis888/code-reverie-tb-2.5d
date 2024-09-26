using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace CodeReverie
{
    public class RadialMenuManager : SerializedMonoBehaviour
    {
        public TMP_Text optionText;
        public List<RadialMenuOption> radialMenuOptions = new List<RadialMenuOption>();
        public RadialMenuOption selectedRadialMenuOption;
        public Vector2 inputDirection;
        public float angleOffset = -90f; // Adjust to rotate the menu orientation (set -90 for "up" to be the first slice)
        public int currentIndex = 0;
        public RadialMenuToolTip radialMenuToolTip; 
        
        private void Awake()
        {
           
        }

        private void OnEnable()
        {
            currentIndex = 0;
            radialMenuOptions = GetComponentsInChildren<RadialMenuOption>().ToList();
            
            SetSelectedRadialMenuOption();
        }

        private void Update()
        {
            
            inputDirection.x = GameManager.Instance.playerInput.GetAxis("Move Horizontal");
            inputDirection.y = GameManager.Instance.playerInput.GetAxis("Move Vertical");
            
            if (inputDirection.sqrMagnitude > 0.1f) // Ensure some input is given
            {
                UpdateMenuSelection();
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
            float angleStep = 360f / radialMenuOptions.Count; // Each button covers 30 degrees in a 12-slice menu
            
            
            currentIndex = Mathf.RoundToInt(angle / angleStep) % radialMenuOptions.Count;
            SetSelectedRadialMenuOption();
            // Move the selector to the currently highlighted button
            //selector.position = menuButtons[currentIndex].position;

        }

        public void SetSelectedRadialMenuOption()
        {
            
            selectedRadialMenuOption = radialMenuOptions[currentIndex];
            
            foreach (RadialMenuOption radialMenuOption in radialMenuOptions)
            {
                
                radialMenuOption.SetActive();
                
                if (radialMenuOption != selectedRadialMenuOption)
                {
                    radialMenuOption.SetInactive();
                }
            }

            radialMenuToolTip.combatToolTipData = selectedRadialMenuOption.toolTipData;

            if (radialMenuToolTip.combatToolTipData == null)
            {
                radialMenuToolTip.gameObject.SetActive(false);
            }
            else
            {
                radialMenuToolTip.SetToolTipData();
                radialMenuToolTip.gameObject.SetActive(true);
            }

        }
        
        
    }
}