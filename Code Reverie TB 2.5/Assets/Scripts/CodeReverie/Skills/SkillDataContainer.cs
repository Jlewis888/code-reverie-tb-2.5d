using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "SkillDataContainer", menuName = "Scriptable Objects/Skills/SkillDataContainer", order = 1)]
    public class SkillDataContainer : SerializedScriptableObject
    {
        
        [HorizontalGroup("Split", 55, LabelWidth = 70)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
        public Sprite icon;
        
        //[VerticalGroup("Split/Meta")]
        [TabGroup("Skill Details", TextColor = "green")] 
        public string id;
        
        [TabGroup("Skill Details")]
        public string skillId;
        
        [TabGroup("Skill Details")]
        public string skillName;
        
        [TabGroup("Skill Details")]
        public string skillDescription;
        
        [TabGroup("Skill Details")]
        public bool baseSkill;
        
        [TabGroup("Skill Details")]
        public SkillType skillType = SkillType.None;
        
        [TabGroup("Skill Details")]
        public ArchetypeCategory archetypeCategory;
        
        [TabGroup("Skill Details")]
        public int actionPointsCost;
        
        [TabGroup("Skill Details")]
        public int skillPointsCost;
        
        [TabGroup("Skill Details")]
        public int resonancePointsCost;

        [TabGroup("Skill Details")] public float skillRange = 1f;
        [TabGroup("Skill Details")] public SkillCastTime skillCastTime = SkillCastTime.Medium;
        [TabGroup("Skill Details")] public TargetType targetType;
        [TabGroup("Skill Details")] public SkillDamageTargetType skillDamageTargetType;
        [TabGroup("Skill Details")] public float aoeRadius = 3f;
        
        [TabGroup("Resonance Skills", TextColor = "yellow")]
        public List<SkillDataContainer> resonanceSkillsList = new List<SkillDataContainer>();
        
        
        [TabGroup("Modifiers", TextColor = "blue")]
        public List<SkillModifierDataContainer> skillModifierDetailsList = new List<SkillModifierDataContainer>();

        [TabGroup("Modifiers")]
        public List<Stat> statModifiers = new List<Stat>();
        
        [TabGroup("Damage Types", TextColor = "orange")]
        public List<DamageTypes> skillDamageTypes = new List<DamageTypes>();
        
        [TabGroup("Skill Objects", TextColor = "red")]
        public SkillObject skillGameObject;
        
        [TabGroup("Animations", TextColor = "purple")]
        public string initialAnimation = "basic_attack_1";
        
        [TabGroup("Animations", TextColor = "purple")]
        public string castAnimation = "cast_1";
        
        [TabGroup("Animations")]
        public List<string> animationComboList = new List<string>();

        [TabGroup("Animations")] public List<SkillEventListener> skillEventListeners = new List<SkillEventListener>();

        [TabGroup("Animations")] public PlayableDirector playableDirector;
        
        
        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            
            SetOnValidateValues();

            //name = Name;
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        protected void SetOnValidateValues()
        {
            id = Regex.Replace( name, @"\s", string.Empty );
            skillId = id;
            skillModifierDetailsList = skillModifierDetailsList.Distinct().ToList();
            //skillMasteryNodes = skillMasteryNodes.Distinct().ToList();
            foreach (SkillModifierDataContainer skillModifierDetails in skillModifierDetailsList)
            {
                skillModifierDetails.skillDetailsId = id;
            }
            
            if (statModifiers == null)
            {
                statModifiers = new List<Stat>();
            }

            if (skillPointsCost == 0)
            {
                skillPointsCost = 5;
            }

            if (actionPointsCost == 0)
            {
                actionPointsCost = 1;
            }

            if (skillDamageTypes.Count == 0)
            {
                skillDamageTypes.Add(DamageTypes.Physical);
            }

            // foreach (Stat statModifier in statModifiers)
            // {
            //     statModifier.isPermanent = true;
            // }
            
            // animationComboList.Insert(0, initialAnimation);
            // animationComboList = animationComboList.Distinct().ToList();
            //
        }
        
    }
}