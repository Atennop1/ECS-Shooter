using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterCrouchingComponent : IComponent
    {
        public bool IsActive;
        
        public CharacterCrouchingStateData StandingData;
        public CharacterCrouchingStateData CrouchingData;
    }
}