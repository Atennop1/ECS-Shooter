using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Input
{
    public struct MovementInputComponent : IComponent
    {
        public bool IsSprintKeyPressed;
        public Vector2 Vector;
    }
}