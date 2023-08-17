using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterHeadBobComponent : IComponent
    {
        public CharacterHeadbobData CrouchingBobData;
        public CharacterHeadbobData WalkingBobData;
        public CharacterHeadbobData SprintingBobData;
    }
}