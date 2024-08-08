using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie

{
    [CreateAssetMenu(fileName = "ArchetypeDataContainer", menuName = "Scriptable Objects/Archetypes/ArchetypeDataContainer", order = 1)]

    public class ArchetypeDataContainer : SerializedScriptableObject
    {
        
        
        [HorizontalGroup("Split", 55, LabelWidth = 70)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
        public Sprite icon;
        
        [TabGroup("Details", TextColor = "green")]
        public string id;
        
        [TabGroup("Details")]
        public string archetypeName;
        
        [TabGroup("Details")]
        public string branchName1;
        
        [TabGroup("Details")]
        public string branchName2;
        
        [TabGroup("Details")]
        public string branchName3;
        
        

        [TabGroup("Base Skills", TextColor = "red")]
        public Dictionary<SkillType, SkillDataContainer> baseSkills = new Dictionary<SkillType, SkillDataContainer>();
        
        [TabGroup("Skill Tree Details", TextColor = "blue")]
        public List<ArchetypeSkillNodeDataContainer> archetypeSkillNodeDataContainers = new List<ArchetypeSkillNodeDataContainer>();

        
#if UNITY_EDITOR
        //[ContextMenu("Update Archetype Data Containers")]
        [Button("Update")]
        public void UpdateAssets()
        {
            string assetPath  = AssetDatabase.GetAssetPath(this);
            assetPath = assetPath.Replace($"/{name}.asset", "");
            
            archetypeSkillNodeDataContainers = AssetDatabase.FindAssets("t:ArchetypeSkillNodeDataContainer", new []{assetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<ArchetypeSkillNodeDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
           
            
        }

        private void OnValidate()
        {
            if (!baseSkills.ContainsKey(SkillType.Basic))
            {
                baseSkills.Add(SkillType.Basic, null);
            }
            
            if (!baseSkills.ContainsKey(SkillType.Dodge))
            {
                baseSkills.Add(SkillType.Dodge, null);
            }
            
            if (!baseSkills.ContainsKey(SkillType.Action))
            {
                baseSkills.Add(SkillType.Action, null);
            }
            
            if (!baseSkills.ContainsKey(SkillType.AlchemicBurst))
            {
                baseSkills.Add(SkillType.AlchemicBurst, null);
            }

            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }
        }

#endif
    }
}