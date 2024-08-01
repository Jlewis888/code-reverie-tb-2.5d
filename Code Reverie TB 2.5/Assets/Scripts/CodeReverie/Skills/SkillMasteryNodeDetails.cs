using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "SkillMasteryNodeDetails", menuName = "Scriptable Objects/Skills/SkillMasteryNodeDetails", order = 1)]
    public class SkillMasteryNodeDetails : SerializedScriptableObject
    {
        public string id;
        public int maxAssignedPoints;
        public bool hasPrerequisites;
        public Dictionary<SkillMasteryNodeDetails, int> skillPrerequisites = new Dictionary<SkillMasteryNodeDetails, int>();
        public List<SkillModifierDataContainer> skillModifierDetailsList = new List<SkillModifierDataContainer>();


        private void OnValidate()
        {
#if UNITY_EDITOR
            id = name;
            

            if (skillPrerequisites != null)
            {
                if (skillPrerequisites.Count > 0)
                {
                    hasPrerequisites = true;
                }
            }
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        
    }
}