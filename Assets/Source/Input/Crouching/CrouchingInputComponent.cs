using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Shooter.Input
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CrouchingInputComponent : IComponent
    {
        public bool IsCrouchKeyPressedThisFrame;
    }
}