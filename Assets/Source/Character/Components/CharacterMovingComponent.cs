using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterMovingComponent : IComponent
    {
        public bool IsWalking;
        public bool IsSprinting;
        
        public float Speed;
    }
}