using System;
using System.Collections.Generic;
using System.Linq;
using CodeReverie.Vendors;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

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
        public CharacterController characterUnitPF;
        
        [TabGroup("Character Details")]
        public float experienceToGive;
        
        [TabGroup("Character Details")]
        public float attackRange = 1f;
        
        
        [TabGroup("Base Stats", TextColor = "blue"), HideLabel]
        public CharacterBaseStats baseStats;

        [TabGroup("Base Stats")] public int healthBarCount = 5;
        
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
            
            string baseStatsAssetPath = assetPath.Replace($"/{name}.asset", "");
            List<CharacterBaseStats> baseStatsList = AssetDatabase.FindAssets("t:CharacterBaseStats", new []{baseStatsAssetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<CharacterBaseStats>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
            List<CharacterController> characterControllers = AssetDatabase.FindAssets("Character Unit Manager t:prefab", new []{baseStatsAssetPath}).Select(guid => AssetDatabase.LoadAssetAtPath<CharacterController>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();

            
            if (baseStatsList.Count > 0)
            {
                baseStats = baseStatsList[0];
            }

            if (characterControllers.Count > 0)
            {
                characterUnitPF = characterControllers[0];
            }
            
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