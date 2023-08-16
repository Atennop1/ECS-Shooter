using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Input
{
    public struct MovementInputComponent : IComponent
    {
        public bool IsSprintPressed;
        public Vector2 Vector;
    }
}