using System;
using UnityEngine;

namespace _Scripts.MonoBehaviours.Audio
{
    [Serializable]
    public class Audio
    {
        [SerializeField] private AudioClip _clip;
        [Range(0f, 1f)] [SerializeField] private float _volume;
        [Range(.1f, 3f)] [SerializeField] private float _pitch;
        [SerializeField] private float _startTime;
        [SerializeField] private bool _isRandomPitch;
        [HideInInspector] [SerializeField] private AudioSource _audioSource;

        public AudioClip Clip => _clip;

        public float Volume => _volume;

        public float Pitch => _pitch;

        public float StartTime => _startTime;

        public bool IsRandomPitch => _isRandomPitch;

        public AudioSource AudioSource
        {
            get => _audioSource;
            set => _audioSource = value;
        }
    }
}