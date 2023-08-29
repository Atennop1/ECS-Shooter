using Scellecs.Morpeh;

namespace Shooter.Character
{
    public struct CharacterFootstepsComponent : IComponent
    {
        public float CrouchingStepTime;
        public float WalkingStepTime;
        public float SprintingStepTime;
    }
}