using _Scripts.ECS.Components;
using _Scripts.MonoBehaviours.Settings;
using Leopotam.Ecs;
using Lofelt.NiceVibrations;

namespace _Scripts.ECS.Systems
{
    public class PlayVibrationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<PlayVibrationOneFrame> _vibrationFilter;
        private EcsFilter<VibrationClipsComponent> _vibrationClipsFilter;
        private EcsFilter<SettingsInfoComponent> _settingsInfoFilter;

        private SettingsInfo _settingsInfo;

        public void Init()
        {
            SetSettingsInfo();
        }

        public void Run()
        {
            foreach (var i in _vibrationFilter)
                PlayVibration();
        }

        private void PlayVibration()
        {
            if (_settingsInfo.IsVibrationEnabled)
            {
                foreach (var i in _vibrationClipsFilter)
                {
                    ref var vibrationClipsComponent = ref _vibrationClipsFilter.Get1(i);
                    HapticController.fallbackPreset = HapticPatterns.PresetType.LightImpact;
                    HapticController.Play(vibrationClipsComponent.Clip);
                }
            }
        }

        private void SetSettingsInfo()
        {
            foreach (var i in _settingsInfoFilter)
            {
                ref var settingsInfoComponent = ref _settingsInfoFilter.Get1(i);
                _settingsInfo = settingsInfoComponent.SettingsInfo;
            }
        }
    }
}