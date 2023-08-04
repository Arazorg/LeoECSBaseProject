using _Scripts.ECS.Components;
using LeoEcsPhysics;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class HandleEnemiesCollisionSystem : IEcsRunSystem
    {
        private EcsFilter<OnCollisionEnterEvent> _collisionEnterEvent;
        private EcsFilter<CharacterTag, DamageComponent> _characterDamageFilter;
        private EcsFilter<EnemyTag, ColliderComponent> _enemyColliderFilter = null;

        private readonly EcsWorld _world;
        private GameObject _characterGameObject;

        public HandleEnemiesCollisionSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            HandleCollisionEvents();
        }

        private void HandleCollisionEvents()
        {
            foreach (var i in _collisionEnterEvent)
            {
                ref var onCollisionEnterEvent = ref _collisionEnterEvent.Get1(i);
                foreach (var j in _characterDamageFilter)
                {
                    ref var damageComponent = ref _characterDamageFilter.Get2(j);
                    EqualColliders(onCollisionEnterEvent, damageComponent.Damage);
                }
                
                _collisionEnterEvent.GetEntity(i).Destroy();
            }
        }

        private void EqualColliders(OnCollisionEnterEvent onCollisionEnterEvent, int damage)
        {
            foreach (var j in _enemyColliderFilter)
            {
                ref var colliderComponent = ref _enemyColliderFilter.Get2(j);
                if (colliderComponent.Collider == onCollisionEnterEvent.collider)
                {
                    AddGetDamageComponent(_enemyColliderFilter.GetEntity(j), damage);
                    CreateVibrationEntity();
                }
            }
        }

        private void AddGetDamageComponent(EcsEntity entity, int damage)
        {
            entity.Get<GetDamageComponent>().Damage = damage;
        }

        private void CreateVibrationEntity()
        {
            _world.NewEntity().Get<PlayVibrationOneFrame>();
        }
    }
}