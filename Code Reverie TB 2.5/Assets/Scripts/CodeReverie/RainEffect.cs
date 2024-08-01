using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeReverie
{
    public class RainEffect : SerializedMonoBehaviour
    {
        
        public string rainClip = "rain_heavy_loop_01";
        
        
        private void Start()
        {
            
            SoundManager.Instance.PlaySoundLoop(rainClip);
        }
    }
}