using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterJumpingComponent : IComponent
    {
        public float VerticalVelocity;
        
        public float JumpHeight;
        public float GravitationalConstant;
    }
}