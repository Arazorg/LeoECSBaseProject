using _Scripts.ECS.Components;
using _Scripts.MonoBehaviours;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using TMPro;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class GameTimeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const string TimeTextName = "TimeText";

        private EcsFilter<TimeComponent> _timeFilter = null;
        private EcsFilter<CurrentGameInfoComponent> _currentGameInfoFilter = null;

        private EcsUiEmitter _ui;
        [EcsUiNamed(TimeTextName)] private TextMeshProUGUI _timeText;

        private CurrentGameInfo _currentGameInfo;

        public GameTimeSystem(EcsWorld world)
        {
            var timeEntity = world.NewEntity();
            timeEntity.Get<TimeComponent>();
        }

        public void Init()
        {
            SetCurrentGameInfo();
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

        private void SetCurrentGameInfo()
        {
            foreach (var i in _currentGameInfoFilter)
            {
                ref var currentGameInfo = ref _currentGameInfoFilter.Get1(i);
                _currentGameInfo = currentGameInfo.CurrentGameInfo;
            }
        }
        
        private void SetTimeText(float time)
        {
            string minutes = ((int)time / 60).ToString("00");
            string seconds = ((int)time % 60).ToString("00");
            string timeFormat = $"{minutes}:{seconds}";
            _timeText.text = timeFormat;
        }
    }
}