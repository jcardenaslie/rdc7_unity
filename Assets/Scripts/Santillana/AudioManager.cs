using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDC.Audio
{
    public class AudioManager : MonoBehaviour 
    {
        private static AudioManager singleton;

        public static AudioManager Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = FindObjectOfType<AudioManager>();
                }

                return singleton;
            }
        }

        public void StopAllSounds()
        {
            AudioSource[] audioSourceArr = FindObjectsOfType<AudioSource>();

            for (int i = 0; i < audioSourceArr.Length; i++)
            {
                audioSourceArr[i].Stop();
            }
        }
    }
}

