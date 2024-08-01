using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "SkillModifierDataContainerList", menuName = "Scriptable Objects/Skills/SkillModifierDataContainerList", order = 1)]

    public class SkillModifierDataContainerList : SerializedScriptableObject
    {
        public List<SkillModifierDataContainer> skillModifierDetails;
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            skillModifierDetails = skillModifierDetails.Distinct().ToList();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }



        public Dictionary<string, SkillModifier> GetAllSkillModifiers(Transform transform)
        {
            Dictionary<string, SkillModifier> skillModifierDetailsMap =
                new Dictionary<string, SkillModifier>();


            foreach (SkillModifierDataContainer skillModifierDetails in skillModifierDetails)
            {
                if (skillModifierDetailsMap.ContainsKey(skillModifierDetails.id))
                {
                    
                }
                
                Type itemType = Type.GetType($"ProjectAlchemy.{skillModifierDetails.skillModifierId}");
                
                SkillModifier skillModifier = null;
                if (itemType != null)
                {
                    skillModifier = Activator.CreateInstance(itemType) as SkillModifier;
                }

                
                skillModifierDetailsMap.Add(skillModifierDetails.id, skillModifier);
                
            }


            return skillModifierDetailsMap;
        }

    }
}