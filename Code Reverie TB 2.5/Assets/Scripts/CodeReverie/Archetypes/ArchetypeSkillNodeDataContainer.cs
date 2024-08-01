using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{

    [CreateAssetMenu(fileName = "ArchetypeSkillNodeDataContainer",
        menuName = "Scriptable Objects/Archetypes/ArchetypeSkillNodeDataContainer", order = 1)]

    public class ArchetypeSkillNodeDataContainer : SerializedScriptableObject
    {
        public string id;
        public SkillDataContainer skillDataContainer;
        public ArchetypeSkillNodeType archetypeSkillNodeType;
        public int minTreePoints;
        public int maxAssignedPoints;
        public List<ArchetypeSkillNodeDataContainer> linkedSkillNodes = new List<ArchetypeSkillNodeDataContainer>();


#if UNITY_EDITOR
        [Button("Link Nodes")]
        public void LinkNodes()
        {
            foreach (ArchetypeSkillNodeDataContainer archetypeSkillNodeDataContainer in linkedSkillNodes)
            {
                if (!archetypeSkillNodeDataContainer.linkedSkillNodes.Contains(this))
                {
                    archetypeSkillNodeDataContainer.linkedSkillNodes.Add(this);
                }
            }
        }
        
        [Button("Unlink Nodes")]
        public void UnlinkNodes(ArchetypeDataContainer archetypeDataContainer)
        {

            foreach (ArchetypeSkillNodeDataContainer archetypeSkillNodeDataContainer in archetypeDataContainer.archetypeSkillNodeDataContainers)
            {
                if (!linkedSkillNodes.Contains(archetypeSkillNodeDataContainer))
                {
                    archetypeSkillNodeDataContainer.linkedSkillNodes.Remove(this);
                }
            }
            
        }
        
        
        [Button("Unlink Node")]
        public void UnlinkNode(ArchetypeSkillNodeDataContainer archetypeSkillNodeDataContainer)
        {
            if (archetypeSkillNodeDataContainer.linkedSkillNodes.Contains(this))
            {
                archetypeSkillNodeDataContainer.linkedSkillNodes.Remove(this);
            }

            if (linkedSkillNodes.Contains(archetypeSkillNodeDataContainer))
            {
                linkedSkillNodes.Remove(archetypeSkillNodeDataContainer);
            }
        }
        #endif
        
        private void OnValidate()
        {
#if UNITY_EDITOR
            //id = name;

            linkedSkillNodes = linkedSkillNodes.Distinct().ToList();
            
            

            if (archetypeSkillNodeType == ArchetypeSkillNodeType.Root)
            {
                linkedSkillNodes.RemoveAll(x => x.archetypeSkillNodeType == ArchetypeSkillNodeType.Root);
            }

            if (linkedSkillNodes.Contains(this))
            {
                linkedSkillNodes.Remove(this);
            }


            
            

            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}