using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    [CreateAssetMenu(fileName = "AreaManagerData", menuName = "Scriptable Objects/Area Management/Area Manager Data", order = 1)]
    public class AreaManagerData : SerializedScriptableObject
    {
        public SceneField combatLocation;
        public Dictionary<Character, CharacterState> characterStateMap = new Dictionary<Character, CharacterState>();
        public List<Character> characterList = new List<Character>();
    }
}