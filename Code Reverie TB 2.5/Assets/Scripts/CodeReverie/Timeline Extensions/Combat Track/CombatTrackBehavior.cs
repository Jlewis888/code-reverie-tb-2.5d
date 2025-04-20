using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    public class CombatTrackBehavior : PlayableBehaviour
    {
        public bool hasPlayed;
        public List<CharacterDataContainer> characterDataContainers;
        public SceneField sceneToLoad;
        public SceneField returnSceneName;

        // public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        // {
        //     
        //     
        //     
        //     
        // }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            
            if (!hasPlayed  && Application.isPlaying)
            {
                CombatConfigDetails combatConfigDetails= new CombatConfigDetails(
                    returnSceneName: returnSceneName.SceneName,
                    characterReturnPosition: Vector3.zero,
                    enemyList: characterDataContainers
                );
                
                PlayerManager.Instance.StartCombat(combatConfigDetails, sceneToLoad.SceneName);
                hasPlayed = true;
                playable.GetGraph().Stop();
            }
        }

        public override void OnGraphStart(Playable playable)
        {
            hasPlayed = false;
        }
    }
}