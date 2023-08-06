using System;
using Scellecs.Morpeh;
using Shooter.Physics;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCollisionDetector : MonoBehaviour
    {
        private World _ecsWorld;

        public void Construct(World world)
            => _ecsWorld = world ?? throw new ArgumentNullException(nameof(world));

        private void OnCollisionStay(Collision other)
        {
            var entity = _ecsWorld.CreateEntity();
            ref var createdStay = ref entity.AddComponent<OnCollisionStay>();

            createdStay.OriginGameObject = gameObject;
            createdStay.Collision = other;
        }
    }
}