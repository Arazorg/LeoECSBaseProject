using _Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class EnemiesMovementInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<EnemyTag, TransformComponent, MovementDirectionComponent> _enemyMovementFilter;
        private EcsFilter<CharacterTag, TransformComponent> _characterTransformFilter;

        private Transform _playerTransform;
        
        public void Init()
        {
            SetPlayerTransform();
        }

        public void Run()
        {
            SetDirectionToTarget();
        }

        private void SetDirectionToTarget()
        {
            foreach (var i in _enemyMovementFilter)
            {
                ref var transformComponent = ref _enemyMovementFilter.Get2(i);
                ref var movementDirectionComponent = ref _enemyMovementFilter.Get3(i);
                var direction = (_playerTransform.position - transformComponent.Transform.position).normalized;
                movementDirectionComponent.Direction = direction;
            }
        }

        private void SetPlayerTransform()
        {
            foreach (var i in _characterTransformFilter)
            {
                ref var transformComponent = ref _characterTransformFilter.Get2(i);
                _playerTransform = transformComponent.Transform;
            }
        }
    }
}