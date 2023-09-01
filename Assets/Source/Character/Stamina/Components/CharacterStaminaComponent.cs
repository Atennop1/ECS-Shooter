using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Shooter.Character
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CharacterStaminaComponent : IComponent
    {
        public float CurrentValue;
        public float MaxValue;

        public float DecreasingPerSecondAmount;
    }
}