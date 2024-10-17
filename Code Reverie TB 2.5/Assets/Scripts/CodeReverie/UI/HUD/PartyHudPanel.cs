using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

namespace CodeReverie
{
    public class PartyHudPanel : SerializedMonoBehaviour
    {
        public Character character;
        public Slider healthSlider;
        public Slider skillPointsSlider;
        public Image characterPortrait;
        public TMP_Text nameText;
        public List<SkillBurstPointsUI> skillBurstPointsUis = new List<SkillBurstPointsUI>();
        public TMP_Text healthText;
        public TMP_Text skillPointsText;
        
        
        private void Awake()
        {
            skillBurstPointsUis = GetComponentsInChildren<SkillBurstPointsUI>().ToList();
        }

        private void Update()
        {
            // if (PlayerManager.Instance.characterSwapCooldownTimer > 0)
            // {
            //     swapCooldownText.gameObject.SetActive(true);
            //     swapCooldownText.text = MathF.Round(PlayerManager.Instance.characterSwapCooldownTimer, 2).ToString();
            //     swapCooldownImage.fillAmount = PlayerManager.Instance.characterSwapCooldownTimer /
            //                                    PlayerManager.Instance.characterSwapCooldown;
            // }
            // else
            // {
            //     swapCooldownText.gameObject.SetActive(false);
            //     swapCooldownImage.fillAmount = 0;
            // }
            
            if (character != null)
            {
                healthSlider.value = character.characterController.GetComponent<Health>().CurrentHealth;
                healthSlider.maxValue = character.characterController.GetComponent<Health>().MaxHealth;
                  
                healthText.text = $"{character.currentHealth.ToString()}/{character.characterController.GetComponent<Health>().MaxHealth}";
                
                skillPointsSlider.value = character.characterController.GetComponent<CharacterBattleManager>().currentSkillPoints;
                skillPointsSlider.maxValue = character.characterController.GetComponent<CharacterBattleManager>().skillPointsMax;
                
                SetSkillPointsBurstUI();
            }
        }
        
        
        public void InitCharacterHudPanel()
        {
            
            characterPortrait.sprite = character.GetCharacterPortrait();
            // healthSlider.maxValue = partySlot.character.characterController.GetComponent<Health>().MaxHealth;
            nameText.text = character.info.characterName;
        }

        public void SetSkillPointsBurstUI()
        {
            int availableBurstPoints = character.availableResonancePoints;
            
            foreach (SkillBurstPointsUI skillBurstPointsUi in skillBurstPointsUis)
            {
                skillBurstPointsUi.innerImage.gameObject.SetActive(false);
                if (availableBurstPoints > 0)
                {
                    skillBurstPointsUi.innerImage.gameObject.SetActive(true);
                }

                availableBurstPoints -= 1;
            }
        }
    }
}