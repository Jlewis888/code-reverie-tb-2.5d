using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    public class SpawnPlayerTrackBehavior : PlayableBehaviour
    {
        public bool hasPlayed;
        public GameObject spawnLocation;
        public GameObject timeline;

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
                PlayerManager.Instance.SetPartyUnits(spawnLocation.transform.position);
                CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController
                    .transform);
                timeline.SetActive(false);
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