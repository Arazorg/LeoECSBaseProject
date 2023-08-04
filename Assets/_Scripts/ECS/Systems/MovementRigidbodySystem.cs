using _Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class MovementRigidbodySystem : IEcsRunSystem
    {
        private EcsFilter<RigidbodyComponent, MovementDirectionComponent, SpeedComponent>
            _characterMovementFilter;

        public void Run()
        {
            Movement();
        }

        private void Movement()
        {
            foreach (var i in _characterMovementFilter)
            {
                ref var rigidbodyComponent = ref _characterMovementFilter.Get1(i);
                ref var directionComponent = ref _characterMovementFilter.Get2(i);
                ref var speedComponent = ref _characterMovementFilter.Get3(i);
                var rigidbody = rigidbodyComponent.Rigidbody;
                var x = directionComponent.Direction.x;
                var z = directionComponent.Direction.z;
                rigidbody.velocity =
                    new Vector3(x * speedComponent.Speed, rigidbody.velocity.y, z * speedComponent.Speed);
            }
        }
    }
}