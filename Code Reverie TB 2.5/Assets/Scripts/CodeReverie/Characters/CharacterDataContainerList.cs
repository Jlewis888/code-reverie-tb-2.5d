using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    
    [CreateAssetMenu(fileName = "CharacterDataContainerList", menuName = "Scriptable Objects/Characters/CharacterDataContainerList", order = 1)]

    public class CharacterDataContainerList : SerializedScriptableObject
    {
        public List<CharacterDataContainer> characters;
        public Dictionary<string, CharacterDataContainer> charactersMap = new Dictionary<string, CharacterDataContainer>();
        private void OnValidate()
        {
#if UNITY_EDITOR
            characters = characters.Distinct().ToList();

            charactersMap = new Dictionary<string, CharacterDataContainer>();
            
            foreach (var character in characters)
            {
                if (!charactersMap.ContainsKey(character.id))
                {
                    charactersMap.Add(character.id, character);
                }

            }
            
            
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}