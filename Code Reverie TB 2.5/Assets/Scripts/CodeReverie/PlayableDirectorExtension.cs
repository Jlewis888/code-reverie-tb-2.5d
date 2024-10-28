using System;
using Unity.Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CodeReverie
{
    
    [RequireComponent(typeof(PlayableDirector))]
    public class PlayableDirectorExtension : SerializedMonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.Instance.generalEvents.onStartCutscene += OnStartCutscene;
        }

        private void OnDisable()
        {
            EventManager.Instance.generalEvents.onStartCutscene -= OnStartCutscene;
        }

        private void Start()
        {
            TimelineAsset timeline = GetComponent<PlayableDirector>().playableAsset as TimelineAsset;

            CinemachineTrack cinemachineTrack;
            
            foreach (TrackAsset trackAsset in timeline.GetOutputTracks())
            {

                if (trackAsset.name == "Cinemachine Track")
                {
                    GetComponent<PlayableDirector>().SetGenericBinding(trackAsset, CameraManager.Instance.mainCamera.GetComponent<CinemachineBrain>());
                    
                    break;
                }
                //Debug.Log(trackAsset.name);
            }
            
           
            
        }


        private void OnStartCutscene(string obj)
        {
            
            if (obj == name)
            {
                GetComponent<PlayableDirector>().Play();
                GetComponent<PlayableDirector>().Pause();
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}