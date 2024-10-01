using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeReverie
{

    public enum ActionCommandType
    {
        Attack,
        Defend,
        Skill
    }
    
    public class ActionBarCommandSlot : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button button;
        public ActionCommandType actionCommandType;
        public SkillSlot skillSlot;
        public Image skillImage;
        public SkillType slotSkillType;
        public int slotNumber;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(SelectCharacterAction);
        }

        private void OnEnable()
        {
            EventManager.Instance.playerEvents.onActionBarSet += Init;
            Init();
        }

        private void Start()
        {
            
        }

        private void OnDestroy()
        {
            EventManager.Instance.playerEvents.onActionBarSet -= Init;
        }

        
        public void Init()
        {
            // if (PlayerManager.Instance.activeParty != null)
            // {
            //     if (PlayerManager.Instance.currentParty[0] != null)
            //     {
            //         skillSlot = PlayerManager.Instance.currentParty[0].equippedArchetype.skills.equippedSkills[slotSkillType];
            //
            //         if (skillSlot != null && skillSlot.skill != null)
            //         {
            //             skillImage.sprite = skillSlot.skill.GetSpriteIcon();
            //         }
            //         else
            //         {
            //             skillImage.sprite = null;
            //         }
            //     }
            // }

            // if (actionCommandType == ActionCommandType.Skill)
            // {
            //     
            //     if(BattleManager.Instance.selectedPlayerCharacter != null)
            //     {
            //         if (BattleManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>() != null)
            //         {
            //             skillSlot = BattleManager.Instance.selectedPlayerCharacter.GetComponent<CharacterUnitController>().character.characterSkills.equippedActionSkills[slotNumber];
            //     
            //             if (skillSlot != null && skillSlot.skill != null)
            //             {
            //                 skillImage.sprite = skillSlot.skill.GetSpriteIcon();
            //                 skillImage.color = new Color(skillImage.color.r, skillImage.color.g, skillImage.color.b, 1f);
            //             }
            //             else
            //             {
            //                 skillImage.sprite = null;
            //                 skillImage.color = new Color(skillImage.color.r, skillImage.color.g, skillImage.color.b, 0f);
            //             }
            //         } 
            //     }
            // }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {


            // if (eventData.pointerEnter.TryGetComponent(out ActionBarSkillSlot actionBarSkillSlot))
            // {
            //     Debug.Log("yo yo yo yo yo yo");
            // }
            //
            //
            // TooltipData tooltipData = new TooltipData();
            //
            // tooltipData.name = skillSlot.skill.info.skillName;
            // tooltipData.toolTipType = "skill";
            // tooltipData.description = $"{skillSlot.skill.info.skillDescription}";
            //
            //
            // EventManager.Instance.generalEvents.OpenToolTip(tooltipData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // if (eventData.fullyExited)
            // {
            //     EventManager.Instance.generalEvents.CloseToolTip();
            // }
        }

        public void SelectCharacterAction()
        {

            switch (actionCommandType)
            {
                case ActionCommandType.Attack:
                    CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Attack;
                    CombatManager.Instance.combatManagerState = CombatManagerState.TargetSelect;
                    CombatManager.Instance.SetSelectableTargets();
                    break;
                
                case ActionCommandType.Defend:
                    CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Defend;
                    break;
                
                case ActionCommandType.Skill:
                    CombatManager.Instance.selectedPlayerCharacter.characterBattleActionState = CharacterBattleActionState.Skill;
                    CombatManager.Instance.combatManagerState = CombatManagerState.TargetSelect;
                    break;
                
            }
            
            
        }
        
    }
}