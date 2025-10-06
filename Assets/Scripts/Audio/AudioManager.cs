using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager:MonoBehaviour
    {
        public static AudioManager Instance;
        public AudioSource FXsource;
        private void Start()
        {
            DontDestroyOnLoad(this);
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySound(string soundName)
        {
            AudioClip clip=Resources.Load<AudioClip>("Audios/"+soundName);
            FXsource.PlayOneShot(clip);
        }
    }
}