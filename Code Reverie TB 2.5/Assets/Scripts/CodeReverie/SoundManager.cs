using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class SoundManager : ManagerSingleton<SoundManager>
    {
        public AudioDataList audioDataList;
        public AudioSource musicSource, effectsSource;
        public AudioClip buttonClick1;
        public AudioClip buttonClick2;
        public AudioClip pistolShot1;
        
        public List<AudioClip> swordHit1;
        public Dictionary<string, AudioClipManager> audioClipManagers;

        
        
        protected override void Awake()
        {
            base.Awake();
            ChangeMasterVolume(2f);
            ChangeEffectsVolume(5f);
            ChangeMusicVolume(5f);
            Initialize();
            audioClipManagers = new Dictionary<string, AudioClipManager>();
        }

        private void Update()
        {
            if (audioClipManagers != null)
            {
                if (audioClipManagers.Count > 0)
                {
                    
                }
            }
        }

        public bool Initialized { get; set; }
        
        public void Initialize()
        {
            Initialized = true;
        }

        public void PlayMusic(string clip)
        {
            if (audioDataList.audioClipMap.ContainsKey(clip))
            {
                PlayMusic(audioDataList.audioClipMap[clip]);
            }
        }
        

        public void PlayMusic(AudioClip clip)
        {
            //musicSource.Stop();

            if (clip == musicSource.clip && musicSource.isPlaying)
            {
                return;
            }
            
            musicSource.clip = clip;
            musicSource.Play();
            musicSource.loop = true;
        }
        
        public void PlayOneShotSound(string clip, bool playSingle = false)
        {

            if (audioDataList.audioClipMap.ContainsKey(clip))
            {
                if (playSingle)
                {
                    if (audioClipManagers.ContainsKey(clip))
                    {
                        if (audioClipManagers[clip].canPlaySound)
                        {
                            audioClipManagers[clip].canPlaySound = false;
                            StartCoroutine(audioClipManagers[clip].CountDown());
                            effectsSource.PlayOneShot(audioDataList.audioClipMap[clip]);
                        }
                        
                    }
                    else
                    {
                        AudioClipManager audioClipManager =
                            new AudioClipManager(clip, audioDataList.audioClipMap[clip].length);
                        
                        audioClipManagers.Add(clip, audioClipManager);
                        StartCoroutine(audioClipManager.CountDown());
                        effectsSource.PlayOneShot(audioDataList.audioClipMap[clip]);
                    }
                }
                else
                {
                    effectsSource.PlayOneShot(audioDataList.audioClipMap[clip]);  
                }
                
            }
            
            
        }
    
        public void PlayOneShotSound(AudioClip clip)
        {
            effectsSource.PlayOneShot(clip);
        }
        
        
        public void PlaySoundLoop(string clip)
        {

            if (audioDataList.audioClipMap.ContainsKey(clip))
            {
                effectsSource.clip = audioDataList.audioClipMap[clip];
                effectsSource.Play();
                effectsSource.loop = true;
            }
            
            
        }
        
        public void PlaySoundLoop(AudioClip clip)
        {
            effectsSource.clip = clip;
            effectsSource.Play();
            effectsSource.loop = true;
        }

        public void PlayButtonClick1()
        {
            effectsSource.PlayOneShot(buttonClick1);
        }

        public void PlayButtonClick2()
        {
            effectsSource.PlayOneShot(buttonClick2);
        }
        
        public void PlayButtonPistolShot1()
        {
            effectsSource.PlayOneShot(pistolShot1);
        }

        public float MasterVolume
        {
            get { return AudioListener.volume; }
            set
            {
                ChangeMasterVolume(value);
            }
        }
        
        public float MusicVolume
        {
            get { return musicSource.volume; }
            set
            {
                ChangeMusicVolume(value);
            }
        }
        
        public float EffectsVolume
        {
            get { return effectsSource.volume; }
            set
            {
                ChangeEffectsVolume(value);
            }
        }
        
    
        public void ChangeMasterVolume(float value)
        {
            float normalizedValue = value / 10;
            
            AudioListener.volume = normalizedValue;
        }
    
        public void ChangeMusicVolume(float value)
        {
            float normalizedValue = value / 10;
            musicSource.volume = normalizedValue;
        }
    

        public void ChangeEffectsVolume(float value)
        {
            float normalizedValue = value / 10;
            effectsSource.volume = normalizedValue;
        }
        
        
    }
}