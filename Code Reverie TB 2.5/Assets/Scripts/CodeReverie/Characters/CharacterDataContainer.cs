using System;
using System.Collections.Generic;
using System.Linq;
using CodeReverie.Vendors;
using Sirenix.OdinInspector;
using Unity.Behavior;
using UnityEditor;
using UnityEngine;
using Unity.Behavior.GraphFramework;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "CharacterDataContainer", menuName = "Scriptable Objects/Characters/CharacterDataContainer", order = 1)]
    public class CharacterDataContainer : SerializedScriptableObject
    {
        [HorizontalGroup("Split", 55, LabelWidth = 70)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
        public Sprite characterPortrait;
        [HorizontalGroup("Split", 55, LabelWidth = 70)]
        [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
        public Sprite characterSprite;
        
        [TabGroup("Character Details", TextColor = "green")] 
        public string id;
        
        [TabGroup("Character Details")]
        public string characterID;
        
        [TabGroup("Character Details")]
        public string characterName;
        
        
        [TabGroup("Character Details"), EnumToggleButtons]
        public CharacterType characterType;
        
        [TabGroup("Character Details")]
        public CharacterUnitController characterUnitPF;
        
        [TabGroup("Character Details")]
        public float experienceToGive;
        
        [TabGroup("Character Details")]
        public float attackRange = 1f;
        
        [TabGroup("Character Details")]
        public BehaviorGraph behaviorGraph;
        
        [TabGroup("Base Stats", TextColor = "blue"), HideLabel]
        public CharacterBaseStats baseStats;

        [TabGroup("Base Stats")] public int healthBarCount = 5;
        
        
        [TabGroup("Archetypes", TextColor = "red"), HideLabel]
        public ArchetypeDataContainer primaryArchetype;
        
        [TabGroup("Archetypes", TextColor = "red"), HideLabel]
        public List<ArchetypeDataContainer> archetypes = new List<ArchetypeDataContainer>() ;

        // [TabGroup("Archetypes")]
        // public ArchetypeDataContainer[,] archetypeDataContainers = new ArchetypeDataContainer[12, 6];
        
        [TabGroup("Character Loot Table")]
        public LootTableDataContainer lootTableDataContainer;

        
        [TabGroup("Character Vendor Inventory")]
        public VendorInventory vendorInventory;
#if UNITY_EDITOR 
        //[TabGroup("Archetypes")]
        [TabGroup("Character Details")]
        [Button("Update Assets")]
        public void UpdateAssets()
        {
            
            id = Guid.NewGuid().ToString();
            characterID = name;
            
            string assetPath  = AssetDatabase.GetAssetPath(this);

            if (characterType == CharacterType.Playable)
            {
                // string archetypesAssetPath = assetPath.Replace($"/{name}.asset", "/Archetypes");
                // archetypes = AssetDatabase.FindAssets("t:ArchetypeDataContainer", new []{archetypesAssetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<ArchetypeDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
                //
                //
                //
                // foreach (ArchetypeDataContainer archetypeDataContainer in archetypes)
                // {
                //     archetypeDataContainer.UpdateAssets();
                // }
            }
            
            string baseAssetPath = assetPath.Replace($"/{name}.asset", "");
            // Debug.Log(baseAssetPath);
            // Debug.Log(assetPath);
            List<CharacterBaseStats> baseStatsList = AssetDatabase.FindAssets("t:CharacterBaseStats", new []{baseAssetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<CharacterBaseStats>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
            List<BehaviorGraph> behaviorGraphAgentsList = AssetDatabase.FindAssets("t:BehaviorGraph", new []{baseAssetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<BehaviorGraph>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
           
            List<CharacterUnitController> characterControllers = new List<CharacterUnitController>();
            string[] prefabGuids = AssetDatabase.FindAssets("t:prefab", new[] { baseAssetPath });
            
            
            foreach (string guid in prefabGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                if (prefab != null)
                {
                    CharacterUnitController controller = prefab.GetComponent<CharacterUnitController>();
                    if (controller != null)
                    {
                        characterControllers.Add(controller);
                    }
                }
            }
            
            
            //string[] behaviorGuids = AssetDatabase.FindAssets("t:BehaviorAuthoringGraph", new[] { baseAssetPath });
            // string[] scriptableObjectGuids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { baseAssetPath });
            // var behaviorAuthoringGraphs = new List<ScriptableObject>();
            //
            // foreach (string guid in scriptableObjectGuids)
            // {
            //     string path = AssetDatabase.GUIDToAssetPath(guid);
            //     
            //     var allAssets = AssetDatabase.LoadAllAssetsAtPath(path);
            //
            //     foreach (var asset in allAssets)
            //     {
            //         if (asset == null) continue;
            //
            //         Type assetType = asset.GetType();
            //         if (assetType.FullName == "Unity.Behavior.BehaviorAuthoringGraph")
            //         {
            //             behaviorAuthoringGraphs.Add((ScriptableObject)asset);
            //         }
            //     }
            // }
            //
            // Debug.Log(behaviorAuthoringGraphs.Count);
            
            //List<CharacterUnitController> characterControllers = AssetDatabase.FindAssets("Character Unit Manager t:prefab", new []{baseAssetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<CharacterUnitController>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();

            
            if (baseStatsList.Count > 0)
            {
                baseStats = baseStatsList[0];
            }

            if (characterControllers.Count > 0)
            {
                characterUnitPF = characterControllers[0];
            }
            
            
            if (behaviorGraphAgentsList.Count > 0)
            {
                behaviorGraph = behaviorGraphAgentsList[0];
                if (characterUnitPF != null)
                {
                    characterUnitPF.GetComponent<BehaviorGraphAgent>().Graph = behaviorGraph;
                }
                //behaviorGraphAgent = behaviorAuthoringGraphs[0];
            }
            UnityEditor.EditorUtility.SetDirty(this);
        }
        
        #endif
        
        
        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            
            if (id == "")
            {
                id = Guid.NewGuid().ToString();
                characterID = name;
               
            }
            UnityEditor.EditorUtility.SetDirty(this);

#endif
        }
        
        
    }
}