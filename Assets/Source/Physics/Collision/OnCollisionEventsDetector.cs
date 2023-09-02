using System;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Shooter.Physics
{
    public sealed class OnCollisionEventsDetector : MonoBehaviour
    {
        private World _ecsWorld;

        [Inject]
        public void Construct(World world)
            => _ecsWorld = world ?? throw new ArgumentNullException(nameof(world));
        
        private void OnCollisionEnter(Collision other)
        {
            var entity = _ecsWorld.CreateEntity();
            ref var createdComponent = ref entity.AddComponent<OnCollisionEnterComponent>();

            createdComponent.OriginGameObject = gameObject;
            createdComponent.Collision = other;
        }

        private void OnCollisionStay(Collision other)
        {
            var entity = _ecsWorld.CreateEntity();
            ref var createdComponent = ref entity.AddComponent<OnCollisionStayComponent>();

            createdComponent.OriginGameObject = gameObject;
            createdComponent.Collision = other;
        }
        
        private void OnCollisionExit(Collision other)
        {
            var entity = _ecsWorld.CreateEntity();
            ref var createdComponent = ref entity.AddComponent<OnCollisionExitComponent>();

            createdComponent.OriginGameObject = gameObject;
            createdComponent.Collision = other;
        }
    }
}