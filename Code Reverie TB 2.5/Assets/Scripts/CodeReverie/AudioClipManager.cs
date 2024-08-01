using System.Collections;
using UnityEngine;

namespace CodeReverie
{
    public class AudioClipManager
    {
        public string clipName;
        public float length;
        public float timer;
        public bool canPlaySound;

        public AudioClipManager(string clipName, float length)
        {
            this.clipName = clipName;
            this.length = length;
        }

        public IEnumerator CountDown()
        {
            yield return new WaitForSeconds(length);
            canPlaySound = true;
        }

    }
}