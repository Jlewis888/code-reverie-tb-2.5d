using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "New Base Item List Container", menuName = "Scriptable Objects/Base Item Details List Container")]
    public class BaseItemDetailsListContainer : SerializedScriptableObject
    {
        public List<ItemInfo> items = new List<ItemInfo>();

#if UNITY_EDITOR     
        [Button("Update")]
        public void Update()
        {
            //archetypeDataContainers = ScriptableObjectUtilities.FindAllScriptableObjectsOfType<ShopItem>("t:ShopItem", "Assets/Your Folders Go Here");
            items = AssetDatabase.FindAssets("t:ItemInfo", null).Select(guid => AssetDatabase.LoadAssetAtPath<ItemInfo>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
            // audioClipMap = new Dictionary<string, AudioClip>();
            //
            // foreach (var audioClip in audioClips)
            // {
            //     if (!audioClipMap.ContainsKey(audioClip.name))
            //     {
            //         audioClipMap.Add(audioClip.name, audioClip);
            //     }
            //     else
            //     {
            //         Debug.Log("Duplicate Audio Clip name");
            //     }
            // }
        }
#endif
        
        
        
        
        
        private void OnValidate()
        {
                            
#if UNITY_EDITOR
            RemoveNulls();
            items = items.Distinct().ToList();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }


        public void RemoveNulls()
        {
            items.RemoveAll(x => x == null);
        }
    }
}