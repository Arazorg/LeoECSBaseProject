using _Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class CharacterMovementInputSystem : IEcsRunSystem
    {
        private EcsFilter<JoystickComponent> _joystickFilter;
        private EcsFilter<CharacterTag, MovementDirectionComponent> _characterDirectionFilter;

        public void Run()
        {
            SetDirection();
        }

        private void SetDirection()
        {
            foreach (var i in _joystickFilter)
            {
                ref var joystickComponent = ref _joystickFilter.Get1(i);
                var joystick = joystickComponent.Joystick;
                foreach (var j in _characterDirectionFilter)
                {
                    var direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
                    ref var movementDirectionComponent = ref _characterDirectionFilter.Get2(j);
                    movementDirectionComponent.Direction = direction;
                }
            }
        }
    }
}