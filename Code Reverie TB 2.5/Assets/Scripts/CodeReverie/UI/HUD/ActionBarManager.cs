using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ActionBarManager : SerializedMonoBehaviour
    {
        // public Image characterPortrait;
        // public Slider healthSlider;
        // public TMP_Text levelText;
        // public Image experienceSlider;
        // public TMP_Text experienceText;
        // public Slider interactionSlider;
        public CommandPanel commandPanel;
        public GameObject characterActionSliderHolder;
        public CharacterActionSlider characterActionSliderPF;
        

        private void Awake()
        {
            Clear();
            //interactionSlider.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onPartyUpdate += Init;
            EventManager.Instance.playerEvents.onCharacterSwap += Init;
            
            //SetActionBar();
        }
        
        private void OnDisable()
        {
            Clear();
            EventManager.Instance.playerEvents.onPartyUpdate -= Init;
            EventManager.Instance.playerEvents.onCharacterSwap -= Init;
        }

        public void Clear()
        {
            foreach (Transform child in characterActionSliderHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        
        private void Update()
        {

            // if (PlayerManager.Instance.activeParty != null)
            // {
            //     if (PlayerManager.Instance.currentParty[0] != null)
            //     {
            //         if (PlayerManager.Instance.currentParty[0].characterController != null)
            //         {
            //             healthSlider.maxValue = PlayerManager.Instance.currentParty[0].characterController.GetComponent<Health>().MaxHealth;
            //
            //             healthSlider.value = PlayerManager.Instance.currentParty[0].characterController.GetComponent<Health>().CurrentHealth;
            //
            //         }
            //     
            //     }
            // }
            //
            // levelText.text = PlayerManager.Instance.Level.ToString();
            // experienceSlider.fillAmount = PlayerManager.Instance.Experience / PlayerManager.Instance.GetCurrentMaxExperience();
            // experienceText.text = $"{PlayerManager.Instance.Experience}/{PlayerManager.Instance.GetCurrentMaxExperience()}";
        }

        public void Init()
        {
            foreach (CharacterBattleManager characterBattleManager in CombatManager.Instance.allUnits)
            {
                CharacterActionSlider characterActionSlider = Instantiate(characterActionSliderPF, characterActionSliderHolder.transform);
                characterActionSlider.characterBattleManager = characterBattleManager;
                characterActionSlider.SetSliderIconPosition();
                characterActionSlider.Init();
            }
            
            if (PlayerManager.Instance.currentParty != null)
            {
                if (PlayerManager.Instance.currentParty[0] != null)
                {
                   
                    // characterPortrait.sprite =
                    //     PlayerManager.Instance.currentParty[0].GetCharacterPortrait();
                }
            }
            
            EventManager.Instance.playerEvents.OnActionBarSet();
        }
        

        public void SetActionBar()
        {
        
            
        }
        
    }
}