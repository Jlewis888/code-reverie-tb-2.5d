using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace CodeReverie
{
    public class ComponentTagManager : SerializedMonoBehaviour
    {

        public List<ComponentTag> componentTagsList = new List<ComponentTag>();
        public HashSet<ComponentTag> componentTags = new HashSet<ComponentTag>();


        private void Awake()
        {
            componentTags = componentTagsList.ToHashSet();
        }


        public bool HasTag(ComponentTag componentTag)
        {
            if (componentTags.Contains(componentTag))
            {
                return true;
            }

            return false;
        }
        
    }
}