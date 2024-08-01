using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class CharacterManager : ManagerSingleton<CharacterManager>
    {
        public CharacterDataContainerList characterDataContainerList;
        public Dictionary<string, CharacterDataContainer> characterDataContainers = new Dictionary<string, CharacterDataContainer>();
        
        protected override void Awake()
        {
            base.Awake();
            SetAllCharacters();
        }
        
        public void SetAllCharacters()
        {
            characterDataContainers = new Dictionary<string, CharacterDataContainer>();
            
            foreach (CharacterDataContainer character in characterDataContainerList.characters)
            {

                if (!characterDataContainers.ContainsKey(character.id))
                {
                    characterDataContainers.Add(character.id, character);
                }
            }
        }
        
        public CharacterDataContainer GetCharacterById(string id)
        {
            if (!characterDataContainerList.charactersMap.ContainsKey(id))
            {
                Debug.Log($"Character needs to be added to list");
                return null;
            }
            
            return characterDataContainerList.charactersMap[id];
        }
        
        public CharacterDataContainer GetCharacterByCharacterId(string id)
        {
            foreach (CharacterDataContainer characterDataContainer in characterDataContainerList.charactersMap.Values)
            {
                if (characterDataContainer.characterID == id)
                {
                    return characterDataContainer;
                }
            }

            return null;
        }
    }
}