using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterStaminaRegeneratingComponent : IComponent
    {
        public int TimeBeforeRegeneratingInMilliseconds;
        public float RegeneratingPerSecondAmount;
    }
}