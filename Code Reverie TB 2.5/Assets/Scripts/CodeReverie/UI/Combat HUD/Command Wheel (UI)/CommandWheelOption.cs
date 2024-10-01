using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CommandWheelOption : SerializedMonoBehaviour
    {
        public Image backgroundImage;
        public Image actionIcon;
        public CombatToolTipData toolTipData;
        public bool disabled;

        private void Awake()
        {
            backgroundImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            if (toolTipData == null)
            {
                Disabled = true;
            }
        }


        public void SetActive()
        {
            if (backgroundImage == null || Disabled)
            {
                return;
            }
            backgroundImage.color = Color.blue;
        }

        public void SetInactive()
        {
            if (backgroundImage == null)
            {
                return;
            }
            
            backgroundImage.color = Color.white;
        }

        public bool Disabled
        {
            get { return disabled; }
            set
            {
                disabled = value;
                actionIcon.gameObject.SetActive(!disabled);
            }
        }
        
    }
}