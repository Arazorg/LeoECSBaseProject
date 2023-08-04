using System;
using UnityEngine;

namespace _Scripts.ECS.Components
{
    [Serializable]
    public struct LevelComponent
    {
        public int ExperienceForLevel;
        public float LevelCoefficient;
        [HideInInspector] public int Level;
        [HideInInspector] public int Experience;
        [HideInInspector] public int PreviousRequiredAmountExperience;
        [HideInInspector] public int RequiredAmountExperience;
    }
}