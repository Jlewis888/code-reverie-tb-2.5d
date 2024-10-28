using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "GlobalAreaManagerConfig", menuName = "Scriptable Objects/Area Management/Global Area Manager Config List", order = 1)]
    public class GlobalAreaManagerConfigList : SerializedScriptableObject
    {

        public List<AreaManagerConfig> areaManagerConfigList = new List<AreaManagerConfig>();
        
        
#if UNITY_EDITOR     
        [Button("Update")]
        public void Update()
        {
            areaManagerConfigList = AssetDatabase.FindAssets("t:AreaManagerConfig", null).Select(guid => AssetDatabase.LoadAssetAtPath<AreaManagerConfig>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
        }
#endif
    }
}