using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterCrouchingComponent : IComponent
    {
        public bool IsActive;
        public bool IsTransiting;
        
        public CharacterCrouchingStateData StandingStateData;
        public CharacterCrouchingStateData CrouchingStateData;
    }
}