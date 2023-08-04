using _Scripts.Saves;
using UnityEngine;

namespace _Scripts.MonoBehaviours.Settings
{
    public class SettingsInfo : MonoBehaviour
    {
        private const string SettingsFile = "Settings.txt";

        private SettingsData _settingsData;
        private SaveSystem _saveSystem;

        public delegate void MusicStateChanged(bool isEnabled);

        public event MusicStateChanged OnMusicStateChanged;

        public bool IsMusicEnabled
        {
            get { return _settingsData.IsMusicEnabled; }

            set
            {
                _settingsData.IsMusicEnabled = value;
                OnMusicStateChanged?.Invoke(_settingsData.IsMusicEnabled);
                Save();
            }
        }

        public bool IsSoundsEnabled
        {
            get { return _settingsData.IsSoundsEnabled; }
            set
            {
                _settingsData.IsSoundsEnabled = value;
                Save();
            }
        }

        public bool IsVibrationEnabled
        {
            get { return _settingsData.IsVibrationEnabled; }
            set
            {
                _settingsData.IsVibrationEnabled = value;
                Save();
            }
        }

        public void Init()
        {
            _saveSystem = new SaveSystem();
            Load();
        }

        private void Load()
        {
            _settingsData = new SettingsData();
            string currentSettings = _saveSystem.Load(SettingsFile);
            if (currentSettings != string.Empty)
                _settingsData = JsonUtility.FromJson<SettingsData>(currentSettings);

            Save();
        }

        private void Save()
        {
            string currentSettings = JsonUtility.ToJson(_settingsData);
            _saveSystem.Save(currentSettings, SettingsFile);
        }
    }
}