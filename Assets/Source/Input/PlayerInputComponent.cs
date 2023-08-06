using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Input
{
    public struct PlayerInputComponent : IComponent
    {
        public bool IsJumpKeyPressed;
        public bool IsShiftPressed;
        public Vector2 MovementInput;
    }
}