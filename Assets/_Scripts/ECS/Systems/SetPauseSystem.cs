using _Scripts.ECS.Components;
using _Scripts.MonoBehaviours.UI;
using Leopotam.Ecs;

namespace _Scripts.ECS.Systems
{
    public class SetPauseSystem : IEcsInitSystem, IEcsDestroySystem 
    {
        private EcsFilter<PauseComponent> _pauseFilter;
        private EcsFilter<AudioManagerComponent> _audioManagerFilter;
        private EcsFilter<GameUIComponent> _gameUIFilter;

        private GameUI _gameUI;

        public void Init()
        {
            SetGameUI();
        }

        public void Destroy()
        {
            _gameUI.OnSetPauseState -= SetPauseState;
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
        
        private void SetGameUI()
        {
            foreach (var i in _gameUIFilter)
            {
                ref var gameUIComponent = ref _gameUIFilter.Get1(i);
                _gameUI = gameUIComponent.GameUI;
                _gameUI.OnSetPauseState += SetPauseState;
            }
        }
    }
}