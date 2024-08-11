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
            ChangeMasterVolume(0.5f);
            ChangeEffectsVolume(0.25f);
            ChangeMusicVolume(0.25f);
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
        
    
        public void ChangeMasterVolume(float value)
        {
            AudioListener.volume = value;
        }
    
        public void ChangeMusicVolume(float value)
        {
            musicSource.volume = value;
        }
    

        public void ChangeEffectsVolume(float value)
        {
            effectsSource.volume = value;
        }
        
        
    }
}