using System;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Shooter.Physics
{
    public sealed class CollisionDetector : MonoBehaviour
    {
        private World _ecsWorld;

        [Inject]
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