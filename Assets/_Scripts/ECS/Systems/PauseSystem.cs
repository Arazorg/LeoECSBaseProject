using _Scripts.ECS.Components;
using Leopotam.Ecs;

namespace _Scripts.ECS.Systems
{
    public class PauseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<PauseComponent> _pauseFilter;

        private readonly EcsWorld _ecsWorld;
        private readonly EcsSystems _systems;

        public PauseSystem(EcsWorld ecsWorld, EcsSystems systems)
        {
            _ecsWorld = ecsWorld;
            _systems = systems;
        }

        public void Init()
        {
            CreatePauseEntity();
        }

        public void Run()
        {
            if (!IsGamePausing())
                _systems.Run();
        }

        private bool IsGamePausing()
        {
            foreach (var i in _pauseFilter)
            {
                ref var pauseComponent = ref _pauseFilter.Get1(i);
                return pauseComponent.IsGamePausing;
            }

            return false;
        }

        private void CreatePauseEntity()
        {
            var pauseEntity = _ecsWorld.NewEntity();
            pauseEntity.Get<PauseComponent>();
        }
    }
}