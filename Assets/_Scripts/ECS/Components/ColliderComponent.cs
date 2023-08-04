using System;
using UnityEngine;

namespace _Scripts.ECS.Components
{
    [Serializable]
    public struct ColliderComponent
    {
        public Collider Collider;
    }
}