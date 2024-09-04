using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "GlobalAreaManagerData", menuName = "Scriptable Objects/Area Management/Global Area Manager Data List", order = 1)]
    public class GlobalAreaManagerDataList : SerializedScriptableObject
    {

        public List<AreaManagerData> areaManagerDataList = new List<AreaManagerData>();
        
        
#if UNITY_EDITOR     
        [Button("Update")]
        public void Update()
        {
            areaManagerDataList = AssetDatabase.FindAssets("t:AreaManagerData", null).Select(guid => AssetDatabase.LoadAssetAtPath<AreaManagerData>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
        }
#endif
    }
}