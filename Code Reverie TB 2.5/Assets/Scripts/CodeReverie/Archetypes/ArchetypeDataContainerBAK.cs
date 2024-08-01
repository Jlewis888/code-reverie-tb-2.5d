using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie

{
    //[CreateAssetMenu(fileName = "ArchetypeDataContainer", menuName = "Scriptable Objects/Archetypes/ArchetypeDataContainer", order = 1)]

    public class ArchetypeDataContainerBAK : SerializedScriptableObject
    {
        public string id;
        public Sprite icon;
        public string archetypeName;

        public List<ArchetypeSkillNodeDataContainer> archetypeSkillNodeDataContainers = new List<ArchetypeSkillNodeDataContainer>();

        public Dictionary<string, ArchetypeSkillNodeDataContainer> archetypeSkillNodeDataContainerMap =
            new Dictionary<string, ArchetypeSkillNodeDataContainer>();


        public ArchetypeTree archetypeTree;
        public List<SkillNodeLink> skillNodeLinks = new List<SkillNodeLink>();

#if UNITY_EDITOR
        //[ContextMenu("Update Archetype Data Containers")]
        [Button("Update")]
        public void UpdateAssets()
        {
            string assetPath  = AssetDatabase.GetAssetPath(this);
            assetPath = assetPath.Replace($"/{name}.asset", "");
            
            archetypeSkillNodeDataContainers = AssetDatabase.FindAssets("t:ArchetypeSkillNodeDataContainer", new []{assetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<ArchetypeSkillNodeDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
            List<ArchetypeTree> archetypeTrees = AssetDatabase.FindAssets("Archetype Tree t:prefab", new []{assetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<ArchetypeTree>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();

            archetypeTree = archetypeTrees[0];
            
        }
        
#endif
    }
}