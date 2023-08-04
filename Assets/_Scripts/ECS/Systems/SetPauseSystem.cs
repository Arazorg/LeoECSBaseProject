using _Scripts.ECS.Components;
using _Scripts.MonoBehaviours.UI;
using Leopotam.Ecs;

namespace _Scripts.ECS.Systems
{
    public class SetPauseSystem : IEcsSystem
    {
        private EcsFilter<PauseComponent> _pauseFilter = null;
        private EcsFilter<AudioManagerComponent> _audioManagerFilter = null;

        public SetPauseSystem(GameUI gameUI)
        {
            gameUI.OnSetPauseState += SetPauseState;
        }

        private void SetPauseState(bool state)
        {
            foreach (var i in _pauseFilter)
            {
                ref var pauseComponent = ref _pauseFilter.Get1(i);
                pauseComponent.IsGamePausing = state;
            }

            SetMusicState(state);
        }

        private void SetMusicState(bool isEnabled)
        {
            foreach (var i in _audioManagerFilter)
            {
                ref var audioManagerComponent = ref _audioManagerFilter.Get1(i);
                if (isEnabled)
                    audioManagerComponent.AudioManager.StopMusic();
                else
                    audioManagerComponent.AudioManager.PlayMusic();
            }
        }
    }
}