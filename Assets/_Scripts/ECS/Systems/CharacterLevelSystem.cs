using System;
using _Scripts.ECS.Components;
using Leopotam.Ecs;

namespace _Scripts.ECS.Systems
{
    public class CharacterLevelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<CharacterTag, LevelComponent> _playerLevelFilter = null;

        private int _previousNeedExperience;

        public void Init()
        {
            SetStartValues();
        }

        public void Run()
        {
            foreach (var i in _playerLevelFilter)
            {
                ref var levelComponent = ref _playerLevelFilter.Get2(i);
                ref var level = ref levelComponent.Level;
                ref var experience = ref levelComponent.Experience;
                ref var needLevelExperience = ref levelComponent.NeedLevelExperience;

                if (experience >= needLevelExperience)
                {
                    levelComponent.Level++;
                    SetNeedLevelExperience();
                }
            }
        }

        private void SetNeedLevelExperience()
        {
            const int experienceForLevel = 10;
            const float levelCoefficient = 1.1f;
            foreach (var i in _playerLevelFilter)
            {
                ref var levelComponent = ref _playerLevelFilter.Get2(i);
                levelComponent.NeedLevelExperience += (int)(experienceForLevel *
                                                            Math.Pow(levelCoefficient, levelComponent.Level));
                _previousNeedExperience += (int)(experienceForLevel *
                                                 Math.Pow(levelCoefficient, levelComponent.Level - 1));
            }
        }
        
        private void SetStartValues()
        {
            foreach (var i in _playerLevelFilter)
            {
                ref var levelComponent = ref _playerLevelFilter.Get2(i);
                levelComponent.Level = 1;
                _previousNeedExperience = 0;
            }
            
            SetNeedLevelExperience();
        }
    }
}