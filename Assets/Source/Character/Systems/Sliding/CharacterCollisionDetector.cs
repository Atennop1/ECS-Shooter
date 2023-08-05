using System;
using Leopotam.EcsLite;
using Shooter.Physics;
using UnityEngine;

namespace Shooter.Character
{
    public sealed class CharacterCollisionDetector : MonoBehaviour
    {
        private EcsWorld _ecsWorld;

        public void Construct(EcsWorld ecsWorld)
            => _ecsWorld = ecsWorld ?? throw new ArgumentNullException(nameof(ecsWorld));

        private void OnCollisionStay(Collision other)
        {
            var entity = _ecsWorld.NewEntity();
            var ecsPool = _ecsWorld.GetPool<OnCollisionStay>();
            ecsPool.Add(entity);

            ref var createdStay = ref ecsPool.Get(entity);
            createdStay.OriginGameObject = gameObject;
            createdStay.Collision = other;
        }
    }
}