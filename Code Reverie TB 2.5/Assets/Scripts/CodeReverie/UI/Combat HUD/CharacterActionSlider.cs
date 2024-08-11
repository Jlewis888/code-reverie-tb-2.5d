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
        }

        private void OnDisable()
        {
            EventManager.Instance.combatEvents.onCharacterDeath -= OnCharacterDeath;
        }

        private void OnDestroy()
        {
            EventManager.Instance.combatEvents.onCharacterDeath -= OnCharacterDeath;
        }


        public void Init()
        {
            slider.maxValue = characterBattleManager.actionPhaseCooldown;
            slider.value = characterBattleManager.cooldownTimer;

            if (GetComponent<CharacterUnitController>() != null)
            {
                if (GetComponent<CharacterUnitController>().character != null)
                {
                    characterPortrait.sprite = characterBattleManager.GetComponent<CharacterUnitController>().character
                        .GetCharacterPortrait();
                }
            }

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
        
    }
}