using _Scripts.ECS.Components;
using _Scripts.MonoBehaviours;
using _Scripts.MonoBehaviours.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class GameTimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<TimeComponent> _timeFilter = null;
        private EcsFilter<CurrentGameInfoComponent> _currentGameInfoFilter = null;
        private EcsFilter<GameUIComponent> _gameUIFilter;

        private GameUI _gameUI;
        private CurrentGameInfo _currentGameInfo;

        public GameTimeSystem(EcsWorld world)
        {
            var timeEntity = world.NewEntity();
            timeEntity.Get<TimeComponent>();
        }

        public void Init()
        {
            SetCurrentGameInfo();
            SetGameUI();
        }

        public void Run()
        {
            SetTime();
        }

        private void SetTime()
        {
            foreach (var i in _timeFilter)
            {
                ref var timeComponent = ref _timeFilter.Get1(i);
                timeComponent.Time += Time.deltaTime;
                _currentGameInfo.PlayTime = timeComponent.Time;
                SetTimeText(timeComponent.Time);
            }
        }
        
        private void SetTimeText(float time)
        {
            string minutes = ((int)time / 60).ToString("00");
            string seconds = ((int)time % 60).ToString("00");
            string timeFormat = $"{minutes}:{seconds}";
            _gameUI.SetTimeText(timeFormat);
        }
        
        private void SetGameUI()
        {
            foreach (var i in _gameUIFilter)
            {
                ref var gameUIComponent = ref _gameUIFilter.Get1(i);
                _gameUI = gameUIComponent.GameUI;
            }
        }

        private void SetCurrentGameInfo()
        {
            foreach (var i in _currentGameInfoFilter)
            {
                ref var currentGameInfo = ref _currentGameInfoFilter.Get1(i);
                _currentGameInfo = currentGameInfo.CurrentGameInfo;
            }
        }
    }
}