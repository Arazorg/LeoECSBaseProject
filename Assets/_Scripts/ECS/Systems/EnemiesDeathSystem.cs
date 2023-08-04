using _Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class EnemiesDeathSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyTag, HealthComponent, GameObjectComponent> _enemyHealthFilter;

        public void Run()
        {
            CheckHealth();
        }

        private void CheckHealth()
        {
            foreach (var i in _enemyHealthFilter)
            {
                ref var healthComponent = ref _enemyHealthFilter.Get2(i);
                if (healthComponent.Health <= 0)
                {
                    ref var gameObjectComponent = ref _enemyHealthFilter.Get3(i);
                    var gameObject = gameObjectComponent.GameObject;
                    _enemyHealthFilter.GetEntity(i).Destroy();
                    Object.Destroy(gameObject);
                }
            }
        }
    }
}