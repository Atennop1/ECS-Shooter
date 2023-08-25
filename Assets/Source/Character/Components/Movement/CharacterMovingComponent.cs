using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterMovingComponent : IComponent
    {
        public bool IsWalking;
        public float CurrentSpeed;
    }
}