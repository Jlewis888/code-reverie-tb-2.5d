using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class PartyMenuSlot : PauseMenuNavigationButton
    {
        public Character character;
        public Slider healthSlider;
        public Slider skillPointsSlider;
        public Image characterPortrait;
        public TMP_Text healthText;
        public TMP_Text skillPointsText;
        
        
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
                healthSlider.value = character.currentHealth;
                healthSlider.maxValue = new CharacterStats(character).GetStat(StatAttribute.Health);

                healthText.text = $"{character.currentHealth.ToString()}/{new CharacterStats(character).GetStat(StatAttribute.Health)}";
                    
                skillPointsSlider.value = character.characterController.GetComponent<CharacterBattleManager>().currentSkillPoints;
                skillPointsSlider.maxValue = character.characterController.GetComponent<CharacterBattleManager>().skillPointsMax;
            }
            

            
            
        }
        
        
        public void InitCharacterHudPanel()
        {
            
            characterPortrait.sprite = character.GetCharacterPortrait();
            // healthSlider.maxValue = partySlot.character.characterController.GetComponent<Health>().MaxHealth;
            // nameText.text = partySlot.character.info.characterName;
        }
        
        
    }
}