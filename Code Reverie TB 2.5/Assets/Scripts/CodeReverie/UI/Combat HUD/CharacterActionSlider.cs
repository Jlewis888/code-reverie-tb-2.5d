using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CharacterActionSlider : SerializedMonoBehaviour
    {
        public CharacterBattleManager characterBattleManager;
        public Slider slider;
        public Image characterPortrait;
        public GameObject sliderIconHolder;
        public Image sliderIcon;
        


        private void Awake()
        {
            slider = GetComponent<Slider>();
            
        }

        private void OnEnable()
        {
            EventManager.Instance.combatEvents.onCharacterDeath += OnCharacterDeath;
            EventManager.Instance.combatEvents.onPlayerSelectTarget += OnPlayerSelectTarget;
            EventManager.Instance.combatEvents.onPlayerSelectTargetEnd += ScaleToNormal;
        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onCharacterDeath -= OnCharacterDeath;
            EventManager.Instance.combatEvents.onPlayerSelectTarget -= OnPlayerSelectTarget;
            EventManager.Instance.combatEvents.onPlayerSelectTargetEnd -= ScaleToNormal;
        }

        private void OnDestroy()
        {
            EventManager.Instance.combatEvents.onCharacterDeath -= OnCharacterDeath;
            EventManager.Instance.combatEvents.onPlayerSelectTarget -= OnPlayerSelectTarget;
            EventManager.Instance.combatEvents.onPlayerSelectTargetEnd -= ScaleToNormal;
        }


        public void Init()
        {
            slider.maxValue = characterBattleManager.actionPhaseCooldown;
            slider.value = characterBattleManager.cooldownTimer;

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

            // var rotation = sliderIconHolder.transform.rotation;
            // rotation.z = 90f;
            // sliderIconHolder.transform.rotation = rotation;

        }
        
        private void Update()
        {
            if (characterBattleManager != null)
            {
                slider.value = characterBattleManager.cooldownTimer;
            }
            
        }

        public void SetSliderIconPosition()
        {
            
            if (characterBattleManager.TryGetComponent(out ComponentTagManager componentTagManager))
            {
                if (componentTagManager.HasTag(ComponentTag.Enemy))
                {
                    RectTransform rectTransform = sliderIconHolder.GetComponent<RectTransform>();
                    
                    rectTransform.localPosition = new Vector3(rectTransform.localPosition.x,
                        -rectTransform.localPosition.y, rectTransform.localPosition.z);
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
                sliderIconHolder.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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
            sliderIconHolder.transform.localScale = Vector3.one;
            
            
        }

        public void UpdateCanvasOrder(int order)
        {
            GetComponent<Canvas>().sortingOrder = order;
            GetComponent<Canvas>().enabled = false;
            GetComponent<Canvas>().enabled = true;
        }
        
    }
}