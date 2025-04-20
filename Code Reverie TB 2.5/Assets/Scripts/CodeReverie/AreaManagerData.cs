using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "AreaManagerConfig", menuName = "Scriptable Objects/Area Management/Area Manager Config", order = 1)]
    public class AreaManagerConfig : SerializedScriptableObject
    {
        public SceneField combatLocation;
        public Dictionary<Character, CharacterState> characterStateMap = new Dictionary<Character, CharacterState>();
        public List<Character> characterList = new List<Character>();
        public bool hasCutsceneOnStart;
        public string cutsceneOnStart;
        public Dictionary<string, PlayableDirector> areaCutscenes;
    }
}