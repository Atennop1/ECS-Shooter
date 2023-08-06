using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public struct CharacterSlidingComponent : IComponent
    {
        public bool IsActive;

        public float SlideSpeed;
        public Vector3 SlidingSurfaceNormal;
    }
}