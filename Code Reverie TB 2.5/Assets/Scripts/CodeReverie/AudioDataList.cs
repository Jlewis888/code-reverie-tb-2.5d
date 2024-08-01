using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "AudioDataList", menuName = "Scriptable Objects/Audio/AudioDataList", order = 1)]
    public class AudioDataList : SerializedScriptableObject
    {
        public List<AudioClip> audioClips = new List<AudioClip>();

        public Dictionary<string, AudioClip> audioClipMap = new Dictionary<string, AudioClip>();

        
#if UNITY_EDITOR
        
        [Button("Update")]
        public void Update()
        {
            //archetypeDataContainers = ScriptableObjectUtilities.FindAllScriptableObjectsOfType<ShopItem>("t:ShopItem", "Assets/Your Folders Go Here");
            audioClips = AssetDatabase.FindAssets("t:AudioClip", new[] {"Assets/Audio"}).Select(guid => AssetDatabase.LoadAssetAtPath<AudioClip>(AssetDatabase.GUIDToAssetPath(guid)) ).ToList();
            audioClipMap = new Dictionary<string, AudioClip>();
            
            foreach (var audioClip in audioClips)
            {
                if (!audioClipMap.ContainsKey(audioClip.name))
                {
                    audioClipMap.Add(audioClip.name, audioClip);
                }
                else
                {
                    Debug.Log("Duplicate Audio Clip name");
                }
            }
        }
        private void OnValidate()
        {

            audioClips = audioClips.Distinct().ToList();
            audioClipMap = new Dictionary<string, AudioClip>();
            
            foreach (var audioClip in audioClips)
            {
                if (!audioClipMap.ContainsKey(audioClip.name))
                {
                    audioClipMap.Add(audioClip.name, audioClip);
                }
                else
                {
                    Debug.Log("Duplicate Audio Clip name");
                }
            }
            
            UnityEditor.EditorUtility.SetDirty(this);

        }

#endif
        

    }
}