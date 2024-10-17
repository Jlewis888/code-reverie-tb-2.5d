using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeReverie
{
    
    
    [DefaultExecutionOrder(-115)]
    public class GameSceneManager : ManagerSingleton<GameSceneManager>, IDataPersistence
    {

        public string currentGameScene;
        public string prevScene;
        public bool isTransitioningScenes;
        public bool fromLoadedData;
        
        protected override void Awake()
        {
           base.Awake();
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
            
            prevScene = SceneManager.GetActiveScene().name;
           
        }
        
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            currentGameScene = SceneManager.GetActiveScene().name;
            fromLoadedData = false;
            CanvasManager.Instance.screenSpaceCanvasManager.hudManager.gameObject.SetActive(true);
            CanvasManager.Instance.screenSpaceCanvasManager.gameOverPanel.gameObject.SetActive(false);
        }
        
        
        
        public string SceneTransitionName { get; private set; }

        public void SetTransitionName(string sceneTransitionName)
        {
            SceneTransitionName = sceneTransitionName;
        }
        
        public bool Initialized { get; set; }

        public bool IsCurrentScene(string sceneName)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                return true;
            }
            
            return false;
        }
        
        public void LoadData(string dataSlot) { }

        public void SaveData(string dataSlot)
        {
            ES3.Save("currentScene", SceneManager.GetActiveScene().name, dataSlot);
        }
        
        
        public async void LoadScene()
        {
            var scene = SceneManager.LoadSceneAsync("HomeScene");
            scene.allowSceneActivation = false;
            
            //downloadingObject.SetActive(true);

            do
            {
                await Task.Delay(100);
                //downloadValue = scene.progress;
            } while (scene.progress < 0.9f);

            scene.allowSceneActivation = true;

            //await Task.Delay(1000);


            // downloadingObject.SetActive(false);
            // startGameObject.SetActive(true);

        }
        
    }
}