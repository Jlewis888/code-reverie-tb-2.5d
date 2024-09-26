using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class RadialMenuOption : SerializedMonoBehaviour
    {
        public Image backgroundImage;
        public CombatToolTipData toolTipData;


        private void Awake()
        {
            backgroundImage = GetComponent<Image>();
            
        }

        public void SetActive()
        {
            backgroundImage.color = Color.blue;
        }

        public void SetInactive()
        {
            backgroundImage.color = Color.white;
        }
        
    }
}