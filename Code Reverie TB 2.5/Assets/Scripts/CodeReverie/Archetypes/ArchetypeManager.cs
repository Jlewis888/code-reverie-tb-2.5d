using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class ArchetypeManager : ManagerSingleton<ArchetypeManager>
    {

        public ArchetypeManagerList archetypeManagerList;

        public Dictionary<string, ArchetypeDataContainer> archetypeDataContainers =
            new Dictionary<string, ArchetypeDataContainer>();


        protected override void Awake()
        {
            base.Awake();
            SetDataMap();
        }



        public void SetDataMap()
        {
            archetypeDataContainers = new Dictionary<string, ArchetypeDataContainer>();


            foreach (ArchetypeDataContainer archetypeDataContainer in archetypeManagerList.archetypeDataContainers)
            {
                if (!archetypeDataContainers.ContainsKey(archetypeDataContainer.id))
                {
                    archetypeDataContainers.Add(archetypeDataContainer.id, archetypeDataContainer);
                }
            }
        }
    }
}