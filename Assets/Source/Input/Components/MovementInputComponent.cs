using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Input
{
    public struct MovementInputComponent : IComponent
    {
        public Vector2 Vector;
        
        public bool IsSprintKeyPressed;
        public bool IsSprintedKeyReleasedThisFrame;
    }
}