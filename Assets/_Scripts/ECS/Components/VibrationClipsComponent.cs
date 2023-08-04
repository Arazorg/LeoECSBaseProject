using System;
using Lofelt.NiceVibrations;

namespace _Scripts.ECS.Components
{
    [Serializable]
    public struct VibrationClipsComponent
    {
        public HapticClip Clip;
    }
}