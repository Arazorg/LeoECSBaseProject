using _Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class SpawnEnemiesSystem : IEcsRunSystem
    {
        private EcsFilter<TimeComponent> _timeFilter;
        private EcsFilter<EnemiesSpawnInfoComponent> _enemiesConfigsFilter;

        private float _timeToSpawn;

        public void Run()
        {
            CheckTime();
        }

        private void CheckTime()
        {
            foreach (var i in _timeFilter)
            {
                ref var timeComponent = ref _timeFilter.Get1(i);
                if (timeComponent.Time > _timeToSpawn)
                    SpawnEnemy(timeComponent.Time);
            }
        }

        private void SpawnEnemy(float spawnTime)
        {
            foreach (var i in _enemiesConfigsFilter)
            {
                ref var enemiesSpawnComponent = ref _enemiesConfigsFilter.Get1(i);
                _timeToSpawn = spawnTime + enemiesSpawnComponent.DelayBetweenSpawn;
                var number = Random.Range(0, enemiesSpawnComponent.EnemiesConfigs.Count);
                var prefab = enemiesSpawnComponent.EnemiesConfigs[number].Prefab;
                Object.Instantiate(prefab, GetSpawnPosition(), Quaternion.identity);
            }
        }

        private Vector3 GetSpawnPosition()
        {
            float radius = 3f;
            float angleRadians = Random.Range(-180f, 180f) * Mathf.PI / 180f;
            Vector3 spawnPosition = new Vector3(radius * Mathf.Cos(angleRadians), 0,
                radius * Mathf.Sin(angleRadians));

            return spawnPosition;
        }
    }
}