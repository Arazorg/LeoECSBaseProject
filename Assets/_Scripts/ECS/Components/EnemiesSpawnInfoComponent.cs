using System;
using System.Collections.Generic;
using _Scripts.MonoBehaviours.SO;

namespace _Scripts.ECS.Components
{
    [Serializable]
    public struct EnemiesSpawnInfoComponent
    {
        public List<EnemyConfig> EnemiesConfigs;
        public float DelayBetweenSpawn;
    }
}