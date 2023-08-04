using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.MonoBehaviours.Settings;
using UnityEngine;

namespace _Scripts.MonoBehaviours.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<Audio> _soundsList;
        [SerializeField] private List<Audio> _musicList;

        private SettingsInfo _settingsInfo;
        private AudioSource _musicSource;
        private List<AudioSource> _currentAudioSources;
        private Audio _currentBackgroundMusic;

        private int _backgroundMusicNumber = -1;
        private bool _isMusicLoop;
        private bool _isSoundsPause;

        public void Init(SettingsInfo settingsInfo)
        {
            _settingsInfo = settingsInfo;
            _settingsInfo.OnMusicStateChanged += SetMusicState;
            InitializeSources();
            PlayMusic();
        }

        public void PlayMusic()
        {
            if (_settingsInfo.IsMusicEnabled)
            {
                StopMusic();
                _musicSource.volume = 1;
                _isMusicLoop = true;
                _currentBackgroundMusic = GetBackgroundMusic();
                SetMusicSource();
            }
        }

        public void StopMusic()
        {
            _isMusicLoop = false;
            _musicSource.Stop();
            _musicSource.volume = 0;
        }

        public void PlayEffect(AudioClip clip)
        {
            if (_settingsInfo.IsSoundsEnabled)
            {
                Audio currentAudio = _soundsList.Where(s => s.Clip == clip).FirstOrDefault();
                if (currentAudio != null)
                {
                    if (!currentAudio.IsRandomPitch)
                    {
                        currentAudio.AudioSource.pitch = currentAudio.Pitch;
                    }
                    else
                    {
                        float pitchSpread = 0.1f;
                        currentAudio.AudioSource.pitch = currentAudio.Pitch + Random.Range(-pitchSpread, pitchSpread);
                    }

                    currentAudio.AudioSource.volume = currentAudio.Volume;
                    currentAudio.AudioSource.PlayOneShot(currentAudio.Clip);
                }
                else
                {
                    Debug.LogError($"Effect {clip.name} not found!");
                }
            }
        }

        public void StopAllEffects()
        {
            foreach (var audioSource in _currentAudioSources)
                audioSource.Stop();
        }

        public void StopMusicGradually()
        {
            StartCoroutine(ReduceMusicVolume());
        }

        private void SetMusicState(bool isEnabled)
        {
            if (isEnabled)
                PlayMusic();
            else
                StopMusic();
        }

        private void InitializeSources()
        {
            _currentAudioSources = new List<AudioSource>();
            _musicSource = gameObject.AddComponent<AudioSource>();
            foreach (var sounds in _soundsList)
            {
                sounds.AudioSource = gameObject.AddComponent<AudioSource>();
                sounds.AudioSource.clip = sounds.Clip;
                sounds.AudioSource.volume = sounds.Volume;
                sounds.AudioSource.reverbZoneMix = 0;
                sounds.AudioSource.time = sounds.StartTime;
                sounds.AudioSource.pitch = sounds.Pitch;
            }
        }

        private void SetMusicSource()
        {
            _musicSource.volume = _currentBackgroundMusic.Volume;
            _musicSource.clip = _currentBackgroundMusic.Clip;
            _musicSource.Play();
        }

        private void Update()
        {
            if (!_musicSource.isPlaying && _isMusicLoop)
                PlayMusic();

            if (!_isSoundsPause && Time.timeScale == 0)
            {
                foreach (var audioSource in _currentAudioSources)
                    audioSource.Pause();

                _isSoundsPause = true;
            }

            if (_isSoundsPause && Time.timeScale > 0)
            {
                foreach (var audioSource in _currentAudioSources)
                    audioSource.Play();

                _isSoundsPause = false;
            }
        }

        private IEnumerator ReduceMusicVolume()
        {
            while (_musicSource.volume != 0)
            {
                _musicSource.volume -= Time.fixedDeltaTime;
                yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
            }
        }

        private Audio GetBackgroundMusic()
        {
            if (_musicList.Count != 1)
            {
                int number = Random.Range(0, _musicList.Count);
                while (_backgroundMusicNumber == number)
                    number = Random.Range(0, _musicList.Count);

                _backgroundMusicNumber = number;
                return _musicList[_backgroundMusicNumber];
            }

            return _musicList[0];
        }
    }
}