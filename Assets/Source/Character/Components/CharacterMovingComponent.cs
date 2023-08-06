using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterMovingComponent : IComponent
    {
        public float Speed;
        
        public bool IsWalking;
        public bool IsSprinting;
    }
}