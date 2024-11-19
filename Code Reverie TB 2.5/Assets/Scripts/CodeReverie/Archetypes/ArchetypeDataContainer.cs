using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie

{
    [CreateAssetMenu(fileName = "ArchetypeDataContainer", menuName = "Archetypes/ArchetypeDataContainer", order = 1)]

    public class ArchetypeDataContainer : SerializedScriptableObject
    {
        
        
        [HorizontalGroup("Split", 55, LabelWidth = 70)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
        public Sprite icon;
        
        [TabGroup("Details", TextColor = "green")]
        public string id;
        
        [TabGroup("Details", TextColor = "green")]
        public string archetypeName;

        // [TabGroup("Base Skills", TextColor = "red")]
        // public Dictionary<SkillType, SkillDataContainer> baseSkills = new Dictionary<SkillType, SkillDataContainer>();
        //
        [TabGroup("Level 1 Skills", TextColor = "#7bcdf2")]
        public List<SkillDataContainer> skillsLevel1 = new List<SkillDataContainer>();
        
        [TabGroup("Level 2 Skills", TextColor = "red")]
        public List<SkillDataContainer> skillsLevel2 = new List<SkillDataContainer>();
        
        [TabGroup("Level 3 Skills", TextColor = "orange")]
        public List<SkillDataContainer> skillsLevel3 = new List<SkillDataContainer>();
        
        [TabGroup("Level 4 Skills", TextColor = "#c191ff")]
        public List<SkillDataContainer> skillsLevel4 = new List<SkillDataContainer>();

        
#if UNITY_EDITOR
        //[ContextMenu("Update Archetype Data Containers")]
        [Button("Update")]
        public void UpdateAssets()
        {
            // string assetPath  = AssetDatabase.GetAssetPath(this);
            // assetPath = assetPath.Replace($"/{name}.asset", "");
            
            //archetypeSkillNodeDataContainers = AssetDatabase.FindAssets("t:ArchetypeSkillNodeDataContainer", new []{assetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<ArchetypeSkillNodeDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
           
            Debug.Log("Currently does nothing");
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }
        }

#endif
    }
}