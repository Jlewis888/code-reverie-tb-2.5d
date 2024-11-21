using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace CodeReverie
{
    public class SpawnPlayerClip : PlayableAsset
    {

        public GameObject spawnLocation;
        public GameObject timeline;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SpawnPlayerTrackBehavior>.Create(graph);

            SpawnPlayerTrackBehavior trackBehavior = playable.GetBehaviour();

            trackBehavior.spawnLocation = spawnLocation;
            trackBehavior.timeline = timeline;
            

            return playable;

        }
    }
}