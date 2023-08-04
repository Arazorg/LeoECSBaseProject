using System;
using UnityEngine;

namespace _Scripts.ECS.Components
{
    [Serializable]
    public struct LevelComponent
    {
        [HideInInspector] public int Level;
        [HideInInspector] public int Experience;
        [HideInInspector] public int NeedLevelExperience;
    }
}