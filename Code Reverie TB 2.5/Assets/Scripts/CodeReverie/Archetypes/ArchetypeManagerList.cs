using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "ArchetypeManagerList", menuName = "Scriptable Objects/Archetypes/ArchetypeManagerList", order = 1)]

    public class ArchetypeManagerList : SerializedScriptableObject
    {
        public List<ArchetypeDataContainer> archetypeDataContainers = new List<ArchetypeDataContainer>();

#if UNITY_EDITOR
        //[ContextMenu("Update Archetype Data Containers")]
        [Button("Update")]
        public void UpdateArchetypeDataContainers()
        {
            //archetypeDataContainers = ScriptableObjectUtilities.FindAllScriptableObjectsOfType<ShopItem>("t:ShopItem", "Assets/Your Folders Go Here");
            archetypeDataContainers = AssetDatabase.FindAssets("t:ArchetypeDataContainer", null).Select(guid => AssetDatabase.LoadAssetAtPath<ArchetypeDataContainer>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();

            foreach (ArchetypeDataContainer archetypeDataContainer in archetypeDataContainers)
            {
                archetypeDataContainer.UpdateAssets();
            }
        }
#endif 
        private void OnValidate()
        {
#if UNITY_EDITOR
            archetypeDataContainers = archetypeDataContainers.Distinct().ToList();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}