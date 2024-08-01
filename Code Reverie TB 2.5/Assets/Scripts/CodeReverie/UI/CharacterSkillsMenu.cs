using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CharacterSkillsMenu : SerializedMonoBehaviour
    {
        private PartySlot activePartySlot;
        public SelectArchetypeTreeButton selectArchetypeTreeButtonPF;
        public GameObject selectArchetypeTreeButtonHolder;
        public SkillType selectedSkillType;
        public GameObject skillButtonHolder;
        public CharacterSkillSelectButton characterSkillSelectButtonPF;
        public List<CharacterSkillSelectButton> characterSkillSelectButtons;

        public Button filterButtonBasic;
        public Button filterButtonMovement;
        public Button filterButtonAction;
        public Button filterButtonAlchemicBurst;
        

        private void Awake()
        {
            Clear();
            ClearSkills();
            
            filterButtonBasic.onClick.AddListener(()=>SetFilter(SkillType.Basic));
            filterButtonMovement.onClick.AddListener(()=>SetFilter(SkillType.Movement));
            filterButtonAction.onClick.AddListener(()=>SetFilter(SkillType.Action));
            filterButtonAlchemicBurst.onClick.AddListener(()=>SetFilter(SkillType.AlchemicBurst));
            
        }

        private void OnEnable()
        {
            if (activePartySlot != null)
            {
                foreach (Archetype archetype in activePartySlot.character.availableArchetypes)
                {
                    SelectArchetypeTreeButton selectArchetypeTreeButton = Instantiate(selectArchetypeTreeButtonPF,
                        selectArchetypeTreeButtonHolder.transform);

                    foreach (ArchetypeTree archetypeTree in CanvasManager.Instance.characterMenuManager.archetypeTrees)
                    {
                        if (archetypeTree.archetype.info.id == archetype.info.id)
                        {
                            selectArchetypeTreeButton.archetypeTree = archetypeTree;
                            break;
                        }
                    }
                }

                SelectedSkillType = SkillType.Basic;
                
                SetSkillButtons();
                FilterSkillButtons();
            }
        }

        private void OnDisable()
        {
            Clear();
            ClearSkills();
        }

        public void Clear()
        {
            foreach (Transform child in selectArchetypeTreeButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        public void ClearSkills()
        {
            foreach (Transform child in skillButtonHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public PartySlot ActivePartySlot
        {
            get { return activePartySlot; }

            set
            {
                if (value != activePartySlot)
                {
                    activePartySlot = value;
                }
            }
        }

        public void SetSelectArchetypeTreeButtons(int index)
        {
            if (ActivePartySlot.character.availableArchetypes.Count > 0)
            {
                
            }
        }

        public void SetSkillButtons()
        {
            characterSkillSelectButtons = new List<CharacterSkillSelectButton>();
            
            foreach (Skill skill in activePartySlot.character.characterSkills.learnedSkills)
            {
                CharacterSkillSelectButton skillSelectButton =
                    Instantiate(characterSkillSelectButtonPF, skillButtonHolder.transform);

                skillSelectButton.skill = skill;
                characterSkillSelectButtons.Add(skillSelectButton);
            }
        }
        
        public void FilterSkillButtons()
        {
            foreach (CharacterSkillSelectButton skillSelectButton in characterSkillSelectButtons)
            {
                skillSelectButton.gameObject.SetActive(skillSelectButton.skill.info.skillType == selectedSkillType);
            }
        }

        public void SetFilter(SkillType skillType)
        {
            SelectedSkillType = skillType;
        }

        public SkillType SelectedSkillType
        {
            get { return selectedSkillType; }
            set
            {
                if (value != selectedSkillType)
                {
                    selectedSkillType = value;
                    FilterSkillButtons();
                    
                }
            }
        }

    }
}