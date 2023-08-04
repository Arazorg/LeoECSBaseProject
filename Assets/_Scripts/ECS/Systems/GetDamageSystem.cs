using _Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace _Scripts.ECS.Systems
{
    public class GetDamageSystem : IEcsRunSystem
    {
        private EcsFilter<GetDamageComponent, HealthComponent> _getDamageFilter;

        public void Run()
        {
            HandleGetDamageComponents();
        }

        private void HandleGetDamageComponents()
        {
            foreach (var i in _getDamageFilter)
            {
                ref var getDamageComponent = ref _getDamageFilter.Get1(i);
                ref var healthComponent = ref _getDamageFilter.Get2(i);
                healthComponent.Health -= getDamageComponent.Damage;
                Debug.Log(healthComponent.Health);
                //_getDamageFilter.GetEntity(i).Del<GetDamageComponent>();
            }
        }
    }
}