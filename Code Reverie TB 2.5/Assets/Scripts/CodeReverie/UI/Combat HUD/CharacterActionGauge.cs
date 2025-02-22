using System;
using CodeReverie;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace a
{
    public class CharacterActionGauge : SerializedMonoBehaviour
    {
        public CharacterBattleManager characterBattleManager;
        public GameObject iconGroup;
        public Transform iconHolder;
        public Image characterPortrait;
        public float rotationSpeed;
        
        public float minValue = 0f;      // Minimum value of the gauge
        public float maxValue = 100f;    // Maximum value of the gauge
        public float value = 0f;         // Current value of the gauge

        public float timer;

        private void Update()
        {
            
            if (characterBattleManager != null)
            {
                // transform.Rotate(0,0, -rotationSpeed * characterBattleManager.cooldownTimer);
                // transform.Rotate(0,0, -angle);


                value = characterBattleManager.cooldownTimer;
                float normalizedValue = (value - minValue) / (maxValue - minValue);
                float angle = normalizedValue * 360f;
                transform.rotation = Quaternion.Euler(0, 0, -angle);
                
                if (iconHolder != null)
                {
                    Vector3 currentRotation = iconHolder.eulerAngles;
                    iconHolder.rotation = Quaternion.Euler(iconHolder.transform.eulerAngles.x, iconHolder.transform.eulerAngles.y,  transform.rotation.z * -1f);
                }
            }
            
        }

        public void Init()
        {
            
            maxValue = characterBattleManager.actionPhaseCooldown;
            value = characterBattleManager.cooldownTimer;
            
            
            
            if (characterBattleManager != null)
            {
                if (characterBattleManager.GetComponent<CharacterUnitController>() != null)
                {
                    if (characterBattleManager.GetComponent<CharacterUnitController>().character != null)
                    {
                        characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>().character
                            .GetCharacterPortrait();
                    }
                }
            }
        }
        
        public void SetSliderIconPosition()
        {
            
            if (characterBattleManager.TryGetComponent(out ComponentTagManager componentTagManager))
            {
                if (componentTagManager.HasTag(ComponentTag.Enemy))
                {
                    RectTransform rectTransform = iconGroup.GetComponent<RectTransform>();
                    
                    rectTransform.localPosition = new Vector3(46.6f,
                        0, 0);
                }
            }
        }
        
        
        public void OnCharacterDeath(CharacterBattleManager characterBattleManager)
        {
            if (this.characterBattleManager == characterBattleManager)
            {
                Destroy(gameObject);
            }
        }
        
        public void OnPlayerSelectTarget(CharacterBattleManager characterBattleManager)
        {
            if (this.characterBattleManager == characterBattleManager)
            {
                UpdateCanvasOrder(1);
                //sliderIconHolder.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                UpdateCanvasOrder(0);
                ScaleToNormal(null);
            }
        }

        public void ScaleToNormal(CharacterBattleManager characterBattleManager)
        {
            UpdateCanvasOrder(0);
            //sliderIconHolder.transform.localScale = Vector3.one;
            
            
        }

        public void UpdateCanvasOrder(int order)
        {
            GetComponent<Canvas>().sortingOrder = order;
            GetComponent<Canvas>().enabled = false;
            GetComponent<Canvas>().enabled = true;
        }
    }
}