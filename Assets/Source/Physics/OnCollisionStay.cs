using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Physics
{
    public struct OnCollisionStay : IComponent
    {
        public GameObject OriginGameObject;
        public Collision Collision;
    }
}