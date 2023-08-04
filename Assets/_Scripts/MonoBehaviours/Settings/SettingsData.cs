using System;

namespace _Scripts.MonoBehaviours.Settings
{
    [Serializable]
    public class SettingsData
    {
        public bool IsMusicEnabled;
        public bool IsSoundsEnabled;
        public bool IsVibrationEnabled;

        public SettingsData()
        {
            IsMusicEnabled = true;
            IsSoundsEnabled = true;
            IsVibrationEnabled = true;
        }
    }
}