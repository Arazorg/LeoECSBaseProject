using _Scripts.ECS.Components;
using _Scripts.ECS.Systems;
using _Scripts.MonoBehaviours.UI;
using LeoEcsPhysics;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;
using Voody.UniLeo;

namespace _Scripts.MonoBehaviours
{
    public class ECSBoostrap : MonoBehaviour
    {
        [SerializeField] private EcsUiEmitter _uiEmitter;
        [SerializeField] private GameUI _gameUI;

        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _pauseSystem;

        public void Init()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems.ConvertScene();
            EcsPhysicsEvents.ecsWorld = _world;
            AddSystems();
            _systems.Init();
            _pauseSystem = new EcsSystems(_world);
            _pauseSystem.Add(new PauseSystem(_world, _systems));
            _pauseSystem.Init();
        }

        private void Update()
        {
            _pauseSystem.Run();
        }

        private void AddSystems()
        {
            _systems.Add(new GameTimeSystem(_world)).
                     InjectUi(_uiEmitter).
                     Add(new SetPauseSystem(_gameUI)).
                     Add(new SpawnEnemiesSystem()).
                     Add(new CharacterMovementInputSystem()).
                     Add(new EnemiesMovementInputSystem()).
                     Add(new MovementRigidbodySystem()).
                     Add(new EnemiesGetDamageSystem(_world)).
                     Add(new PlayVibrationSystem()).
                     Add(new EnemiesDeathSystem()).
                     OneFrame<PlayVibrationOneFrame>();
        }

        private void OnDestroy()
        {
            if (_pauseSystem == null || _systems == null)
                return;

            EcsPhysicsEvents.ecsWorld = null;
            _pauseSystem.Destroy();
            _systems.Destroy();
            _pauseSystem = null;
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}