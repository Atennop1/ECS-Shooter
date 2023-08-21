using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterStaminaComponent : IComponent
    {
        public float CurrentValue;
        public float MaxValue;

        public float DecreasingPerSecondAmount;
    }
}