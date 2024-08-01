using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{
    public class ActionBarSkillSlot : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public SkillSlot skillSlot;
        public int slotNum;
        public Image buttonImage;
        public Image skillImage;
        public float skillCooldownTimer;
        public float skillCooldown;
        public Image cooldownImage;
        public TMP_Text cooldownText;
        public SkillType slotSkillType;


        private void OnEnable()
        {
            //EventManager.Instance.playerEvents.onPartyUpdate += Init;
            //EventManager.Instance.playerEvents.onCharacterSwap += Init;
            //EventManager.Instance.playerEvents.onSkillSlotUpdate += Init;
            EventManager.Instance.playerEvents.onActionBarSet += Init;
        }

        private void Start()
        {
            Init();
        }

        private void OnDestroy()
        {
            EventManager.Instance.playerEvents.onActionBarSet -= Init;
        }


        private void Update()
        {

            if (skillSlot != null)
            {
                
                if (skillSlot.skillSlotCooldownTimer > 0)
                {
                    cooldownText.gameObject.SetActive(true);
                    cooldownText.text = Mathf.Ceil(skillSlot.skillSlotCooldownTimer).ToString();
                    cooldownImage.fillAmount = skillSlot.skillSlotCooldownTimer / skillCooldown;
                }
                else
                {
                    cooldownText.gameObject.SetActive(false);
                    cooldownImage.fillAmount = 0;
                } 
            }

            
      
        }


        public void Init()
        {
            if (PlayerManager.Instance.currentParty != null)
            {
                if (PlayerManager.Instance.currentParty[0] != null)
                {
                    // if (alchemicBurstSlot)
                    // {
                    //     skillSlot =  PlayerManager.Instance.currentParty[0]SkillsManager.character.characterSkills.equippedAlchemicBurst;
                    // }
                    // else
                    // {
                    //     skillSlot = PlayerManager.Instance.currentParty[0]SkillsManager.character.characterSkills.equippedActionSkills[slotNum];
                    // }
                    
                    
                    //skillSlot = PlayerManager.Instance.currentParty[0].characterSkills.equippedSkills[slotSkillType];
                    skillSlot = PlayerManager.Instance.currentParty[0].equippedArchetype.skills.equippedSkills[slotSkillType];
            
            
                    if (skillSlot != null && skillSlot.skill != null)
                    {
                        skillImage.sprite = skillSlot.skill.GetSpriteIcon();
                        //skillCooldown = skillSlot.skill.info.cooldown;
                    }
                    else
                    {
                        skillImage.sprite = null;
                        cooldownText.gameObject.SetActive(false);
                        cooldownImage.fillAmount = 0;
                    }
                }
            }
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            // if (eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(transform))
            //     return;

            if (eventData.pointerEnter.TryGetComponent(out ActionBarSkillSlot actionBarSkillSlot))
            {
                Debug.Log("yo yo yo yo yo yo");
            }
            
            
            TooltipData tooltipData = new TooltipData();

            tooltipData.name = skillSlot.skill.info.skillName;
            tooltipData.toolTipType = "skill";
            tooltipData.description = $"{skillSlot.skill.info.skillDescription}";
            
            
            EventManager.Instance.generalEvents.OpenToolTip(tooltipData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // if (eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(transform))
            //     return;

            if (eventData.fullyExited)
            {
                EventManager.Instance.generalEvents.CloseToolTip();
            }
            
            
        }
        
    }
}