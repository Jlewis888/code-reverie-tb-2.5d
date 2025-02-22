using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeReverie
{
    public class PreloadManager : SerializedMonoBehaviour
    {
        public SceneField persistentData;
        public SceneField titleScreen;

        private void Awake()
        {
            SceneManager.LoadSceneAsync(persistentData, LoadSceneMode.Additive);
            
        }        
    }
}