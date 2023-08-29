using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterHeadBobComponent : IComponent
    {
        public CharacterHeadBobData CrouchingBobData;
        public CharacterHeadBobData WalkingBobData;
        public CharacterHeadBobData SprintingBobData;
    }
}