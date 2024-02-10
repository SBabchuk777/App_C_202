using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class SoundsController : MonoBehaviour
    {
        [SerializeField] 
        private List<AudioSource> _audioSources;

        public void PlaySound(AudioClip clip)
        {
            int index = _audioSources[0].isPlaying ? 1 : 0;

            _audioSources[index].clip = clip;
            _audioSources[index].Play();
        }
    }
}