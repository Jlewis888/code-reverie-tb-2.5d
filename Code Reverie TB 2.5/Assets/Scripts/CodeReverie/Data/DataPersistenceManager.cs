using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    [DefaultExecutionOrder(-119)]
    public class DataPersistenceManager : ManagerSingleton<DataPersistenceManager>
    {
        
        public string currentSaveFile;
        
        [SerializeField]
        private List<IDataPersistence> dataPersistenceObjects;
        public bool isDataLoaded;
        public bool debugging;
        
        
        protected override void Awake()
        {
            base.Awake();

            if (debugging)
            {

                if (Instance == null)
                {
                    dataPersistenceObjects = FindAllDataPersistenceObjects();
                    LoadGame(0);
                    debugging = false;
                    Debug.Log("we are testing");
                }

               
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
        
        public void OnSceneUnloaded(Scene scene)
        {
            
            Debug.Log("Scene Unloaded");
            Debug.Log(SceneManager.GetActiveScene().name);
            GameSceneManager.Instance.prevScene = SceneManager.GetActiveScene().name;
            //AutoSave();
        }

        public void AutoSave()
        {
            // foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
            // {
            //     dataPersistenceObject.SaveData($"0/SaveFile.es3");
            // }
            
            SaveGame(0);
            
        }


        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene Loaded");
            Debug.Log(SceneManager.GetActiveScene().name);
            dataPersistenceObjects = FindAllDataPersistenceObjects();
            
        }
        
        
        List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects =
                FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
        
        public void NewGame()
        {

            if (ES3.FileExists($"{0}/SaveFile.es3"))
            {
                
                ES3.DeleteFile($"{0}/SaveFile.es3");

            }
            
            // ES3.Save("myKey", "", $"{0}/SaveFile.es3");
            //     
            // ES3.DeleteKey("myKey", $"{0}/SaveFile.es3");

            LoadGame(0);
        }
        
        
        public void LoadGame(int dataSlot)
        {
            
            // gameData = dataHandler.Load(profileId);
            //
            // if (gameData == null & initializeDataIfNull)
            // {
            //     NewGame();
            // }
            //
            // if (gameData == null)
            // {
            //     Debug.Log("No data was found. A New Game needs to be started before data can be loaded");
            //     return;
            // }
            
            
            if (!SaveFileExist(dataSlot) && !debugging)
            {
                
                Debug.Log("No data was found. A New Game needs to be started before data can be loaded");
                return;

            }


            foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
            {
                dataPersistenceObject.LoadData($"{dataSlot}/SaveFile.es3");
            }
            
            //EventManager.Instance.generalEvents.OnLoad();
        }

        public bool SaveFileExist(int dataSlot)
        {
            if (ES3.FileExists($"{dataSlot}/SaveFile.es3"))
            {
                
                return true;

            }
            
            return false;
        }
        
        
        public void SaveGame(int dataSlot)
        {
            
            // if (gameData == null)
            // {
            //     Debug.Log("No data was found. A New Game needs to be started before data can be saved");
            //     return;
            // }
            
            
            if (!ES3.FileExists($"{dataSlot}/SaveFile.es3"))
            {
                ES3.Save("myKey", "", $"{dataSlot}/SaveFile.es3");
                
                ES3.DeleteKey("myKey", $"{dataSlot}/SaveFile.es3");

            }
            
            
            foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
            {
                dataPersistenceObject.SaveData($"{dataSlot}/SaveFile.es3");
            }
            
        }
        
    }
}