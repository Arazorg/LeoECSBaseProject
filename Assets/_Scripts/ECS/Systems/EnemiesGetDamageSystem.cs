using _Scripts.ECS.Components;
using LeoEcsPhysics;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class EnemiesGetDamageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<OnCollisionEnterEvent> _collisionEnterEvent;
        private EcsFilter<CharacterTag, GameObjectComponent, DamageComponent> _characterGameObjectFilter;
        private EcsFilter<EnemyTag, ColliderComponent, HealthComponent> _enemyFilter;

        private readonly EcsWorld _world;
        private GameObject _characterGameObject;

        public EnemiesGetDamageSystem(EcsWorld world)
        {
            _world = world;
        }
        
        public void Init()
        {
            SetCharacterGameObject();
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
                if (onCollisionEnterEvent.senderGameObject == _characterGameObject)
                {
                    foreach (var j in _enemyFilter)
                    {
                        ref var colliderComponent = ref _enemyFilter.Get2(j);
                        if (colliderComponent.Collider == onCollisionEnterEvent.collider)
                        {
                            ref var healthComponent = ref _enemyFilter.Get3(j);
                            healthComponent.Health -= GetDamage();
                            CreateVibrationEntity();
                        }
                    }
                }
                
                _collisionEnterEvent.GetEntity(i).Destroy();
            }
        }

        private int GetDamage()
        {
            foreach (var i in _characterGameObjectFilter)
            {
                ref var damageComponent = ref _characterGameObjectFilter.Get3(i);
                return damageComponent.Damage;
            }

            return 0;
        }

        private void CreateVibrationEntity()
        {
            _world.NewEntity().Get<PlayVibrationOneFrame>();
        }

        private void SetCharacterGameObject()
        {
            foreach (var i in _characterGameObjectFilter)
            {
                ref var gameObjectComponent = ref _characterGameObjectFilter.Get2(i);
                _characterGameObject = gameObjectComponent.GameObject;
            }
        }
    }
}