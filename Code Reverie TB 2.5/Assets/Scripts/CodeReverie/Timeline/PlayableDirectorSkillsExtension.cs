using System;
using Unity.Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

namespace CodeReverie
{
    
    [RequireComponent(typeof(PlayableDirector))]
    public class PlayableDirectorSkillExtension : SerializedMonoBehaviour
    {
        public GameObject testObject;
        
        private void OnEnable()
        {
            //EventManager.Instance.generalEvents.onStartCutscene += OnStartCutscene;
        }

        private void OnDisable()
        {
            //EventManager.Instance.generalEvents.onStartCutscene -= OnStartCutscene;
        }

        private void Start()
        {
            GetComponent<PlayableDirector>().Pause();
            TimelineAsset timeline = GetComponent<PlayableDirector>().playableAsset as TimelineAsset;

            CinemachineTrack cinemachineTrack;

            GameObject animatedObject = Instantiate(testObject, transform);
            //animatedObject.transform.position = CombatManager.Instance.selectedSkillPlayerCharacter.transform.position;
            
            foreach (TrackAsset trackAsset in timeline.GetOutputTracks())
            {
                
                if (trackAsset.name == "Animation Track")
                {
                    Debug.Log("Found Animation Track");

                    if (trackAsset is AnimationTrack animationTrack)
                    {
                        
                        Vector3 worldTarget = CombatManager.Instance.selectedSkillPlayerCharacter.transform.position;
                        Vector3 localOffset = animatedObject.transform.InverseTransformPoint(worldTarget);
                        
                        animationTrack.trackOffset = TrackOffset.ApplyTransformOffsets;
                        //animationTrack.infiniteClipOffsetPosition = Vector3.zero;
                        animationTrack.infiniteClipOffsetPosition = localOffset;
                        GetComponent<PlayableDirector>().SetGenericBinding(trackAsset, animatedObject.GetComponent<Animator>());
                        CameraManager.Instance.SetSkillCameraFollow(animatedObject);
                        
                        //animationTrack.infiniteClipOffsetPosition = new Vector3(0f, 4f, 0f);  // Example offset
                        // animationTrack.infiniteClipOffsetPosition =
                        //     CombatManager.Instance.selectedSkillPlayerCharacter.transform.position;
                        //animationTrack.openClipOffsetRotation = new Vector3(0f, 90f, 0f); // Example rotation offset
                    }
                }

                if (trackAsset.name == "Casting Fire" || trackAsset.name == "Projectile Fire")
                {
                    Object bindingObject = GetComponent<PlayableDirector>().GetGenericBinding(trackAsset);

                    if (bindingObject != null)
                    {
                        GameObject bindingGameObject = null;


                        if (bindingObject is GameObject castingGameObject)
                        {
                            bindingGameObject = castingGameObject;


                            bindingGameObject.transform.position =
                                CombatManager.Instance.skillObjectSpawnPoint1.transform.position;

                        }
                    }

                }
                

                if (trackAsset.name == "Cinemachine Track")
                {
                    GetComponent<PlayableDirector>().SetGenericBinding(trackAsset, CameraManager.Instance.mainCamera.GetComponent<CinemachineBrain>());
                    
                    //break;
                }

                
            }
            
            GetComponent<PlayableDirector>().Play();
            
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