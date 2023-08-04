using _Scripts.MonoBehaviours.Audio;
using _Scripts.MonoBehaviours.Settings;
using UnityEngine;

namespace _Scripts.MonoBehaviours
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SettingsInfo _settingsInfo;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private ECSBoostrap _ecsBoostrap;
        
        private void Awake()
        {
            _settingsInfo.Init();
            _audioManager.Init(_settingsInfo);
            _ecsBoostrap.Init();
        }
    }
}