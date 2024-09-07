using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeReverie
{
    public class AudioSettingsMenuManager : SerializedMonoBehaviour
    {
        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider effectsVolumeSlider;
        //public Button closePopupButton;

        

        private void OnEnable()
        {
            
            Debug.Log(SoundManager.Instance.MasterVolume);
            
            masterVolumeSlider.value = SoundManager.Instance.MasterVolume * 10;
            musicVolumeSlider.value = SoundManager.Instance.MusicVolume * 10;
            effectsVolumeSlider.value = SoundManager.Instance.EffectsVolume * 10;
            
            
            Screen.SetResolution(640, 480, true);
        }
        
        private void Start()
        {
            masterVolumeSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
            musicVolumeSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
            effectsVolumeSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeEffectsVolume(val));
            //closePopupButton.onClick.AddListener(()=>gameObject.SetActive(false));
        }

        
        
    }
}