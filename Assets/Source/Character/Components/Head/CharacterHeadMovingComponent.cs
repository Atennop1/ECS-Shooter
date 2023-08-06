using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterHeadMovingComponent : IComponent
    {
        public float MouseSensitivity;
        
        public CharacterHeadbobData WalkingBobData;
        public CharacterHeadbobData SprintingBobData;
    }
}