using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Shooter.Character
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CharacterJumpingComponent : IComponent
    {
        public float VerticalVelocity;
        public float JumpHeight;
    }
}