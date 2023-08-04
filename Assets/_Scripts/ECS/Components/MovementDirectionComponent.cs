using System;
using UnityEngine;

namespace _Scripts.ECS.Components
{
    [Serializable]
    public struct MovementDirectionComponent
    {
        [HideInInspector] public Vector3 Direction;
    }
}