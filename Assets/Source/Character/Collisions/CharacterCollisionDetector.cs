using System;
using Scellecs.Morpeh;
using Shooter.Physics;
using UnityEngine;
using Zenject;

namespace Shooter.Character
{
    public sealed class CharacterCollisionDetector : MonoBehaviour
    {
        private World _ecsWorld;

        [Inject]
        public void Construct(World world)
            => _ecsWorld = world ?? throw new ArgumentNullException(nameof(world));

        public void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Debug.Log("CollisionStay");
            
            var entity = _ecsWorld.CreateEntity();
            ref var createdStay = ref entity.AddComponent<CharacterOnCollisionStay>();

            createdStay.OriginGameObject = gameObject;
            createdStay.Hit = hit;
        }
    }
}