using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace CodeReverie
{
    public class AreaManager : SerializedMonoBehaviour
    {
        public static AreaManager instance;
        public AreaManagerConfig areaManagerConfig;
        public bool ignoreAreaManagerConfig;

        public AreaType areaType;
        public Transform defaultAreaSpawnPoint;
        public Transform areaSpawnPoint;
        public Collider areaCameraConfiner;

        public int spawnPointIndex;
        public List<Transform> spawnPoints;
        public List<Transform> spawnPoints2;
        public List<Transform> spawnPoints3;

        public List<AreaEntrance> areaEntrances;
        public bool sceneAutoSaved;
        public float autoSaveDelay;

        public Tilemap mapBase;
        public string audioClip;
        public SceneField combatLocation;
        public SceneField persistentData;

        private void Awake()
        {

            // if (persistentData != null)
            // {
            //     SceneManager.LoadSceneAsync(persistentData, LoadSceneMode.Additive);
            // }
            
            
            instance = this;
            autoSaveDelay = 5f;


            // GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            // GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 0);
            // areaManagerData.characterList =
            //     GetComponentsInChildren<CharacterUnitController>().ToList().Select(x => x.character).ToList();
            //
            //
            // int instanceID = GetInstanceID();
            
            
            GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 0);
            
            CameraManager.Instance.SetCameraConfiner(areaCameraConfiner);
        }

#if UNITY_EDITOR
        [Button("Set Character IDs")]
        public void SetCharacterIDs()
        {
            //TODO MAKE SURE ALL IS UNIQUE
            GetComponentsInChildren<CharacterUnitController>().ToList().ForEach(x => x.SetInstanceID());
        }
#endif


        private void OnEnable()
        {
            //Debug.Log(EventManager.Instance);
            //EventManager.Instance.playerEvents.onPlayerSpawn += SpawnPlayer;
            
        }

        private void OnDisable()
        {
            //EventManager.Instance.playerEvents.onPlayerSpawn -= SpawnPlayer;
        }


        private void Start()
        {
            //CameraManager.Instance.SetCameraConfiner(areaCameraConfiner);


            //SpawnPlayerCharacterUnits();

            //CanvasManager.Instance.screenSpaceCanvasManager.hudManager.gameObject.SetActive(true);
            // EventManager.Instance.generalEvents.OpenMenuManager(CanvasManager.Instance.screenSpaceCanvasManager.hudManager);


            // foreach (AreaEntrance areaEntrance in areaEntrances)
            // {
            //     if (areaEntrance.transitionName == SceneTransitionManager.Instance.SceneTransitionName)
            //     {
            //         PlayerController.Instance.transform.position = areaEntrance.transform.position;
            //         SceneTransitionManager.Instance.isTransitioningScenes = false;
            //         break;
            //     }
            // }
            
            
            
            
            if (areaManagerConfig != null)
            {
                if (areaManagerConfig.hasCutsceneOnStart && !ignoreAreaManagerConfig)
                {
                    EventManager.Instance.generalEvents.OnStartCutscene(areaManagerConfig.cutsceneOnStart);
                }
                else
                {
                    Init();
                }
            }
            else
            {
                Init();
            }
        }

        private void Update()
        {
            if (autoSaveDelay <= 0)
            {
                AutoSaveScene();
            }
            else
            {
                autoSaveDelay -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                SpawnPlayerParty();
            }
        }

        public void Init()
        {
            


            if (GameManager.Instance.newGame)
            {
                DataPersistenceManager.Instance.AutoSave();
            }

            // if (GameSceneManager.Instance.fromLoadedData)
            // {
            //     DataPersistenceManager.Instance.AutoSave();
            // }

            if (!string.IsNullOrEmpty(audioClip))
            {
                SoundManager.Instance.PlayMusic(audioClip);
            }


            GameManager.Instance.newGame = false;


            
        }
        
        public void AutoSaveScene()
        {
            if (!sceneAutoSaved)
            {
                if (!GameSceneManager.Instance.fromLoadedData)
                {
                    sceneAutoSaved = true;
                    DataPersistenceManager.Instance.AutoSave();
                }
            }
        }


        public void SpawnPlayerParty()
        {
            
            if (!GameManager.Instance.newGame)
            {
                PlayerManager.Instance.SetPartyUnits(PlayerManager.Instance.characterOnSaveLoadPosition);
            }
            else
            {
                PlayerManager.Instance.SetPartyUnits(defaultAreaSpawnPoint != null
                    ? defaultAreaSpawnPoint.position
                    : Vector3.zero);
            }
            
            if (PlayerManager.Instance.combatConfigDetails != null)
            {
                CanvasManager.Instance.victoryPopup.SetActive(false);


                TransitionAnimator.Start(
                    TransitionType.Wipe, // transition type
                    duration: 1f,
                    invert: true
                );


                CanvasManager.Instance.screenSpaceCanvasManager.hudManager.combatHudManager.gameObject
                    .SetActive(false);
                CameraManager.Instance.UnsetBattleCamera();
                CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController
                    .transform);
                Destroy(GetComponentsInChildren<CharacterUnitController>().ToList().Find(x =>
                        x.characterInstanceID == PlayerManager.Instance.combatConfigDetails.characterInstanceID)
                    .gameObject);

                PlayerManager.Instance.SetPartyReturnPosition();
                PlayerManager.Instance.combatConfigDetails = null;
            }
            
            CameraManager.Instance.UpdateCamera(PlayerManager.Instance.currentParty[0].characterController
                .transform);
        }

        public void SpawnPlayer()
        {
            // if (!SceneTransitionManager.Instance.isTransitioningScenes)
            // {
            //     SpawnPlayerCharacterUnits();
            // }
            // else
            // {
            //
            //     if (!SceneTransitionManager.Instance.fromLoadedData)
            //     {
            //         
            //         foreach (AreaEntrance areaEntrance in areaEntrances)
            //         {
            //             if (areaEntrance.transitionName == SceneTransitionManager.Instance.SceneTransitionName)
            //             {
            //                 PlayerManager.Instance.SelectedCharacter.characterUnit.transform.position =
            //                     areaEntrance.transform.position;
            //                 SceneTransitionManager.Instance.isTransitioningScenes = false;
            //                 break;
            //             }
            //         }
            //        
            //     }
            //     else
            //     {
            //         SpawnPlayerCharacterUnits();
            //         SceneTransitionManager.Instance.fromLoadedData = false;
            //         GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
            //         GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 0);
            //         PlayerManager.Instance.SelectedCharacter.characterUnit.transform.position = DataPersistenceManager.Instance.GetPlayerPosition();
            //     }
            // }
        }


        // public void LoadData(GameData data)
        // {
        //   
        //     // if (SceneTransitionManager.Instance.fromLoadedData)
        //     // {
        //     //   
        //     //     SpawnPlayerCharacterUnitsType1();
        //     //     SceneTransitionManager.Instance.fromLoadedData = false;
        //     //     GameManager.Instance.playerInput.controllers.maps.SetAllMapsEnabled(false);
        //     //     GameManager.Instance.playerInput.controllers.maps.SetMapsEnabled(true, 0);
        //     //     PlayerManager.Instance.selectedCharacter.characterUnit.transform.position = data.playerPosition;
        //     // }
        //
        //     
        // }
        //
        // public void SaveData()
        // {
        //     if (!SceneTransitionManager.Instance.fromLoadedData)
        //     {
        //         // data.playerPosition = PlayerManager.Instance.selectedCharacter.characterUnit.transform.position;
        //         
        //         DataPersistence.Save("playerPosition", PlayerManager.Instance.SelectedCharacter.characterUnit.transform.position);
        //         
        //     }
        // }
    }
}