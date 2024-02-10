using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu]
    public class SoundsList : ScriptableObject
    {
        public List<AudioClip> _sceneAudioClip;
        public AudioClip _clickClip;
        public AudioClip _musicClip;
    }
}