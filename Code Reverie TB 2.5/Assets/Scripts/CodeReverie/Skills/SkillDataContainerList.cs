using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


namespace CodeReverie
{
    [CreateAssetMenu(fileName = "Skill Data Container List", menuName = "Scriptable Objects/Skills/SkillDataContainerList", order = 1)]
    public class SkillDataContainerList : SerializedScriptableObject
    {
        public List<SkillDataContainer> skillDetailsList;
        
#if UNITY_EDITOR
        [Button("Update Skilll")]
        public void UpdateArchetypeDataContainers()
        {
            //archetypeDataContainers = ScriptableObjectUtilities.FindAllScriptableObjectsOfType<ShopItem>("t:ShopItem", "Assets/Your Folders Go Here");
            skillDetailsList = AssetDatabase.FindAssets("t:SkillDataContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<SkillDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
            
            
            foreach (SkillDataContainer skillDetails in skillDetailsList)
            {
                Type itemType = Type.GetType($"CodeReverie.{skillDetails.skillId}");

                if (itemType == null)
                {
                    Debug.Log($"Skill Script Not Found: {skillDetails}");
                }
                
            }
            
        }
        #endif

        private void OnValidate()
        {
#if UNITY_EDITOR
            skillDetailsList = skillDetailsList.Distinct().ToList();
            
            
            // foreach (SkillDataContainer skillDetails in skillDetailsList)
            // {
            //     Type itemType = Type.GetType($"CodeReverie.{skillDetails.skillId}");
            //
            //     if (itemType == null)
            //     {
            //         Debug.Log($"Skill Script Not Found: {skillDetails}");
            //     }
            //     
            // }
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        

        private void Start()
        {
            
            // Type itemType = Type.GetType($"ProjectAlchemy.FireballSkill");
            //
            // if (itemType != null)
            // {
            //     testSkill = Activator.CreateInstance(itemType, new [] {skillDetails}) as FireballSkill;
            // }
            //Init();
        }
        
        // public void Init()
        // {
        //     skills = new Dictionary<string, Skill>();
        //     skills = GetAllSkills();
        // }

        public Dictionary<string, SkillDataContainer> GetAllSkills()
        {
            
            
            //SkillDataContainer[] skillDetailsList = Resources.LoadAll<SkillDataContainer>("Skills");
            Dictionary<string, SkillDataContainer> allSkills = new Dictionary<string, SkillDataContainer>();
            
            foreach (SkillDataContainer skillDetails in skillDetailsList)
            {
                if (allSkills.ContainsKey(skillDetails.id))
                {
                    Debug.Log("Duplicate Skill IDs");
                }
                
                Type itemType = Type.GetType($"CodeReverie.{skillDetails.skillId}");

                if (itemType == null)
                {
                    Debug.Log($"Skill Script Not Found: {skillDetails}");
                }
                
                
                SkillDataContainer skillDataContainer = Instantiate(skillDetails);
                
                allSkills.Add(skillDataContainer.id, skillDataContainer);
            }

            return allSkills;
        }
        
    }
}