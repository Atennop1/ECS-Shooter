using Scellecs.Morpeh;

namespace Shooter.Input
{
    public struct SprintingInputComponent : IComponent
    {
        public bool IsSprintKeyPressed;
        public bool IsSprintedKeyReleasedThisFrame;
    }
}