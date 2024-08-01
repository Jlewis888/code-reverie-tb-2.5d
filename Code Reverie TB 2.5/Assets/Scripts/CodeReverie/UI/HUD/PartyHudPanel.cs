using System;
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
                    
                skillPointsSlider.value = character.characterController.GetComponent<CharacterBattleManager>().currentSkillPoints;
                skillPointsSlider.maxValue = character.characterController.GetComponent<CharacterBattleManager>().skillPointsMax;
            }
            

            
            
        }
        
        
        public void InitCharacterHudPanel()
        {
            
            //characterPortrait.sprite = partySlot.character.GetCharacterPortrait();
            // healthSlider.maxValue = partySlot.character.characterController.GetComponent<Health>().MaxHealth;
            // nameText.text = partySlot.character.info.characterName;
        }
        
        
    }
}