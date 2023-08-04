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
                ref var experience = ref levelComponent.Experience;
                ref var requiredAmountExperience = ref levelComponent.RequiredAmountExperience;
                if (experience >= requiredAmountExperience)
                {
                    levelComponent.Level++;
                    SetNeedLevelExperience();
                }
            }
        }

        private void SetNeedLevelExperience()
        {
            foreach (var i in _playerLevelFilter)
            {
                ref var levelComponent = ref _playerLevelFilter.Get2(i);
                var experienceForLevel = levelComponent.ExperienceForLevel;
                var levelCoefficient = levelComponent.LevelCoefficient;

                levelComponent.RequiredAmountExperience += (int)(experienceForLevel *
                                                                 Math.Pow(levelCoefficient, levelComponent.Level));

                levelComponent.PreviousRequiredAmountExperience += (int)(experienceForLevel *
                                                                         Math.Pow(levelCoefficient,
                                                                             levelComponent.Level - 1));
            }
        }

        private void SetStartValues()
        {
            foreach (var i in _playerLevelFilter)
            {
                ref var levelComponent = ref _playerLevelFilter.Get2(i);
                levelComponent.Level = 1;
                levelComponent.PreviousRequiredAmountExperience = 0;
            }

            SetNeedLevelExperience();
        }
    }
}