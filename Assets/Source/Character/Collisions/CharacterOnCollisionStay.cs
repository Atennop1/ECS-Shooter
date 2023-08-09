using Scellecs.Morpeh;
using UnityEngine;

namespace Shooter.Character
{
    public struct CharacterOnCollisionStay : IComponent
    {
        public GameObject OriginGameObject;
        public ControllerColliderHit Hit;
    }
}