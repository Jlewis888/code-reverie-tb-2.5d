using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "SkillModifierDataContainer", menuName = "Scriptable Objects/Skills/SkillModifierDataContainer", order = 1)]

    public class SkillModifierDataContainer : SerializedScriptableObject
    {
        public string id;
        public string skillDetailsId;
        public string skillModifierId;
        
        
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            id = name;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}