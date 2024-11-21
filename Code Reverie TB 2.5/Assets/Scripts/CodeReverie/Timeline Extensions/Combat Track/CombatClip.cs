using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace CodeReverie
{
    public class CombatClip : PlayableAsset
    {

        public List<CharacterDataContainer> characterDataContainers;
        public SceneField sceneToLoad;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<CombatTrackBehavior>.Create(graph);

            CombatTrackBehavior combatTrackBehavior = playable.GetBehaviour();

            combatTrackBehavior.characterDataContainers = characterDataContainers;
            combatTrackBehavior.sceneToLoad = sceneToLoad;
            

            return playable;

        }
    }
}