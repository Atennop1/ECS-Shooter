using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Shooter.Character
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CharacterCrouchingStateData
    {
        public float Height;
        public Vector3 Center;
    }
}